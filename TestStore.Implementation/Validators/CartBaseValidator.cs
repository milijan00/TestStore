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
    public class CartBaseValidator : AbstractValidator<CartDto>
    {
        public CartBaseValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User's id must not be null or empty")
                .Must(id => context.Users.Any(x => x.Id == id)).WithMessage("There is no such user");

            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("List of products must not be empty.")
                .Must(p => p.Count() == p.Distinct().Count()).WithMessage("There are some duplicates in the product list.");

            RuleForEach(x => x.Products)
                .Must(p => context.Products.Any(x => x.Id == p.Id)).WithMessage("The given product doesn't exist.");
        }
    }
}
