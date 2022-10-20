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
    public class BaseSpecificationValidator : AbstractValidator<SpecificationDto>
    {
        public BaseSpecificationValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Specification's name must not be null or empty")
                .Must(name => !context.Specifications.Any(x => x.Name == name)).WithMessage("There is already a specification with given name.");
        }
    }
}
