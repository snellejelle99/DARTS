using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataObjects
{
    [TestClass]
    public class Turn_UnitTest
    {
        [TestMethod]
        public void Turn_Id_Should_Not_Be_Null()
        {
            // Arrange.
            TurnFactory turnFactory = new TurnFactory();
            Turn turn = (Turn)turnFactory.Spawn();

            // Act.
            turn.Post();

            // Assert.
            Assert.IsNotNull(turn.Id);
        }

        [TestMethod]
        public void Turn_LegId_Should_Not_Be_Null()
        {
            // Arrange.
            TurnFactory turnFactory = new TurnFactory();
            Turn turn = (Turn)turnFactory.Spawn();

            LegFactory legFactory = new LegFactory();
            Leg leg = (Leg)legFactory.Spawn();
            leg.Turns.Add(turn);

            // Act.
            leg.Post();

            // Assert.
            Assert.IsNotNull(turn.LegId);
        }

        [TestMethod]
        public void Reading_Turn_Properties_Should_Not_Cause_Exceptions()
        {
            TurnFactory turnFactory = new TurnFactory();
            Turn turn = (Turn)turnFactory.Spawn();
            turn.PlayerTurn = PlayerEnum.Player1;

            turn.Post();
            turn = (Turn)turnFactory.Get(turn.Id);

            Assert.IsNotNull(turn.PlayerTurn);
        }
    }
}
