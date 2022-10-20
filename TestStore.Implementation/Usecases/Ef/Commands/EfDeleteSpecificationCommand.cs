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
    public class EfDeleteSpecificationCommand : EfBase, IDeleteSpecificationCommand
    {
        public EfDeleteSpecificationCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 36;

        public string Name => "EfDeleteSpecificationCommand";

        public bool AdminOnly => true;

        public void Execute(int data)
        {
            var specification = this.Context.Specifications.Find(data);
            if(specification == null)
            {
                throw new EntityNotFoundException();
            }

            specification.SpecificationValues.Clear();
            specification.IsActive = false;
            this.Context.SaveChanges();
        }
    }
}
