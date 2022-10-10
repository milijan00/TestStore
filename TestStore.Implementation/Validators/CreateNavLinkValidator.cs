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
    public class CreateNavLinkValidator : AbstractValidator<NavLinkDto>
    {
        public CreateNavLinkValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nav link's name must not be null or empty.")
                .Must(name => !context.NavLinks.Any(x => x.Name == name)).WithMessage("There is already a nav. link with given name.");

            RuleFor(x => x.Controller)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nav link's controller name must not be null or empty.")
                .Matches(@"^[a-z-]{0,29}").WithMessage("Controller name must begin with the forward slash. After it, it can contain letters and - symbol.");
        }
    }
}
