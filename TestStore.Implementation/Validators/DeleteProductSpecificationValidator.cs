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
    public class DeleteProductSpecificationValidator : AbstractValidator<DeleteProductSpecificationDto>
    {
        public DeleteProductSpecificationValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Product's id must not be  null or empty.")
                .Must(id => context.Products.Any(x => x.Id == id)).WithMessage("There is no such product.");
            RuleFor(x => x.SpecificationId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Specification's id must not be  null or empty.")
                .Must(id => context.Specifications.Any(x => x.Id == id)).WithMessage("There is no such specification.");

            RuleFor(x => new { pId = x.ProductId, sId = x.SpecificationId })
                .Must(ids => context.ProductsSpecifications.Any(x => x.ProductId == ids.pId && x.SpecificationId == ids.sId)).WithMessage("There is no such specifications that is appended to the given product.");
        }
    }
}
