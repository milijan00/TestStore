using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfDeleteNavLinkCommand : EfBase, IDeleteNavLinkCommand
    {
        public EfDeleteNavLinkCommand(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "EfDeleteNavLinkCommand";

        public bool AdminOnly => true;

        public void Execute(int data)
        {
            var navLink = this.Context.NavLinks.Find(data);
            if(navLink == null)
            {
                throw new EntityNotFoundException();
            }

            this.Context.NavLinks.Remove(navLink);
            this.Context.SaveChanges(); 
        }
    }
}
