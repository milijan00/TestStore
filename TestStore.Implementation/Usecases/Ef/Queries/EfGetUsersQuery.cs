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
    public class EfGetUsersQuery : EfBase, IGetUsersQuery
    {
        public EfGetUsersQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "EfGetUsersQuery";

        public bool AdminOnly => false;

        public IEnumerable<UserDto> Execute()
        {
            return this.Context.Users.Where(x => x.IsActive).Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Username = x.Username
            }).ToList();
        }
    }
}
