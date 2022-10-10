using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Validators;

namespace TestStore.Tests
{
    [TestClass]
    public class UpdateCategoryValidatorTests
    {

        private TestStoreDbContext _context;
        private UpdateCategoryValidator _validator;
        public UpdateCategoryValidatorTests()
        {
            this._context = new TestStoreDbContext();
            this._validator = new UpdateCategoryValidator(this._context);
        }
        [TestMethod]
        public void ValidateDto_ValidationSuccessfull()
        {
            var dto = new CategoryDto
            {
                Id = 1,
                Name = "Aca"
            };

            var result = this._validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidatonUnsuccessfullWhenNameNotPassed()
        {
            var dto = new CategoryDto
            {
                Id = 1
            };

            var result = this._validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
    }
}
