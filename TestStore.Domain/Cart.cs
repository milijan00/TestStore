using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class Cart : Entity
    {
        public int UserId { get; set; }
        public int CheckoutId { get; set; }
        public User User { get; set; }
        public Checkout Checkout { get; set; }

        public ICollection<CartProduct> Products { get; set; } = new List<CartProduct>();
    }
}
