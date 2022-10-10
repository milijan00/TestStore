using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Dto.searches;

namespace TestStore.Application.Usecases.Queries
{
    public interface IGetProductsQuery  : IQuery<SearchProductsDto,IEnumerable<ProductDto>>
    {
    }
}
