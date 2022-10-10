using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Implementation.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public object Errors { get; set; }
        public UnprocessableEntityException(object obj) 
        {
            this.Errors = obj;
        }
    }
}
