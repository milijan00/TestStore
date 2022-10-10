using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public interface IActionUser
    {
        public int Id { get;  }
        public IEnumerable<int>  AllowedUsecasesIds { get; }

        public string Role { get; }
        public string Identity { get; }
    }
}
