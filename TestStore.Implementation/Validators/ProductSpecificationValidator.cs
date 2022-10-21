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
    public class ProductSpecificationValidator : AbstractValidator<ProductSpecificationsDto>
    {
        public ProductSpecificationValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Product's id must not be null.")
                .Must(id => context.Products.Any(x => x.Id == id)).WithMessage("There is no such product.");

            RuleFor(x => x.SpecificationsIds)
                .Must(ids => ids.Count() == ids.Distinct().Count()).WithMessage("There are some duplicates in the list.");
            RuleForEach(x => x.SpecificationsIds)
                .Must(id => id > 0).WithMessage("Id has to be greater than 0")
                .Must(id => context.Specifications.Any(x => x.Id == id)).WithMessage("There are some specifications that don't exist");
                
        }
    }
}
