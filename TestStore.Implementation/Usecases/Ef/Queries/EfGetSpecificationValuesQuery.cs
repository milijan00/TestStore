using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;

namespace TestStore.Implementation.Usecases.Ef.Queries
{
    public class EfGetSpecificationValuesQuery : EfBase, IGetSpecificationValuesQuery
    {
        public EfGetSpecificationValuesQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 42;

        public string Name => "EfGetSpecificationValuesQuery";

        public bool AdminOnly => false;

        public IEnumerable<SpecificationValueDto> Execute(int data)
        {
            var specificationValues = this.Context.SpecificationsValues.Where(x => x.SpecificationId == data).ToList();
            if(specificationValues.Count == 0)
            {
                throw new EntityNotFoundException();
            }
            return specificationValues.Select(x => new SpecificationValueDto
            {
                SpecificationId = x.SpecificationId,
                Value = x.Value
            }).ToList();
        }
    }
}
