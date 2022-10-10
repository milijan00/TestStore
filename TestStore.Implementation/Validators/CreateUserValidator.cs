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
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Users first name must not be null or empty.")
                .MaximumLength(30).WithMessage("First name's maximum length is 30 characters.");
                
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Users last name must not be null or empty.")
                .MaximumLength(30).WithMessage("Last name's maximum length is 30 characters.");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Users username must not be null or empty.")
                .MaximumLength(30).WithMessage("Username's maximum length is 30 characters.")
                .Must(username => !context.Users.Any(u => u.Username == username)).WithMessage("There is already an user with given username.");
            
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Users email must not be null or empty.")
                .MaximumLength(100).WithMessage("Email's maximum length is 100 characters.")
                .Must(email => !context.Users.Any(u => u.Email == email)).WithMessage("There is already an user with given email.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Users email must not be null or empty.")
                .MaximumLength(100).WithMessage("Password's maximum length is 100 characters.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Minimum eight characters, at least one letter and one number");
        }
    }
}
