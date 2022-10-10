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
        private TestStoreDbContext _context;
        private UpdateBrandValidator _validator;
        public UpdateBrandValidatorTests()
        {
            this._context = new TestStoreDbContext();
            this._validator = new UpdateBrandValidator(this._context);
        }
        [TestMethod]
        public void ValidateDto_ValidationSuccessfull()
        {

            var dto = new BrandDto
            {
                Id = 1,
                Name = "Laki"
            };
            var result = this._validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidationUnsuccessfullWhenIdNotPassed()
        {

            var dto = new BrandDto
            {
                Name = "Laki"
            };
            var result = this._validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidationUnsuccessfullWhenNameNotPassed()
        {

            var dto = new BrandDto
            {
                Id = 1
            };
            var result = this._validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
    }
}