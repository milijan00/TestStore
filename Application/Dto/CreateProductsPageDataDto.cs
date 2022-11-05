using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class CreateProductsPageDataDto
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<BrandDto> Brands { get; set; }
    }
    public class UpdateProductsPageDataDto: CreateProductsPageDataDto
    {
        public ProductDto Product { get; set; }
    }
}
