using DARTS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.IsFalse(legValidator.Validate(4, null).IsValid);
            Assert.IsTrue(legValidator.Validate(23, null).IsValid);
            Assert.IsFalse(legValidator.Validate(8, null).IsValid);

            Assert.IsTrue(setValidator.Validate(9, null).IsValid);
            Assert.IsTrue(setValidator.Validate(67, null).IsValid);
            Assert.IsFalse(setValidator.Validate(44, null).IsValid);
            Assert.IsFalse(setValidator.Validate(12, null).IsValid);
        }
    }
}
