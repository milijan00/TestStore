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
    public class EfCreateRoleCommand : EfBase, ICreateRoleCommand
    {
        private BaseRoleValidator _validator;
        public EfCreateRoleCommand(TestStoreDbContext context, BaseRoleValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 47; 

        public string Name => "EfCreateRoleCommand";

        public bool AdminOnly => true;

        public void Execute(RoleDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var role = new Role
            {
                Name = data.Name
            };
            this.Context.Roles.Add(role);
            this.Context.SaveChanges();
        }
    }
}
