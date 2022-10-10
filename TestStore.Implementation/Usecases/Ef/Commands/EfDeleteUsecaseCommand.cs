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
    public class EfDeleteUsecaseCommand : EfBase, IDeleteUsecaseCommand
    {
        public EfDeleteUsecaseCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 3;

        public string Name => "EfDeleteUsecaseCommand";

        public bool AdminOnly => true;

        public void Execute(int data)
        {
            var usecase = this.Context.Usecases.Find(data);
            if(usecase == null)
            {
                throw new EntityNotFoundException();
            }
            //this.Context.Usecases.Remove(usecase);
            usecase.IsActive = false;
            this.Context.SaveChanges();
        }
    }
}
