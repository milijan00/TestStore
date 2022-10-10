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
    public class EfGetUsecasesQuery : EfBase, IGetUsecasesQuery
    {
        public EfGetUsecasesQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "EfGetUsecasesQuery";

        public bool AdminOnly => true;

        public IEnumerable<UsecaseDto> Execute()
        {
            return this.Context.Usecases.Where(x => x.IsActive).Select(x => new UsecaseDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
