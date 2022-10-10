using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class ChartProduct
    {
        public Chart Chart { get; set; }
        public Product Product { get; set; }
        public int ChartId { get; set; }
        public int ProductId { get; set; }
    }
}
