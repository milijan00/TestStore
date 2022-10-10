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
    public class EfGetUserQuery : EfBase, IGetUserQuery
    {
        public EfGetUserQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 13;

        public string Name => "EfGetUserQuery";

        public bool AdminOnly => false;

        public UserDto Execute(int data)
        {
            var user = this.Context.Users.Find(data);
            if(user == null)
            {
                throw new EntityNotFoundException();
            }
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
