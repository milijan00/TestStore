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
    public class UpdateProductValidator : AbstractValidator<ProductDto>
    {
        public UpdateProductValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product's name mustn't be null or empty.")
                .Must(name => !context.Products.Any(p => p.Name == name)).WithMessage("There is already a product with the given name.")
                .When(x => x.Name.IsStringNotNullOrEmpty());


            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product's description mustn't be null or empty.")
                .When(x => x.Description.IsStringNotNullOrEmpty());

            RuleFor(x => x.Price)
                .Must(price => price > 0).WithMessage("Product's price must be greater than zero.")
                .When(x => x.Price != null);

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Category id must not be null.")
                .Must(id => context.Categories.Any(x => x.Id == id)).WithMessage("There is no category with the given id")
                .When(x => x.CategoryId != null);

            RuleFor(x => x.BrandId)
                .NotNull().WithMessage("Category id must not be null.")
                .Must(id => context.Brands.Any(x => x.Id == id)).WithMessage("There is no Brand with the given id")
                .When(x => x.BrandId != null);
        }
    }
}
