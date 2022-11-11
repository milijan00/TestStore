using System.IdentityModel.Tokens.Jwt;

namespace TestStore.Web.Core
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _accessor;
        private JwtSecurityTokenHandler _tokenHandler;
        private string _cookie;
        public AuthService(IHttpContextAccessor accessor, JwtSecurityTokenHandler tokenHandler)
        {
            _accessor = accessor;
            _tokenHandler = tokenHandler;
            
        }
        private void RetrieveCookieFromRequest()
        {
            this._cookie = this._accessor.HttpContext.Request.Headers.Cookie.FirstOrDefault(x => x.StartsWith("access="));
            if (this._cookie != null)
            {
                this._cookie = this._cookie.Split("=")[1];
            }
        }
        public bool Authenticated
        {
            get
            {
                this.RetrieveCookieFromRequest();
                if (this._cookie == null || !this.JWTIsValid) return false;
                return true;
            }
        }
        
        private bool JWTIsValid
        {
            get {
                return this._tokenHandler.CanReadToken(this._cookie);  
            }
        }
        public JwtSecurityToken Token 
        {
            get
            {
                if (this.JWTIsValid) return this._tokenHandler.ReadJwtToken(this._cookie);
                else throw new InvalidOperationException();
            }
        }
        
    }
}
