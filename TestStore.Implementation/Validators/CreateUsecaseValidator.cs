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
    public class CreateUsecaseValidator : AbstractValidator<CreateUsecaseDto>
    {
        public CreateUsecaseValidator(TestStoreDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Usecase name must not be empty.")
                .Must(name => !context.Usecases.Any(u => u.Name == name)).WithMessage("There is already an usecase with given name.");
        }
    }
}
