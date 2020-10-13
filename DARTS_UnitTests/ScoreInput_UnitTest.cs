using DARTS.Data.DataObjects;
using DARTS.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.ViewModel;
using System.Globalization;
using DARTS;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace DARTS_UnitTests
{
    [TestClass]
    public class ScoreInput_UnitTest
    {
        private ThrowInputValidationRule validator = new ThrowInputValidationRule();

        [TestMethod]
        public void TestOnlyNumbers()
        {
            Assert.IsTrue(validator.Validate(1, null).IsValid);
            Assert.IsFalse(validator.Validate("test", null).IsValid);
        }
       
        [TestMethod]
        public void TestOnlyNumbers0to60()
        {
            Assert.IsTrue(validator.Validate(30, null).IsValid);
            Assert.IsTrue(validator.Validate(60, null).IsValid);
            Assert.IsFalse(validator.Validate(61, null).IsValid);
            Assert.IsFalse(validator.Validate(-1, null).IsValid);
        }
    }
}
