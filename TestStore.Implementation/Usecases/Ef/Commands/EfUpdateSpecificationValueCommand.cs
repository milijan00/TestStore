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
    public class EfUpdateSpecificationValueCommand : EfBase, IUpdateSpecificationValueCommand
    {
        private UpdateSpecificationValueValidator _validator;
        public EfUpdateSpecificationValueCommand(TestStoreDbContext context, UpdateSpecificationValueValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 40;

        public string Name => "EfUpdateSpecificationValueCommand";

        public bool AdminOnly => true;

        public void Execute(SpecificationValueDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var specificationValue = this.Context.SpecificationsValues.FirstOrDefault(x => x.SpecificationId == data.SpecificationId.Value);
            specificationValue.Value = data.NewValue;
            this.Context.SaveChanges();
        }
    }
}
