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
    public class UpdateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public UpdateCategoryValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .Must(id => id > 0).WithMessage("Id has to be greater than 0")
                .Must(id => context.Categories.Any(x => x.Id == id)).WithMessage("There is no such category");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Category's name must not be empty.")
                .Must(name => !context.Categories.Any(c => c.Name == name)).WithMessage("There is already a category with given name.");
        }
    }
}
