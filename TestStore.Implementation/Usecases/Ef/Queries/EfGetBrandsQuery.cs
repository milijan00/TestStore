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
    public class EfGetBrandsQuery : EfBase, IGetBrandsQuery
    {
        public EfGetBrandsQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 22;

        public string Name => "EfGetBrandsQuery";

        public bool AdminOnly => false;

        public IEnumerable<BrandDto> Execute()
        {
            return this.Context.Brands.Where(x => x.IsActive).Select(x => new BrandDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
