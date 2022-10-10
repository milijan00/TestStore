using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Extensions;

namespace TestStore.Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UserDto>
    {
        public UpdateUserValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User id mmust not be null")
                .Must(id => id > 0).WithMessage("User's id value is unacceptable.")
                .Must(id => context.Users.Any(x=> x.Id == id)).WithMessage("There is given user.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .Must(email => !context.Users.Any(x => x.Email == email))
                    .WithMessage("There is already an user with the given email.")
                    .When(x => x.Email.IsStringNotNullOrEmpty());

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .Must(username => !context.Users.Any(x => x.Username == username))
                    .WithMessage("There is already an user with the given username.")
                    .When(x => x.Username.IsStringNotNullOrEmpty());

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^[^0-9$-+?!()\[\]\{\}]*$")
                    .WithMessage("No numbers of special characters are allowed within the first name.")
                    .When(x => x.FirstName.IsStringNotNullOrEmpty());

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^[^0-9$-+?!()\[\]\{\}]*$")
                    .WithMessage("No numbers of special characters are allowed within the last name.")
                    .When(x => x.LastName.IsStringNotNullOrEmpty());

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$")
                    .WithMessage("At least one letter and one number. Minimum characters 8 and maximum is 16")
                    .When(x => x.Password.IsStringNotNullOrEmpty());

            RuleFor(x => x.NewPassword)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$")
                    .WithMessage("At least one letter and one number. Minimum characters 8 and maximum is 16")
                    .When(x => x.NewPassword.IsStringNotNullOrEmpty());
            
        }
    }
}
