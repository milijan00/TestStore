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
    public class BrandBaseValidator : AbstractValidator<BrandDto>
    {
        protected TestStoreDbContext Context { get; set; }
        public BrandBaseValidator(TestStoreDbContext context)
        {
            this.Context = context; 

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Brand's name mustn't be null or empty.")
                .Must(name => !context.Brands.Any(x => x.Name == name)).WithMessage("There is already a brand with given name.");

        }
    }
}
