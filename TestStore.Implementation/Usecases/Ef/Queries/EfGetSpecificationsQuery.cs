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
    public class EfGetSpecificationsQuery : EfBase, IGetSpecificationsQuery
    {
        public EfGetSpecificationsQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 37;

        public string Name => "EfGetSpecificationsQuery";

        public bool AdminOnly => false;

        public IEnumerable<SpecificationDto> Execute()
        {
            return this.Context.Specifications.Where(x => x.IsActive).Select(x => new SpecificationDto { 
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
