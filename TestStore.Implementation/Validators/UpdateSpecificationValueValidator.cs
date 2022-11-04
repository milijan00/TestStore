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
    public class UpdateSpecificationValueValidator : AbstractValidator<SpecificationValueDto> 
    {
        public UpdateSpecificationValueValidator(TestStoreDbContext context) 
        {
            RuleFor(x => x.NewValue)
                .NotEmpty().WithMessage("Specification's new value must not be null or empty.");
            RuleFor(x => new {newValue = x.NewValue, id = x.SpecificationId})
                .Must(x => !context.SpecificationsValues.Any(y => y.SpecificationId == x.id && y.Value == x.newValue)).WithMessage("There is already a specification with given value");
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Specification's value must not be null or empty");
            RuleFor(x => new {value = x.Value, id = x.SpecificationId})
                .Must(y => context.SpecificationsValues.Any(x => x.Value == y.value && x.SpecificationId == y.id )).WithMessage("There is no such value for given specification.");
        }
    }
}
