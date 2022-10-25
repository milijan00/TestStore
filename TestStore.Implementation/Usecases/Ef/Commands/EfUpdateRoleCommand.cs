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
    public class EfUpdateRoleCommand : EfBase, IUpdateRoleCommand
    {
        private UpdateRoleValidator _validator;
        public EfUpdateRoleCommand(TestStoreDbContext context,UpdateRoleValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 48;

        public string Name => "EfUpdateRoleCommand";

        public bool AdminOnly => true;

        public void Execute(RoleDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var role = this.Context.Roles.Find(data.Id.Value);
            role.Name = data.Name;
            this.Context.SaveChanges();
        }
    }
}
