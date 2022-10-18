using System.IdentityModel.Tokens.Jwt;

namespace TestStore.Web.Core
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _accessor;
        private JwtSecurityTokenHandler _tokenHandler;
        private string _cookie;
        public AuthService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public void RetrieveCookieFromRequest()
        {
            this._cookie = this._accessor.HttpContext.Request.Headers.Cookie.FirstOrDefault(x => x.StartsWith("access="));
        }
        public bool Authenticated
        {
            get
            {
                if (this._cookie == null && this.JWTIsValid) return false;
                return true;
            }
        }
        
        private bool JWTIsValid
        {
            get {
                return this._tokenHandler.CanReadToken(this._cookie);  
            }
        }
        
    }
}
