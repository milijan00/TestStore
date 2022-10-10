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
    public class EfGetNavLinksQuery : EfBase, IGetNavLinksQuery
    {
        public EfGetNavLinksQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "EfGetNavLinksQuery";

        public bool AdminOnly => false;

        public IEnumerable<NavLinkDto> Execute()
        {
            return this.Context.NavLinks.Where(x => x.IsActive).Select(x => new NavLinkDto
            {
                Id = x.Id,
                Name = x.Name,
                Action = x.Action
            }).ToList();
        }
    }
}
