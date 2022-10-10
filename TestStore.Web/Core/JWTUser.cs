using TestStore.Domain;

namespace TestStore.Web.Core
{
    public class JWTUser : IActionUser
    {
        public IEnumerable<int> AllowedUsecasesIds { get; set; }

        public string Role { get; set; }

        public string Identity { get; set; }

        public int Id { get; set; }

        public string Username { get; set; }
    }
}
