using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DARTS.Functionality;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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
            match.MatchState = PlayState.InProgress;

            this.match = match;
        }

        [TestMethod]
        public void Should_Choose_Random_Player_On_Match_Start()
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
        public void Should_Start_Match_With_Given_Player()
        {
            // Arrange
            PlayerEnum beginningPlayer = PlayerEnum.Player2;
            match.BeginningPlayer = beginningPlayer;

            // Act
            match.Start();

            // Assert
            Assert.AreEqual(match.Sets[0].Legs[0].Turns[0].PlayerTurn, beginningPlayer, "The given PlayerEnum isn't equal to the PlayerEnum in the first turn object.");
        }

        [TestMethod]
        public void Should_Win_Leg_When_LegScore_Is_Zero()
        {
            // Arrange
            match.Start();
            match.Sets[0].Legs[0].Player1LegScore = 0;

            // Act
            match.ChangeTurn();

            // Assert
            Assert.AreEqual(PlayerEnum.Player1, match.Sets[0].Legs[0].WinningPlayer, "Expected Player 1 to win, but this was not the case.");
        }

        [TestMethod]
        public void Should_Create_New_Turn_On_ChangeTurn_Call()
        {
            //Arrange
            match.Start();

            //Act
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(match.Sets[0].Legs[0].Turns.Count, 2, "New Turn was not created.");
            Assert.AreEqual(match.Sets[0].Legs[0].Turns[1].PlayerTurn, PlayerEnum.Player2, "It should be Player 2's turn now but it is not.");
        }

        [TestMethod]
        public void Should_Create_New_Leg_On_Leg_Win()
        {
            //Arrange
            match.Start();
            match.Sets[0].Legs[0].Player1LegScore = 0;

            //Act
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(match.Sets[0].Legs.Count, 2, "New Leg was not created.");
            Assert.AreEqual(match.Sets[0].Legs[1].BeginningPlayer, PlayerEnum.Player2, "Expected Player2 to be the BeginningPlayer for this leg, but this was not the case.");
        }

        [TestMethod]
        public void Should_Create_New_Set_On_Set_Win()
        {
            //Arrange
            match.Start();
            match.Sets[0].Legs[0].Player1LegScore = 0;
            match.Sets[0].Player1LegsWon = 2;

            //Act
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(match.Sets.Count, 2, "New Set was not created.");
            Assert.AreEqual(match.Sets[0].WinningPlayer, PlayerEnum.Player1, "Expected Player1 to win the Set but this was not the case.");
            Assert.AreEqual(match.Sets[1].BeginningPlayer, PlayerEnum.Player2, "Expected Player2 to be the BeginningPlayer for this set, but this was not the case.");
        }

        [TestMethod]
        [DataRow(ScoreType.Miss, 0, 0)]
        [DataRow(ScoreType.Single, 10, 10)]
        [DataRow(ScoreType.Double, 10, 20)]
        [DataRow(ScoreType.Triple, 10, 30)]
        [DataRow(ScoreType.Bull, 25, 25)]
        [DataRow(ScoreType.Bullseye, 50, 50)]
        public void Should_Subtract_Proper_Value_From_Leg_Score(ScoreType scoreType, int throwScore, int subtractScore)
        {
            // Arrange
            match.Start();
            uint StartingLegScore = match.GetCurrentLeg().Player1LegScore;
            match.GetCurrentTurn().Throws.Add(ProcessThrow.CalculateThrowScore(throwScore, scoreType));

            //Act
            match.GetCurrentLeg().SubtractScore();

            //Assert
            Assert.AreEqual(match.GetCurrentLeg().Player1LegScore, StartingLegScore - subtractScore);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(21)]
        [DataRow(26)]
        [DataRow(51)]
        public void CalculateThrowScore_Should_Throw_Exception_When_ThrowScore_Is_Out_Of_Allowed_Range(int throwScore)
        {
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=>ProcessThrow.CalculateThrowScore(throwScore, ScoreType.Single));
        }

        [TestMethod]
        [DataRow(20, ScoreType.Miss)]
        [DataRow(25, ScoreType.Single)]
        [DataRow(25, ScoreType.Double)]
        [DataRow(25, ScoreType.Triple)]
        [DataRow(20, ScoreType.Bull)]
        [DataRow(20, ScoreType.Bullseye)]
        public void CalculateThrowScore_Should_Throw_Exception_When_ThrowScore_Does_Not_Match_ScoreType(int throwScore, ScoreType scoreType)
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => ProcessThrow.CalculateThrowScore(throwScore, scoreType));
        }

        [TestMethod]
        public void Should_Set_Score_To_0_When_Score_Is_Double()
        {
            // Arrange
            match.Start();
            uint StartingLegScore = match.GetCurrentLeg().Player1LegScore = 36;
            int ThrowScore = 18;
            match.GetCurrentTurn().Throws.Add(ProcessThrow.CalculateThrowScore(ThrowScore, ScoreType.Double));

            //Act
            match.GetCurrentLeg().SubtractScore();

            //Assert
            Assert.AreEqual(match.GetCurrentLeg().Player1LegScore, StartingLegScore - (ThrowScore * 2));
        }

        [TestMethod]
        public void Should_Not_Set_Score_To_0_When_Score_Is_Not_Double()
        {
            // Arrange
            match.Start();
            uint StartingLegScore = match.GetCurrentLeg().Player1LegScore = 20;
            int ThrowScore = 20;
            match.GetCurrentTurn().Throws.Add(ProcessThrow.CalculateThrowScore(ThrowScore, ScoreType.Single));

            //Act
            match.GetCurrentLeg().SubtractScore();

            //Assert
            Assert.AreEqual(match.GetCurrentLeg().Player1LegScore, StartingLegScore);
        }

        [TestMethod]
        public void Should_Set_Score_To_Score_Before_Throw_When_Throw_Exceeds_LegScore()
        {
            // Arrange
            match.Start();
            uint StartingLegScore = match.GetCurrentLeg().Player1LegScore = 35;
            int ThrowScore = 18;
            match.GetCurrentTurn().Throws.Add(ProcessThrow.CalculateThrowScore(ThrowScore, ScoreType.Double));

            //Act            
            match.GetCurrentLeg().SubtractScore();

            //Assert
            Assert.AreEqual(match.GetCurrentLeg().Player1LegScore, StartingLegScore);
        }
    }
}
