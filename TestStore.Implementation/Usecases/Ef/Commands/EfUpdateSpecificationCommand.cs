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
    public class EfUpdateSpecificationCommand : EfBase, IUpdateSpecificationCommand
    {
        private UpdateSpecificationValidator _validator;
        public EfUpdateSpecificationCommand(TestStoreDbContext context, UpdateSpecificationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 35;

        public string Name => "EfUpdateSpecificationCommand";

        public bool AdminOnly => true;

        public void Execute(SpecificationDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var specification = this.Context.Specifications.Find(data.Id.Value);
            specification.Name = data.Name;
            this.Context.SaveChanges();
        }
    }
}
