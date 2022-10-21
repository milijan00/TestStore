using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfDeleteProductSpecificationCommand : EfBase, IDeleteProductSpecificationCommand
    {
        private DeleteProductSpecificationValidator _validator;
        public EfDeleteProductSpecificationCommand(TestStoreDbContext context, DeleteProductSpecificationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 45;

        public string Name => "EfDeleteProductSpecificationCommand";

        public bool AdminOnly => true;

        public void Execute(DeleteProductSpecificationDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var productSpecification = this.Context.ProductsSpecifications.FirstOrDefault(x => x.ProductId == data.ProductId.Value && x.SpecificationId == data.SpecificationId);
            this.Context.ProductsSpecifications.Remove(productSpecification);
            this.Context.SaveChanges();
        }
    }
}
