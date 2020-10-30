using DARTS.Data.DataBase;
using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataObjects
{
    [TestClass]
    public class Set_UnitTest
    {
        [TestMethod]
        public void Set_Id_Should_Not_Be_Null()
        {
            // Arrange.
            SetFactory setFactory = new SetFactory();
            Set set = (Set)setFactory.Spawn();

            // Act.
            set.Post();

            // Assert.
            Assert.IsNotNull(set.Id);
        }

        [TestMethod]
        public void Set_MatchId_Should_Not_Be_Null()
        {
            // Arrange.
            SetFactory setFactory = new SetFactory();
            Set set = (Set)setFactory.Spawn();

            MatchFactory matchFactory = new MatchFactory();
            Match match = (Match)matchFactory.Spawn();
            match.Sets.Add(set);

            // Act.
            match.Post();

            // Assert.
            Assert.IsNotNull(set.MatchId);
        }

        [TestMethod]
        public void Reading_Set_Properties_Should_Not_Cause_Exceptions()
        {
            SetFactory setFactory = new SetFactory();
            Set set = (Set)setFactory.Spawn();
            set.WinningPlayer = PlayerEnum.Player1;
            set.BeginningPlayer = PlayerEnum.Player1;
            set.SetState = PlayState.InProgress;
            set.NumLegs = 1;
            set.Player1LegsWon = 0;
            set.Player2LegsWon = 0;

            set.Post();
            set = (Set)setFactory.Get(set.Id);

            Assert.IsNotNull(set.WinningPlayer);
            Assert.IsNotNull(set.BeginningPlayer);
            Assert.IsNotNull(set.SetState);
            Assert.IsNotNull(set.NumLegs);
            Assert.IsNotNull(set.Player1LegsWon);
            Assert.IsNotNull(set.Player2LegsWon);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            DataBaseProvider.Instance.Dispose();
        }
    }
}
