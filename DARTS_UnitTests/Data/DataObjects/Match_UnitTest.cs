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
        [TestMethod]
        public void CreateMatchTest()
        {
            // Arrange
            Player player1 = new Player();
            player1.Name = "Klaas";

            Player player2 = new Player();
            player2.Name = "Pieter";

            int numSets = 22;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            // Act
            Match newMatch = new Match();
            newMatch.Player1 = player1;
            newMatch.Player2 = player2;
            newMatch.NumSets = numSets;
            newMatch.NumLegs = numLegs;
            newMatch.BeginningPlayer = beginningPlayer;

            // Assert
            Assert.IsNotNull(newMatch);
            Assert.AreEqual(numSets, newMatch.NumSets, "The number of sets created in the Match aren't equal to the given numer of sets.");
        }
    }
}
