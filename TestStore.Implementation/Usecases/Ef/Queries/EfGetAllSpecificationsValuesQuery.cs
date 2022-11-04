using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;

namespace TestStore.Implementation.Usecases.Ef.Queries
{
    public class EfGetAllSpecificationsValuesQuery : EfBase, IGetAllSpecificationsValuesQuery
    {
        public EfGetAllSpecificationsValuesQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 52;

        public string Name => "EfGetAllSpecificationValuesQuery";

        public bool AdminOnly => true;

        public IEnumerable<SpecificationValueDto> Execute()
        {
            return this.Context.SpecificationsValues.Include(x => x.Specification).Where(x => x.Specification.IsActive).Select(x => new SpecificationValueDto
            {
                SpecificationId = x.SpecificationId,
                SpecificationName = x.Specification.Name,
                Value = x.Value
            }).ToList();
        }
    }
}
