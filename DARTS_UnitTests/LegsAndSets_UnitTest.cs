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
    class LegsAndSets_UnitTest
    {
        private LegsValidationRule legValidator = new LegsValidationRule();
        private SetsValidationRule setValidator = new SetsValidationRule(); 

        [TestMethod]
        public void Should_Only_Accept_Odd_Numbers_When_Int_Is_Inputed()
        {
            Assert.IsTrue(legValidator.Validate(1, null).IsValid);
            Assert.IsTrue(legValidator.Validate(4, null).IsValid);
            Assert.IsTrue(legValidator.Validate(23, null).IsValid);
            Assert.IsTrue(legValidator.Validate(8, null).IsValid);

            Assert.IsTrue(setValidator.Validate(9, null).IsValid);
            Assert.IsTrue(setValidator.Validate(67, null).IsValid);
            Assert.IsTrue(setValidator.Validate(44, null).IsValid);
            Assert.IsTrue(setValidator.Validate(12, null).IsValid);

        }
    }
}
