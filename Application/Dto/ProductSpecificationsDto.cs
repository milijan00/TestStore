using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class ProductSpecificationsDto 
    {
        public int? ProductId { get; set; }
        public IEnumerable<int> SpecificationsIds { get; set; }
    }

    public class DeleteProductSpecificationDto
    {
        public int? ProductId { get; set; }
        public int? SpecificationId { get; set; }
    }

}
