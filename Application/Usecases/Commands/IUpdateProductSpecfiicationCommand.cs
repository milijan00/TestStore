using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;

namespace TestStore.Application.Usecases.Commands
{
    public interface IUpdateProductSpecfiicationCommand : ICommand<ProductsSpecificationDto>
    {
    }
}
