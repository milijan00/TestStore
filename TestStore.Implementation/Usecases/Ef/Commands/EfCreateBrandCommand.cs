using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Domain;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfCreateBrandCommand : EfBase, ICreateBrandCommand
    {
        private CreateBrandValidator _validator;
        public EfCreateBrandCommand(TestStoreDbContext context, CreateBrandValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 28;

        public string Name => "EfCreateBrandCommand";

        public bool AdminOnly => true;

        public void Execute(BrandDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var brand = new Brand
            {
                Name = data.Name
            };
            this.Context.Brands.Add(brand);
            this.Context.SaveChanges();
        }
    }
}
