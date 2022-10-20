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
    public class EfCreateSpecificationCommand : EfBase, ICreateSpecificationCommand
    {
        private BaseSpecificationValidator _validator;
        public EfCreateSpecificationCommand(TestStoreDbContext context, BaseSpecificationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 34;

        public string Name => "EfCreateSpecificationCommand";

        public bool AdminOnly => true;

        public void Execute(SpecificationDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var specification = new Specification
            {
                Name = data.Name
            };
            this.Context.Specifications.Add(specification);
            this.Context.SaveChanges();
        }
    }
}
