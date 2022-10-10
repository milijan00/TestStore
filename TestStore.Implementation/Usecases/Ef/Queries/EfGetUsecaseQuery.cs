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
    public class EfGetUsecaseQuery : EfBase, IGetUsecaseQuery
    {
        public EfGetUsecaseQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 7;

        public string Name => "EfGetUsecaseQuery";

        public bool AdminOnly => true;

        public UsecaseDto Execute(int data)
        {
            var usecase = this.Context.Usecases.Find(data);
            if(usecase == null)
            {
                throw new EntityNotFoundException();
            }
            return new UsecaseDto
            {
                Id = usecase.Id,
                Name = usecase.Name
            };
        }
    }
}
