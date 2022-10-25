using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TestStore.Implementation.DataAccess;

namespace TestStore.Implementation.Validators
{
    public class UpdateRoleValidator : BaseRoleValidator
    {
        public UpdateRoleValidator(TestStoreDbContext context) : base(context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Role's id must not be null")
                .Must(id => context.Roles.Any(x => x.Id == id)).WithMessage("There is no such role");
        }
    }
}
