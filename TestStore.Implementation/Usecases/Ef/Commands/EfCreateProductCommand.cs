using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Domain;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfCreateProductCommand : EfBase, ICreateProductCommand
    {
        private CreateProductValidator _validator;
        public EfCreateProductCommand(TestStoreDbContext context, CreateProductValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 18;

        public string Name => "EfCreateProductCommand";

        public bool AdminOnly => true;

        public void Execute(ProductDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var product = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                CategoryId = data.CategoryId.Value,
                BrandId = data.BrandId.Value,
                Image = data.ImageName

            };
            product.Price = new ProductPrice()
            {
                Product = product,
                Value = data.Price.Value
            };

            this.Context.Products.Add(product);
            this.Context.SaveChanges();
        }
    }
}
