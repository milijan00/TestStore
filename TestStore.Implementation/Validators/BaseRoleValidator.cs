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
    public class BaseRoleValidator : AbstractValidator<RoleDto>
    {
        public BaseRoleValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Role's name must not be null or empty.")
                .Must(name => !context.Roles.Any(x => x.Name == name)).WithMessage("There is already a role with the given name.");
        }
    }
}
