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
    public class EfGetNavLinkQuery : EfBase, IGetNavLinkQuery
    {
        public EfGetNavLinkQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "EfGetNavLinkQuery";

        public bool AdminOnly => false;

        public NavLinkDto Execute(int data)
        {
            var navLink = this.Context.NavLinks.Find(data);
            if(navLink == null)
            {
                throw new EntityNotFoundException();
            }
            return new NavLinkDto
            {
                Id = navLink.Id,
                Name = navLink.Name,
                Action = navLink.Action
            };
        }
    }
}
