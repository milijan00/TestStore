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
    public class EfCreateUserCommand : EfBase, ICreateUserCommand
    {
        private CreateUserValidator _validator;
        public EfCreateUserCommand(TestStoreDbContext context, CreateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "EfCreateUserCommand";

        public bool AdminOnly => false;

        public void Execute(CreateUserDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var hash = BCrypt.Net.BCrypt.HashPassword(data.Password);
            const int REGULAR_USER = 2;
            var user = new User
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Password = hash,
                Username = data.Username,
                RoleId = REGULAR_USER ,
                Image = "Default.jpg"
            };

            this.Context.Users.Add(user);
            this.Context.SaveChanges();
        }
    }
}
