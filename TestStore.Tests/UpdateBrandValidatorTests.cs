using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestStore.Application.Dto;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Validators;

namespace TestStore.Tests
{
    [TestClass]
    public class UpdateBrandValidatorTests
    {
        [TestMethod]
        public void ValidateDto_ValidationSuccessfull()
        {
            var context = new TestStoreDbContext();
            var validator = new UpdateBrandValidator(context);

            var dto = new BrandDto
            {
                Id = 1,
                Name = "Laki"
            };
            var result = validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidationUnsuccessfullWhenIdNotPassed()
        {
            var context = new TestStoreDbContext();
            var validator = new UpdateBrandValidator(context);

            var dto = new BrandDto
            {
                Name = "Laki"
            };
            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidationUnsuccessfullWhenNameNotPassed()
        {
            var context = new TestStoreDbContext();
            var validator = new UpdateBrandValidator(context);

            var dto = new BrandDto
            {
                Id = 1
            };
            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
    }
}