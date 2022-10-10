namespace TestStore.Web.Core
{
    public class JWTSettings
    {
        public int AccessTokenMinutes { get; set; }
        public int RefreshTokenMinutes { get; set; }
        public string Issuer { get; set; }
        public string AccessSecretKey { get; set; }
        public string RefreshSecretKey { get; set; }
    }
}
