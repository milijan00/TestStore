using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class Checkout 
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }
       public string  Adress { get; set; }
    }
}
