using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Domain;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfCreateNavLinkCommand : EfBase, Application.Usecases.Commands.ICreateNavLinkCommand
    {
        private CreateNavLinkValidator _validator;
        public EfCreateNavLinkCommand(TestStoreDbContext context, CreateNavLinkValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "EfCreateNavLinkCommand";

        public bool AdminOnly => true;

        public void Execute(NavLinkDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var navLink = new NavLink
            {
                Name = data.Name,
                Action = data.Action,
                Controller = data.Controller,
            };
            this.Context.NavLinks.Add(navLink);
            this.Context.SaveChanges();
        }
    }
}
