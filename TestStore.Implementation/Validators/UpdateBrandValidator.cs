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
    public class UpdateBrandValidator : AbstractValidator<BrandDto>
    {
        public UpdateBrandValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Brand id must not be null")
                .Must(id => context.Brands.Any(x => x.Id == x.id)).WithMessage("There is no brand with the given id");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Brand's name mustn't be null or empty.")
                .Must(name => !context.Brands.Any(x => x.Name == name)).WithMessage("There is already a brand with given name.");
        }
    }
}
