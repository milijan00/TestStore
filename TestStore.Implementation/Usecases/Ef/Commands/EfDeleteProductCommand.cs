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
    public class EfDeleteProductCommand : EfBase, IDeleteProductCommand
    {
        public EfDeleteProductCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 20;

        public string Name => "EfDeleteProductCommand";

        public bool AdminOnly => true;

        public void Execute(int data)
        {
            var product = this.Context.Products.Find(data);
            if(product == null)
            {
                throw new EntityNotFoundException();
            }

            this.Context.Products.Remove(product);
            this.Context.SaveChanges();
        }
    }
}
