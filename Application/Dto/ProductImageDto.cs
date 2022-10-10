using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class ProductImageDto 
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
    }
}
