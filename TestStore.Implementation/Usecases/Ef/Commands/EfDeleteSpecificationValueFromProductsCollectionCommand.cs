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
    public class EfDeleteSpecificationValueFromProductsCollectionCommand : EfBase, IDeleteSpecificationValueFromProductsCollectionCommand
    {
        public EfDeleteSpecificationValueFromProductsCollectionCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 53;

        public string Name => "EfDeleteSpecificationValueFromProductsCollectionCommand";

        public bool AdminOnly => true;

        public void Execute(RemoveSpecificationDto data)
        {
            var productSpecification = this.Context.ProductsSpecifications.FirstOrDefault(x => x.ProductId == data.ProductId && x.SpecificationId == data.SpecificationId);
            if(productSpecification == null)
            {
                throw new EntityNotFoundException();
            }

            this.Context.ProductsSpecifications.Remove(productSpecification);
            this.Context.SaveChanges();
        }
    }
}
