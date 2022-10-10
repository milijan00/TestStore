using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfUpdateUsecaseCommand : EfBase, IUpdateUsecaseCommand
    {
        private UpdateUsecaseValidator _validator;
        public EfUpdateUsecaseCommand(TestStoreDbContext context, UpdateUsecaseValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "EfUpdateUsecaseCommand";

        public bool AdminOnly => true;

        public void Execute(UpdateUsecaseDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var usecase = this.Context.Usecases.Find(data.Id.Value);
            usecase.Name = data.Name;
            this.Context.SaveChanges();
        }
    }
}
