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
    public class SpecificationValueValidatorTests
    {
        private TestStoreDbContext _context;
        private SpecificationValueValidator _validator;
        public SpecificationValueValidatorTests()
        {
            _context = new TestStoreDbContext();
            this._validator = new SpecificationValueValidator(_context);
        }

        [TestMethod]
        public void ValidateDto_ValidationSuccessfull()
        {
            var dto = new SpecificationValueDto
            {
                SpecificationId = 1,
                Value = "Nesto novo"
            };
            var result = this._validator.Validate(dto);
            // assert
            Assert.IsTrue(result.IsValid);

        }
        [TestMethod]
        public void ValidateDto_ValidationUnsuccessfull()
        {
            var dto = new SpecificationValueDto
            {
                Value = "Nesto novo"
            };
            var result = this._validator.Validate(dto);
            // assert
            Assert.IsFalse(result.IsValid);

        }
    }
}
