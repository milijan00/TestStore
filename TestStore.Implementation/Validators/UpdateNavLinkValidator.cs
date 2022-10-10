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
    public class UpdateNavLinkValidator : AbstractValidator<NavLinkDto>
    {
        public UpdateNavLinkValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => id > 0).WithMessage("There is no navigation links with given id.")
                .Must(id => context.NavLinks.Any(x => x.Id == id)).WithMessage("There is no such navigation link");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Must(name => !context.NavLinks.Any(x => x.Name == name)).WithMessage("There is already a navigation link with given name.").When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Controller)
                .Cascade(CascadeMode.Stop)
                .Must(controller => !context.NavLinks.Any(x => x.Controller == controller)).WithMessage("There is already a navigation link with given controller name.").When(x => !string.IsNullOrEmpty( x.Controller));
        }
    }
}
