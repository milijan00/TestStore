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
    public class UpdateUsecaseValidatorTests
    {
        private TestStoreDbContext _context;
        private UpdateUsecaseValidator _validator;
        public UpdateUsecaseValidatorTests()
        {
            this._context = new TestStoreDbContext();
            this._validator = new UpdateUsecaseValidator(this._context);
        }
        [TestMethod]
        public void ValidateDto_ValidationSuccessfull()
        {
            var dto = new UsecaseDto
            {
                Id = 1,
                Name = "Efasdasd"
            };

            var result = this._validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidationSuccessfullWhenIdNotPassed()
        {
            var dto = new UsecaseDto
            {
                Name = "Efasdasd"
            };

            var result = this._validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
    }
}
