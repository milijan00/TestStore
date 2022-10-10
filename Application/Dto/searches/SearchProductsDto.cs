using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto.searches
{
    public class SearchProductsDto
    {
        public string Keyword { get; set; }
        public string SortValue { get; set; } = "Name-ASC";
        public IEnumerable<int> Categories { get; set; } = new List<int>();
        public IEnumerable<int> Brands { get; set; } = new List<int>();
    }
}
