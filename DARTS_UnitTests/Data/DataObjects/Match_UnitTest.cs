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
            string player1 = "Klaas";
            string player2 = "Pieter";

            int numSets = 22;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            Match newMatch = new Match(player1, player2, numSets, numLegs, beginningPlayer);

            Assert.IsNotNull(newMatch);
            Assert.IsTrue(newMatch.Sets.Count.Equals(numSets));
        }
    }
}
