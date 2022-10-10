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

        [TestMethod]
        public void ValidateDto_ValidationSuccessfull()
        {
            var context = new TestStoreDbContext();
            var validator = new UpdateCategoryValidator(context);
            var dto = new CategoryDto
            {
                Id = 1,
                Name = "Aca"
            };

            var result = validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidatonUnsuccessfullWhenNameNotPassed()
        {
            var context = new TestStoreDbContext();
            var validator = new UpdateCategoryValidator(context);
            var dto = new CategoryDto
            {
                Id = 1
            };

            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
    }
}
