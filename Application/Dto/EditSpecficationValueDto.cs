using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class EditSpecficationValueDto
    {
        public SpecificationValueDto Dto { get; set; }
        public IEnumerable<SpecificationDto> Specifications { get; set; }
    }
}
