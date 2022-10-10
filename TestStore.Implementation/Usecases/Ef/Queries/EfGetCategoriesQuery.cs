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
    public class EfGetCategoriesQuery : EfBase, IGetCategoriesQuery
    {
        public EfGetCategoriesQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "EfGetCategoriesQuery"; 

        public bool AdminOnly => false;

        public IEnumerable<CategoryDto> Execute()
        {
            return this.Context.Categories.Where(x => x.IsActive).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
