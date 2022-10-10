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
    public class EfDeleteBrandCommand : EfBase, IDeleteBrandCommand
    {
        public EfDeleteBrandCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 30;

        public string Name => "EfDeleteBrandCommand";

        public bool AdminOnly => true;

        public void Execute(int data)
        {
            var brand = this.Context.Brands.Find(data);
            if(brand == null)
            {
                throw new EntityNotFoundException();
            }

            this.Context.Brands.Remove(brand);
            this.Context.SaveChanges();
        }
    }
}
