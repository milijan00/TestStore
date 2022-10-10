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
    public class EfGetCategoryQuery : EfBase, IGetCategoryQuery
    {
        public EfGetCategoryQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 23;

        public string Name => "EfGetCategoryQuery";

        public bool AdminOnly => true;

        public CategoryDto Execute(int data)
        {
            var category = this.Context.Categories.Find(data);
            if(category == null)
            {
                throw new EntityNotFoundException();
            }
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
