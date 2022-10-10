using TestStore.Domain;

namespace TestStore.Web.Core
{
    public class AnonymousUser : IActionUser
    {
        public IEnumerable<int> AllowedUsecasesIds => new List<int>();

        public string Role => "Unauthenticated";

        public string Identity => "Anonymous";

        public int Id => 0;
    }
}
