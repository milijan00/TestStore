using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class RoleUsecase
    {
        public int RoleId { get; set; }
        public int UsecaseId { get; set; }
        public Role Role { get; set; }
        public Usecase Usecase { get; set; }
    }
}
