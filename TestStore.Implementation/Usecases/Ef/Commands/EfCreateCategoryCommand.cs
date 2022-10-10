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
    public class EfCreateCategoryCommand : EfBase, ICreateCategoryCommand
    {
        private CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(TestStoreDbContext context, CreateCategoryValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 24;

        public string Name => "EfCreateCategoryCommand";

        public bool AdminOnly => true;

        public void Execute(CategoryDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }

            var category = new Category
            {
                Name = data.Name
            };
            this.Context.Categories.Add(category);
            this.Context.SaveChanges();
        }
    }
}
