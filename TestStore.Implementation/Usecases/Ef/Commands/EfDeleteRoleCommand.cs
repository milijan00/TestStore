using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfDeleteRoleCommand : EfBase, IDeleteRoleCommand
    {
        public EfDeleteRoleCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 49;

        public string Name => "EfDeleteRoleCommand";

        public bool AdminOnly => true;

        public void Execute(int data)
        {
            var role = this.Context.Roles.Find(data);
            if(role == null)
            {
                throw new EntityNotFoundException();
            }

            role.IsActive = false;
            this.Context.SaveChanges();
        }
    }
}
