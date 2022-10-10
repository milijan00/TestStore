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
    public class CreateBrandValidator : AbstractValidator<BrandDto>
    {
        public CreateBrandValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Brand's name mustn't be null or empty.")
                .Must(name => !context.Brands.Any(x => x.Name == name)).WithMessage("There is already a brand with given name.");
        } 
    }
}
