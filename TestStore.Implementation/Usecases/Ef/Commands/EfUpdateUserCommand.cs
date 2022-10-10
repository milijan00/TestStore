using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Extensions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfUpdateUserCommand : EfBase, IUpdateUserCommand
    {
        private UpdateUserValidator _validator;
        public EfUpdateUserCommand(TestStoreDbContext context, UpdateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 14;

        public string Name => "EfUpdateUserCommand";

        public bool AdminOnly => false;

        public void Execute(UserDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }

            var user = this.Context.Users.Find(data.Id.Value);
            if (data.FirstName.IsStringNotNullOrEmpty())
            {
                user.FirstName = data.FirstName;
            }

            if (data.LastName.IsStringNotNullOrEmpty())
            {
                user.LastName = data.LastName;
            }

            if (data.Username.IsStringNotNullOrEmpty())
            {
                user.Username = data.Username;
            }

            if (data.Email.IsStringNotNullOrEmpty())
            {
                user.Email = data.Email;
            }
            
            if(data.Password.IsStringNotNullOrEmpty() && data.NewPassword.IsStringNotNullOrEmpty() &&  BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
            }

            this.Context.SaveChanges();
        }
    }
}
