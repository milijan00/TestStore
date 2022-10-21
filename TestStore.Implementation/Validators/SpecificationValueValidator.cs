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
    public class SpecificationValueValidator : AbstractValidator<SpecificationValueDto>
    {
        public SpecificationValueValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.SpecificationId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Specification id must not be null or empty")
                .Must(id => context.Specifications.Any(x => x.Id == id)).WithMessage("There is no given specification.");

            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Specification's value must not be null or empty");
            RuleFor(x => new {value = x.Value, id = x.SpecificationId})
                .Must(y => !context.SpecificationsValues.Any(x => x.Value == y.value && x.SpecificationId == y.id )).WithMessage("There is already such value for given specification.");
        }
    }
}
