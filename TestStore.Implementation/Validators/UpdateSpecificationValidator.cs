using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Implementation.DataAccess;

namespace TestStore.Implementation.Validators
{
    public class UpdateSpecificationValidator : BaseSpecificationValidator
    {
        public UpdateSpecificationValidator(TestStoreDbContext context) : base(context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Specification's id must not be null.")
                .Must(id => context.Specifications.Any(x => x.Id == id)).WithMessage("There is no given specification.");
        }
    }
}
