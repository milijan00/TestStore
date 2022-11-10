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
    public class EfGetProductQuery : EfBase, IGetProductQuery
    {
        public EfGetProductQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 17;

        public string Name => "EfGetProductQuery";

        public bool AdminOnly => false;

        public ProductDto Execute(int data)
        {
            var product = this.Context.Products.Where(x => x.IsActive)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Price)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification).ThenInclude(x => x.SpecificationValues)
                .FirstOrDefault(x => x.Id == data);
            if(product == null)
            {
                throw new EntityNotFoundException();
            }

            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Brand = product.Brand.Name,
                Category = product.Category.Name,
                Price = product.Price.Value,
                ImageName = product.Image,
                Specifications = product.Specifications.Select(x => new ProductsSpecificationDto
                {
                    SpecificationId = x.SpecificationId,
                    Name = x.Specification.Name,
                    Value = x.Specification.SpecificationValues.First().Value
                })
            };
        }
    }
}
