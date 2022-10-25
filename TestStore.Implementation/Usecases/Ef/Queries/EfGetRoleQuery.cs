using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;

namespace TestStore.Implementation.Usecases.Ef.Queries
{
    public class EfGetRoleQuery : EfBase, IGetRoleQuery
    {
        public EfGetRoleQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 51;

        public string Name => "EfGetRoleQuery";

        public bool AdminOnly => true;

        public RoleDto Execute(int data)
        {
            var role = this.Context.Roles.Find(data);
            if(role == null)
            {
                throw new EntityNotFoundException();
            }
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
