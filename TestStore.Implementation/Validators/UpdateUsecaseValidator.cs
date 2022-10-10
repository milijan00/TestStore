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
    public class UpdateUsecaseValidator : AbstractValidator<UpdateUsecaseDto>
    {
        public UpdateUsecaseValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Usecase id must not be null.")
                .Must(id => context.Usecases.Any(x => x.Id == id)).WithMessage("There is no such usecase.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Usecase name must not be null or empty.")
                .Must(name => !context.Usecases.Any(u => u.Name == name)).WithMessage("There is already an usecase with given name.");
        }
    }
}
