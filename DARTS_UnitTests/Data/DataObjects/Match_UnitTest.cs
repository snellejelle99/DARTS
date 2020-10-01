using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Datastructuur
{
    [TestClass]
    public class Match_UnitTest
    {
        private Match match;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            Player player1 = new Player();
            player1.Name = "Klaas";

            Player player2 = new Player();
            player2.Name = "Pieter";

            int numSets = 11;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            Match match = new Match();
            match.Player1 = player1;
            match.Player2 = player2;
            match.NumSets = numSets;
            match.NumLegs = numLegs;
            match.BeginningPlayer = beginningPlayer;

            this.match = match;
        }

        [TestMethod]
        public void CreateMatchTest()
        {
            // Arrange
            int numSets = 13;
            match.NumSets = numSets;

            // Assert
            Assert.IsNotNull(match);
            Assert.AreEqual(numSets, match.NumSets, "The number of sets created in the Match aren't equal to the given numer of sets.");
        }

        [TestMethod]
        public void ChooseRandomPlayer()
        {
            // Arrange
            match.BeginningPlayer = PlayerEnum.None;

            // Act
            match.Start();

            // Assert
            Assert.AreNotEqual(match.BeginningPlayer, PlayerEnum.None, "BeginningPlayer was PlayerEnum.None.");
            Assert.IsTrue(match.BeginningPlayer == PlayerEnum.Player1 || match.BeginningPlayer == PlayerEnum.Player2, "BeginningPlayer was not Player1 or Player2.");
        }

        [TestMethod]
        public void StartMatch()
        {
            // Arrange
            PlayerEnum beginningPlayer = PlayerEnum.Player2;
            match.BeginningPlayer = beginningPlayer;

            // Act
            match.Start();

            // Assert
            Assert.AreEqual(match.Sets[0].Legs[0].Turns[0].PlayerTurn, beginningPlayer, "The given PlayerEnum isn't equal to the PlayerEnum in the first turn object.");
        }

        [TestMethod()]
        public void CheckWinTest()
        {
            // Arrange
            match.Start();
            match.Sets[0].Legs[0].Player1LegScore = 0;


            // Act
            match.ChangeTurn();

            // Assert
            Assert.AreEqual(PlayerEnum.Player1, match.Sets[0].Legs[0].WinningPlayer, "Expected Player 1 to win, but this was not the case.");
        }


        [TestMethod()]
        public void CheckNewTurnCreation()
        {
            //Arrange
            match.Start();

            //Act
            match.ChangeTurn();
          
            //Assert
            Assert.AreEqual(match.Sets[0].Legs[0].Turns.Count, 2, "New Turn was not created.");
            Assert.AreEqual(match.Sets[0].Legs[0].Turns[1].PlayerTurn, PlayerEnum.Player2, "It should be Player 2's turn now but it is not.");
        }

        [TestMethod()]
        public void CheckNewLegCreation()
        {
            //Arrange
            match.Start();
            match.Sets[0].Legs[0].Player1LegScore = 0;

            //Act
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(match.Sets[0].Legs.Count, 2, "New Leg was not created.");
            Assert.AreEqual(PlayerEnum.Player1, match.Sets[0].Legs[0].WinningPlayer, "Expected Player 1 to be the winner but this is not the case.");
            Assert.AreEqual(match.Sets[0].Legs[1].BeginningPlayer, PlayerEnum.Player2, "Expected Player2 to be the BeginningPlayer for this leg, but this was not the case.");
        }

        [TestMethod()]
        public void CheckNewSetCreation()
        {
            //Arrange
            match.Start();
            match.Sets[0].Legs[0].Player1LegScore = 0;
            match.Sets[0].Player1LegsWon = 10;
           
            //Act
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(match.Sets.Count, 2, "New Set was not created.");
            Assert.AreEqual(match.Sets[0].WinningPlayer, PlayerEnum.Player1, "Expected Player1 to win the Set but this was not the case.");
            Assert.AreEqual(match.Sets[1].BeginningPlayer, PlayerEnum.Player2, "Expected Player2 to be the BeginningPlayer for this set, but this was not the case.");
        }
    }
}
