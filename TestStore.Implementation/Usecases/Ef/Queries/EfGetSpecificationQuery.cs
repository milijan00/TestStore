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
    public class EfGetSpecificationQuery : EfBase, IGetSpecificationQuery
    {
        public EfGetSpecificationQuery(TestStoreDbContext context) : base(context)
        {
        }

        public int Id => 38;

        public string Name => "EfGetSpecificationQuery";

        public bool AdminOnly => false;

        public SpecificationDto Execute(int data)
        {
            var specification = this.Context.Specifications.Find(data);
            if(specification == null)
            {
                throw new EntityNotFoundException();
            }

            return new SpecificationDto
            {
                Id = specification.Id,
                Name = specification.Name
            };
        }
    }
}
