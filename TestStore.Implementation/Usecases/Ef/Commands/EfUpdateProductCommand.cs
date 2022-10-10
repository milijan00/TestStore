using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Extensions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfUpdateProductCommand : EfBase, IUpdateProductCommand
    {
        private UpdateProductValidator _validator;
        public EfUpdateProductCommand(TestStoreDbContext context, UpdateProductValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "EfUpdateProductCommand";

        public bool AdminOnly => true;

        public void Execute(ProductDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var product = this.Context.Products.Find(data.Id.Value);

            if (data.Name.IsStringNotNullOrEmpty())
            {
                product.Name = data.Name;
            }

            if (data.Description.IsStringNotNullOrEmpty())
            {
                product.Description = data.Description;
            }

            if(data.Price != null)
            {
                product.Price.Value = data.Price.Value;
            }
            
            if(data.CategoryId != null)
            {
                product.CategoryId = data.CategoryId.Value;
            }

            if(data.BrandId != null)
            {
                product.BrandId = data.BrandId.Value;
            }
            if (data.ImageName.IsStringNotNullOrEmpty())
            {
                product.Image = data.ImageName;
            }

            this.Context.SaveChanges();

        }
    }
}
