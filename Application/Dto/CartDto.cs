using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class CartDto
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string Adress { get; set; }

        public IEnumerable<CartProductDto> Products { get; set; }
    }
    public class CartProductDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
