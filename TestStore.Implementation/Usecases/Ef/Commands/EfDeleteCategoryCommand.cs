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
    public class EfDeleteCategoryCommand : EfBase, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 26;

        public string Name => "EfDeleteCategoryCommand";

        public bool AdminOnly => true;

        public void Execute(int data)
        {
            var category = this.Context.Categories.Find(data);
            if(category == null)
            {
                throw new EntityNotFoundException();
            }

            this.Context.Categories.Remove(category);
            this.Context.SaveChanges();
        }
    }
}
