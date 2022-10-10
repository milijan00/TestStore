using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class Role : Entity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<RoleUsecase> Usecases { get; set; } = new List<RoleUsecase>();


    }
}
