using Microsoft.EntityFrameworkCore;
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
    public class EfUpdateProductSpecificationCommand : EfBase, IUpdateProductSpecfiicationCommand
    {
        private UpdateProductSpecificationValidator _validator;
        public EfUpdateProductSpecificationCommand(TestStoreDbContext context, UpdateProductSpecificationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 55;

        public string Name => "EfUpdateProductSpecificationCommand";

        public bool AdminOnly => true;

        public void Execute(ProductsSpecificationDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var product = this.Context.Products.Include(x => x.Specifications).First(x => x.Id == data.ProductId);
            product.Specifications.Add(new Domain.ProductSpecification
            {
                ProductId = product.Id,
                SpecificationId = data.SpecificationId
            });
            this.Context.SaveChanges(); 
        }
    }
}
