using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfDeleteSpecificationValueCommand : EfBase, IDeleteSpecificationValueCommand
    {
        public EfDeleteSpecificationValueCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 41;

        public string Name => "EfDeleteSpecificationValueCommand";

        public bool AdminOnly => true;

        public void Execute(SpecificationValueDto data)
        {
            var specificationValue = this.Context.SpecificationsValues.FirstOrDefault(x => x.SpecificationId == data.SpecificationId.Value && x.Value == data.Value);
            if(specificationValue == null)
            {
                throw new EntityNotFoundException();
            }
            this.Context.SpecificationsValues.Remove(specificationValue);
            this.Context.SaveChanges();
        }
    }
}
