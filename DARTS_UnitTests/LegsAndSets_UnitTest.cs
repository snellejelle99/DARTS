using DARTS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DARTS_UnitTests
{
    [TestClass]
    public class LegsAndSets_UnitTest
    {
        private readonly LegsValidationRule legValidator = new LegsValidationRule();
        private readonly SetsValidationRule setValidator = new SetsValidationRule();

        [TestMethod]
        public void Legs_Should_Only_Accept_Odd_Numbers_When_Value_Is_Entered()
        {
            Assert.IsTrue(legValidator.Validate(1, null).IsValid);
            Assert.IsFalse(legValidator.Validate(4, null).IsValid);
            Assert.IsTrue(legValidator.Validate(23, null).IsValid);
            Assert.IsFalse(legValidator.Validate(8, null).IsValid);
        }

        [TestMethod]
        public void Sets_Should_Only_Accept_Odd_Numbers_When_Value_Is_Entered()
        {
            Assert.IsTrue(setValidator.Validate(9, null).IsValid);
            Assert.IsTrue(setValidator.Validate(67, null).IsValid);
            Assert.IsFalse(setValidator.Validate(44, null).IsValid);
            Assert.IsFalse(setValidator.Validate(12, null).IsValid);
        }
    }
}
