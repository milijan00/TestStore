using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class Specification : Entity
    {
        public string Name { get; set; }
        public ICollection<SpecificationValue> SpecificationValues { get; set; } = new List<SpecificationValue>();
        public ICollection<ProductSpecification> Products { get; set; } = new List<ProductSpecification>();
    }
}
