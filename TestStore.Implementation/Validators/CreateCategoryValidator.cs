﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Implementation.DataAccess;

namespace TestStore.Implementation.Validators
{
    public class CreateCategoryValidator : AbstractValidator<Application.Dto.CategoryDto>
    {
        public CreateCategoryValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Category's name must not be empty.")
                .Must(name => !context.Categories.Any(c => c.Name == name)).WithMessage("There is already a category with given name.");
        }
    }
}
