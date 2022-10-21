using System; 
using TestStore.Application.Usecases.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Implementation.DataAccess;
using TestStore.Application.Dto;
using TestStore.Implementation.Validators;
using TestStore.Domain;
using TestStore.Implementation.Exceptions;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfCreateProductSpecificationCommand : EfBase, ICreateProductSpecificationCommand
    {
        private ProductSpecificationValidator _validator;
        public EfCreateProductSpecificationCommand(TestStoreDbContext context, ProductSpecificationValidator validator) : base(context)
        {
            _validator = validator;
        }


        public int Id => 44;

        public string Name => "EfCreateProductSpecificationCommand";

        public bool AdminOnly => true;

        public void Execute(ProductSpecificationsDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var productSpecifications = data.SpecificationsIds.Select(x => new ProductSpecification
            {
                ProductId = data.ProductId.Value,
                SpecificationId = x
            });
            this.Context.ProductsSpecifications.AddRange(productSpecifications);
            this.Context.SaveChanges();
        }
    }
}
