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
    public class EfUpdateNavLinkCommand : EfBase, IUpdateNavLinkCommand
    {
        private UpdateNavLinkValidator _validator;
        public EfUpdateNavLinkCommand(TestStoreDbContext context, UpdateNavLinkValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "EFUpdateNavLinkCommand";

        public bool AdminOnly => true;

        public void Execute(NavLinkDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            
            var navLink = this.Context.NavLinks.Find(data.Id);
            if (!string.IsNullOrEmpty(data.Name))
            {
                navLink.Name = data.Name;
            }
            if (!string.IsNullOrEmpty(data.Action))
            {
                navLink.Action = data.Action;
            }
            if (!string.IsNullOrEmpty(data.Controller))
            {
                navLink.Controller = data.Controller;
            }
            this.Context.SaveChanges();
        }
    }
}
