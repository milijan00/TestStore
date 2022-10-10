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
    public class EfDeleteUserCommand : EfBase, IDeleteUserCommand
    {
        public EfDeleteUserCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 15;

        public string Name => "EfDeleteUserCommand";

        public bool AdminOnly => false;

        public void Execute(int data)
        {
            var user = this.Context.Users.Find(data);
            if(user == null)
            {
                throw new EntityNotFoundException();
            }
            this.Context.Users.Remove(user);
            this.Context.SaveChanges();
        }
    }
}
