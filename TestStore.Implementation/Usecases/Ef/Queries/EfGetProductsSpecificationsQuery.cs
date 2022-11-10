using Microsoft.EntityFrameworkCore;
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
    public class EfGetProductsSpecificationsQuery : EfBase, IGetProductsSpecificationsQuery
    {
        public EfGetProductsSpecificationsQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 54;

        public string Name => "EfGetProductsSpecificationsQuery";

        public bool AdminOnly => true;

        public IEnumerable<ProductsSpecificationDto> Execute(int data)
        {
            var product = this.Context.Products
                .Include(x => x.Specifications).ThenInclude(x => x.Specification).ThenInclude(x => x.SpecificationValues)
                .FirstOrDefault(x => x.Id == data);
            if(product == null)
            {
                throw new EntityNotFoundException();
            }

            return product.Specifications.Select(x => new ProductsSpecificationDto
            {
                SpecificationId = x.SpecificationId,
                Name = x.Specification.Name,
                Value = x.Specification.SpecificationValues.First().Value
            });
        }
    }
}
