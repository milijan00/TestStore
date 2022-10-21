using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int? CategoryId { get; set; }
        public string Brand { get; set; }
        public int? BrandId { get; set; }
        public decimal? Price { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<ProductsSpecificationDto> Specifications { get; set; }
    }

    public class ProductsSpecificationDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class ProductWithImageDto : ProductDto
    {
        public IFormFile Image { get; set; }
    }
}
