using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Implementation.DataAccess;

namespace TestStore.Implementation.Validators
{
    public class UpdateProductSpecificationValidator : AbstractValidator<ProductsSpecificationDto>
    {
        public UpdateProductSpecificationValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.ProductId)
               .Must(id => context.Products.Any(x => x.Id == id)).WithMessage("There is no such product.");

            RuleFor(x => x.SpecificationId)
                .Must(id => context.Specifications.Any(x => x.Id == id)).WithMessage("There is no such specification.");

            RuleFor(x => new { sid = x.SpecificationId, pid = x.ProductId })
                .Must(data => !context.ProductsSpecifications.Any(x => x.ProductId == data.pid && x.SpecificationId == data.sid)).WithMessage("This specification is already assigned to the given product.");
        }
    }
}
