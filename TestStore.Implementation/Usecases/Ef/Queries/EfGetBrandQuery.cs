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
    public class EfGetBrandQuery : EfBase, IGetBrandQuery
    {
        public EfGetBrandQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 27;

        public string Name => "EFGetBrandQuery";

        public bool AdminOnly => true;

        public BrandDto Execute(int data)
        {
            var brand = this.Context.Brands.Find(data);
            if(brand == null)
            {
                throw new EntityNotFoundException();
            }

            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }
    }
}
