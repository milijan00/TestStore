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
    public class CreateProductValidator : AbstractValidator<ProductDto>
    {
        public CreateProductValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product's name mustn't be null or empty.")
                .Must(name => !context.Products.Any(p => p.Name == name)).WithMessage("There is already a product with the given name.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product's description mustn't be null or empty.");

            RuleFor(x => x.Price)
                .Must(price => price > 0).WithMessage("Product's price must be greater than zero.");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Category id must not be null.")
                .Must(id => context.Categories.Any(x => x.Id == id)).WithMessage("There is no category with the given id");

            RuleFor(x => x.BrandId)
                .NotNull().WithMessage("Category id must not be null.")
                .Must(id => context.Brands.Any(x => x.Id == id)).WithMessage("There is no Brand with the given id");
        }
    }
}
