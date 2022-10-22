using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Implementation.Validators;

namespace TestStore.Tests
{
    [TestClass]
    public class UpdateSpecificationValueValidatorTests
    {
        private UpdateSpecificationValueValidator _validator;
        public UpdateSpecificationValueValidatorTests()
        {
            this._validator = new UpdateSpecificationValueValidator(new Implementation.DataAccess.TestStoreDbContext());
        }

        [TestMethod]
        public void ValidateDto_ValidationSuccessfull()
        {
            var dto = new SpecificationValueDto
            {
                SpecificationId = 1,
                Value = "Wireless",
                NewValue = "Wireless2"
            };
            var result = this._validator.Validate(dto);
            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void ValidateDto_ValidationUnsuccessfull()
        {
            var dto = new SpecificationValueDto
            {
                SpecificationId = 1,
                Value = "Wireless"
            };
            var result = this._validator.Validate(dto);
            Assert.IsFalse(result.IsValid);
        }
    }
}
