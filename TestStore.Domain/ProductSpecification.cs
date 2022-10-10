using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class ProductSpecification 
    {
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }
        public Product Product { get; set; }
        public Specification Specification { get; set; }
    }
}
