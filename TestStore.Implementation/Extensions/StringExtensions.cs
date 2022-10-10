using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Implementation.Extensions
{
    public static class StringExtensions
    {
        public static bool IsStringNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}
