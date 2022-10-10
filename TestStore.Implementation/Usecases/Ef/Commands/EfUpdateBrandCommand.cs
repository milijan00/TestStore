using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfUpdateBrandCommand : EfBase, IUpdateBrandCommand
    {
        private UpdateBrandValidator _validator;
        public EfUpdateBrandCommand(TestStoreDbContext context, UpdateBrandValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 29;

        public string Name => "EfUpdateBrandCommand";

        public bool AdminOnly => true;

        public void Execute(BrandDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }

            var brand = this.Context.Brands.Find(data.Id.Value);
            brand.Name = data.Name;
            this.Context.SaveChanges();
        }
    }
}
