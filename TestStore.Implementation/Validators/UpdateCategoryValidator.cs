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
    public class UpdateCategoryValidator : CategoryBaseValidator 
    {
        public UpdateCategoryValidator(TestStoreDbContext context) : base(context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Category id must not be null")
                .Must(id => context.Categories.Any(x => x.Id == id)).WithMessage("There is no such category");
        }
    }
}
