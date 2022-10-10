using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Domain;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfCreateUsecaseComand : EfBase,  ICreateUsecaseCommand
    {
        private UsecasesBaseValidator _validator;
        public EfCreateUsecaseComand(TestStoreDbContext context,  UsecasesBaseValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "EfCreateUsecaseCommand";

        public bool AdminOnly => true;

        public void Execute(UsecaseDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }

            var usecase = new Usecase
            {
                Name = data.Name
            };
            this.Context.Usecases.Add(usecase);
            this.Context.SaveChanges();
        }
    }
}
