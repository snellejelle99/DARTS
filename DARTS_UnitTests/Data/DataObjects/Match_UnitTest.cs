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
            player1.PlayerType = PlayerEnum.Player1;
            player1.Name = "Klaas";

            Player player2 = new Player();
            player2.PlayerType = PlayerEnum.Player2;
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

        [TestMethod]
        public void ChooseRandomPlayer()
        {
            // Arrange
            List<Match> matches = new List<Match>();
            List<PlayerEnum> randomPlayers = new List<PlayerEnum>();

            for (int i = 0; i < 20; i++)
            {
                Player player1 = new Player();
                player1.PlayerType = PlayerEnum.Player1;
                player1.Name = "Klaas";

                Player player2 = new Player();
                player2.PlayerType = PlayerEnum.Player2;
                player2.Name = "Pieter";

                int numSets = 22;
                int numLegs = 3;

                PlayerEnum beginningPlayer = PlayerEnum.None;

                Match newMatch = new Match();
                newMatch.Player1 = player1;
                newMatch.Player2 = player2;
                newMatch.NumSets = numSets;
                newMatch.NumLegs = numLegs;
                newMatch.BeginningPlayer = beginningPlayer;

                matches.Add(newMatch);
            }

            // Act
            foreach (Match match in matches)
            {
                match.Start();
                randomPlayers.Add(match.BeginningPlayer);
            }

            // Assert
            CollectionAssert.DoesNotContain(randomPlayers, PlayerEnum.None, "List randomPlayers contained PlayerEnum.None entry.");
            CollectionAssert.Contains(randomPlayers, PlayerEnum.Player1, "List randomPlayers did't contain a PlayerEnum.Player1 entry, the player wasn't chosen randomly.");
            CollectionAssert.Contains(randomPlayers, PlayerEnum.Player2, "List randomPlayers did't contain a PlayerEnum.Player2 entry, the player wasn't chosen randomly.");
        }

        [TestMethod]
        public void StartMatch()
        {
            // Arrange
            Player player1 = new Player();
            player1.PlayerType = PlayerEnum.Player1;
            player1.Name = "Klaas";

            Player player2 = new Player();
            player2.PlayerType = PlayerEnum.Player2;
            player2.Name = "Pieter";

            int numSets = 22;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            Match newMatch = new Match();
            newMatch.Player1 = player1;
            newMatch.Player2 = player2;
            newMatch.NumSets = numSets;
            newMatch.NumLegs = numLegs;
            newMatch.BeginningPlayer = beginningPlayer;

            // Act
            newMatch.Start();

            // Assert
            Assert.AreEqual(newMatch.Sets[0].Legs[0].Turns[0].PlayerTurn, beginningPlayer, "The given PlayerEnum isn't equal to the PlayerEnum in the first turn object.");
        }
    }
}
