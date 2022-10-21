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
    public class BaseSpecificationValidatorTests
    { 
        private TestStoreDbContext _context;
        private BaseSpecificationValidator _validator;
        public BaseSpecificationValidatorTests()
        {
            this._context = new TestStoreDbContext();
            this._validator = new BaseSpecificationValidator(this._context);
        }
   
        [TestMethod]
        public void ValidateDto_ValidationSuccessful()
        {
            var dto = new SpecificationDto
            {
                Name = "New specification"
            };
            var result = this._validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ValidateDto_ValidationUnsuccessfull()
        {
            var dto = new SpecificationDto
            {
                Name = "Backlight"
            };
            var result = this._validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
        }
    }
}
