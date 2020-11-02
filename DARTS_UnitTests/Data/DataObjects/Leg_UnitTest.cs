using DARTS.Data.DataBase;
using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DARTS_UnitTests.Data.DataObjects
{
    [TestClass]
    public class Leg_UnitTest
    {
        [TestMethod]
        public void Reading_Leg_Properties_Should_Not_Cause_Exceptions()
        {
            LegFactory legFactory = new LegFactory();
            Leg leg = (Leg)legFactory.Spawn();
            leg.WinningPlayer = PlayerEnum.Player1;
            leg.BeginningPlayer = PlayerEnum.Player1;
            leg.LegState = PlayState.InProgress;
            leg.Player1LegScore = 1;
            leg.Player2LegScore = 0;

            leg.Post();
            leg = (Leg)legFactory.Get(leg.Id);

            Assert.IsNotNull(leg.WinningPlayer);
            Assert.IsNotNull(leg.BeginningPlayer);
            Assert.IsNotNull(leg.LegState);
            Assert.IsNotNull(leg.Player1LegScore);
            Assert.IsNotNull(leg.Player2LegScore);
        }

        [TestMethod]
        public void Leg_Delete_Should_Delete_All_Nested_Components()
        {
            //ARRANGE
            LegFactory legFactory = new LegFactory();
            Leg leg = (Leg)legFactory.Spawn();
            leg.Start();
            leg.Post();

            //ACT
            leg.Delete();

            //ASSERT
            Assert.AreEqual(ObjectState.Deleted, leg.ObjectState);
            Assert.IsNull(legFactory.Get(leg.Id));
            foreach (Turn turn in leg.Turns)
            {
                Assert.AreEqual(ObjectState.Deleted, turn.ObjectState);
                Assert.IsNull(new TurnFactory().Get(turn.Id));
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DataBaseProvider.Instance.Dispose();
        }
    }
}
