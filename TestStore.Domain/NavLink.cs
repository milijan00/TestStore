using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class NavLink : Entity
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
