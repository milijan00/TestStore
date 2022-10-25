using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;

namespace TestStore.Implementation.Usecases.Ef.Queries
{
    public class EfGetRolesQuery : EfBase, IGetRolesQuery
    {
        public EfGetRolesQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 50;

        public string Name => "EfGetRolesQuery";

        public bool AdminOnly => true;

        public IEnumerable<RoleDto> Execute()
        {
            return this.Context.Roles.Where(x => x.IsActive).Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
