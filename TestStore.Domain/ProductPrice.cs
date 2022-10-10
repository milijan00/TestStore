﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Domain
{
    public class ProductPrice 
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
