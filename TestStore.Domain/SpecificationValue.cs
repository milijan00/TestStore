using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class SpecificationValue 
    {
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
        public string Value { get; set; }
    }
}
