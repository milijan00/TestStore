using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfUpdateCategoryCommand : EfBase, IUpdateCategoryCommand
    {
        private UpdateCategoryValidator _validator;
        public EfUpdateCategoryCommand(TestStoreDbContext context, UpdateCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 25;

        public string Name => "EFUpdateCategoryCommand";

        public bool AdminOnly => true;

        public void Execute(CategoryDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }

            var category = this.Context.Categories.Find(data.Id.Value);
            category.Name = data.Name;
            this.Context.SaveChanges();
        }
    }
}
