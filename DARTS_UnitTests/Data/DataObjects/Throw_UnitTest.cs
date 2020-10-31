using DARTS.Data.DataBase;
using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DARTS_UnitTests.Data.DataObjects
{
    [TestClass]
    public class Throw_UnitTest
    {
        [TestMethod]
        public void Reading_Throw_Properties_Should_Not_Cause_Exceptions()
        {
            ThrowFactory throwFactory = new ThrowFactory();
            Throw dartThrow = (Throw)throwFactory.Spawn();
            dartThrow.Score = 2;
            dartThrow.ScoreType = ScoreType.Single;

            dartThrow.Post();
            dartThrow = (Throw)throwFactory.Get(dartThrow.Id);

            Assert.IsNotNull(dartThrow.Score);
            Assert.IsNotNull(dartThrow.ScoreType);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            DataBaseProvider.Instance.Dispose();
        }
    }
}
