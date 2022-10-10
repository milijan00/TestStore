using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestStore.Domain;
using TestStore.Implementation.DataAccess;

namespace TestStore.Web.Core
{
    public class JWTManager
    {
        private readonly TestStoreDbContext _context;
        private readonly JWTSettings _settings;

        public JWTManager(TestStoreDbContext context, JWTSettings settings)
        {
            _settings = settings;
            _context = context;
        }

        public List<string> MakeTokens(int id)
        {
            var user = _context.Users.Where(u => u.IsActive).Include(x => x.Role).ThenInclude(r => r.Usecases).FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            return new List<string>() { this.GenerateAccessToken(user), this.GenerateRefreshToken(user) };
        }
        public List<string> MakeTokens(string username, string password)
        {
            var user = _context.Users.Where(u => u.IsActive).Include(x => x.Role).ThenInclude(r => r.Usecases).FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(!valid)
            {
                throw new UnauthorizedAccessException();
            }

            return new List<string>() { this.GenerateAccessToken(user), this.GenerateRefreshToken(user) };
        }
        private string GenerateAccessToken(User user)
        {
            var actor = new JWTUser
            {
                Id = user.Id,
                AllowedUsecasesIds = user.Role.Usecases.Select(x => x.UsecaseId).ToList(),
                Identity = user.Role.Name,
                Username = user.Username
            };

            var claims = new List<Claim> // Jti : "",
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _settings.Issuer),
                new Claim("Usecases", JsonConvert.SerializeObject(actor.AllowedUsecasesIds)),
                new Claim("Role", user.Role.Name),
                //new Claim("Email", user.Email),
                new Claim("Username", user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.AccessSecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.AccessTokenMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken(User user)
        {
            var actor = new JWTUser
            {
                Id = user.Id,
                Identity = user.Role.Name
            };

            var claims = new List<Claim> // Jti : "",
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _settings.Issuer)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.RefreshSecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.RefreshTokenMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
