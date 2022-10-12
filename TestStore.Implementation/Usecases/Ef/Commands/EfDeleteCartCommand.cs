using Microsoft.EntityFrameworkCore;
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
    public class EfDeleteCartCommand : EfBase, IDeleteCartCommand
    {
        public EfDeleteCartCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 31;

        public string Name => "EfDeleteCartCommand";

        public bool AdminOnly => false;

        public void Execute(int data)
        {
            var cart = this.Context.Carts.Include(x => x.Products).FirstOrDefault(x => x.Id == data);
            if(cart == null)
            {
                throw new EntityNotFoundException();
            }

            cart.Products.Clear();
            cart.IsActive = false;
            this.Context.SaveChanges();
        }
    }
}
