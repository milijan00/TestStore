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
    public class EfCreateSpecificationValueCommand : EfBase, ICreateSpecificationValueCommand
    {
        private SpecificationValueValidator _validator;
        public EfCreateSpecificationValueCommand(TestStoreDbContext context, SpecificationValueValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 39;

        public string Name => "EfCreateSpecificationValueCommand";

        public bool AdminOnly => true;

        public void Execute(SpecificationValueDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var specificationValue = new SpecificationValue
            {
                SpecificationId = data.SpecificationId.Value,
                Value = data.Value
            };
            this.Context.SpecificationsValues.Add(specificationValue);
            this.Context.SaveChanges();
        }
    }
}
