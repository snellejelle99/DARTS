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
            string player1 = "Klaas";
            string player2 = "Pieter";

            int numSets = 22;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            // Act
            Match newMatch = new Match(player1, player2, numSets, numLegs, beginningPlayer);

            // Assert
            Assert.IsNotNull(newMatch);
            Assert.AreEqual(numSets, newMatch.Sets.Count, "The number of sets created in the Match aren't equal to the given numer of sets.");
        }
    }
}
