using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Dto.searches;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Extensions;

namespace TestStore.Implementation.Usecases.Ef.Queries
{
    public class EfGetProductsQuery : EfBase, IGetProductsQuery
    {
        public EfGetProductsQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => "EfGetProductsQuery";

        public bool AdminOnly => false;

        public IEnumerable<ProductDto> Execute(SearchProductsDto dto)
        {

            var products = Context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.Price).Where(x => x.IsActive).AsQueryable();

            if (dto.Keyword.IsStringNotNullOrEmpty())
            {
                products = products.Where(p => p.Name.Contains(dto.Keyword) || p.Category.Name.Contains(dto.Keyword) || p.Brand.Name.Contains(dto.Keyword));
            }

            if (dto.Categories.Count() > 0)
            {
                products = products.Where(p => dto.Categories.Contains(p.CategoryId));
            }
            
            if (dto.Brands.Count() > 0)
            {
                products = products.Where(p => dto.Brands.Contains(p.BrandId));
            }
            
            if(dto.SortValue == "Name-ASC")
            {
                products = products.OrderBy(x => x.Name);
            }else if(dto.SortValue == "Name-DESC")
            {
                products = products.OrderByDescending(x => x.Name);
            }else if(dto.SortValue == "Price-ASC")
            {
                products = products.OrderBy(x => x.Price);
            }else if(dto.SortValue == "Price-DESC")
            {
                products = products.OrderByDescending(x => x.Price);
            }

            return products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Category = x.Category.Name,
                Brand = x.Brand.Name,
                Price = x.Price.Value,
                ImageName = x.Image
            }).ToList();
        }
    }
}
