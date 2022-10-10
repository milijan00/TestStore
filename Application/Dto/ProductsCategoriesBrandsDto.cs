using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class ProductsCategoriesBrandsDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<BrandDto> Brands { get; set; }
    }
}
