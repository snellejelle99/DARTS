using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DARTS.Functionality;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using DARTS.Data.DataObjectFactories;
using DARTS_UnitTests.ViewModel;
using DARTS.Data.DataBase;

namespace DARTS_UnitTests.Data.DataObjects
{
    [TestClass]
    public class Match_UnitTest
    {
        private Match match;
        private PlayerFactory playerFactory;
        private MatchFactory matchFactory;
        private LegFactory legFactory;
        private SetFactory setFactory;
        private TurnFactory turnFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            playerFactory = new PlayerFactory();
            matchFactory = new MatchFactory();
            legFactory = new LegFactory();
            setFactory = new SetFactory();
            turnFactory = new TurnFactory();


            Player player1 = (Player)playerFactory.Spawn();
            player1.Name = "Klaas";

            Player player2 = (Player)playerFactory.Spawn();
            player2.Name = "Pieter";

            int numSets = 3;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            Match match = (Match)matchFactory.Spawn();
            match.Player1 = player1;
            match.Player2 = player2;
            match.NumSets = numSets;
            match.NumLegs = numLegs;
            match.BeginningPlayer = beginningPlayer;
            match.MatchState = PlayState.InProgress;
            match.PointsPerLeg = 301;

            this.match = match;
        }


        [TestMethod]
        public void Should_Go_Back_To_Previous_Turn_In_Leg()
        {
            //Arrange
            Set firstSet = (Set)setFactory.Spawn();
            firstSet.SetState = PlayState.InProgress;
            firstSet.Player1LegsWon = 0;
            firstSet.Player2LegsWon = 0;
            firstSet.PlayerPoints = 301;
            firstSet.WinningPlayer = PlayerEnum.None;
            firstSet.NumLegs = match.NumLegs;

            Leg firstLeg = (Leg)legFactory.Spawn();
            firstLeg.LegState = PlayState.InProgress;
            firstLeg.BeginningPlayer = PlayerEnum.Player1;
            firstLeg.Player1LegScore = 301;
            firstLeg.Player2LegScore = 301;
            firstSet.Legs.Add(firstLeg);

            match.Sets.Add(firstSet);

            Turn firstTurn = (Turn)turnFactory.Spawn();
            Turn secondTurn = (Turn)turnFactory.Spawn();

            firstTurn.ThrownPoints = 180;
            firstTurn.TurnState = PlayState.Finished;
            firstLeg.Turns.Add(firstTurn);

            secondTurn.TurnState = PlayState.InProgress;
            secondTurn.ThrownPoints = 0; //empty turn thats automatically added.
            firstLeg.Turns.Add(secondTurn);

            Assert.IsTrue(secondTurn == match.GetCurrentTurn(), "Expected secondturn to be the current turn.");

            //Act
            match.PreviousTurn();

            //Assert
            Assert.IsTrue(firstTurn == match.GetCurrentTurn(), "Expected firstTurn to be the current turn.");
            Assert.AreEqual(firstTurn.ThrownPoints, 0, "Expected firstTurns thrownpoints to be reset.");
            Assert.AreEqual(firstTurn.TurnState, PlayState.InProgress, "Expected firstTurn to be restarted.");
            Assert.AreEqual(secondTurn.TurnState, PlayState.NotStarted, "Expected secondTurn to be notStarted.");
        }

        [TestMethod]
        public void Should_Go_Back_To_Previous_Leg()
        {

            Set firstSet = (Set)setFactory.Spawn();
            firstSet.SetState = PlayState.InProgress;
            firstSet.Player1LegsWon = 0;
            firstSet.Player2LegsWon = 0;
            firstSet.PlayerPoints = 301;
            firstSet.WinningPlayer = PlayerEnum.None;
            firstSet.NumLegs = match.NumLegs;

            Leg firstLeg = (Leg)legFactory.Spawn();
            firstLeg.LegState = PlayState.InProgress;
            firstLeg.BeginningPlayer = PlayerEnum.Player1;
            firstLeg.Player1LegScore = 301;
            firstLeg.Player2LegScore = 301;
            firstSet.Legs.Add(firstLeg);

            match.Sets.Add(firstSet);

            firstLeg.LegState = PlayState.Finished;
            firstLeg.Player1LegScore = 0;

            Turn lastTurnFirstLeg = (Turn)turnFactory.Spawn();
            lastTurnFirstLeg.ThrownPoints = 180;
            lastTurnFirstLeg.TurnState = PlayState.Finished;
            lastTurnFirstLeg.PlayerTurn = PlayerEnum.Player1;
            firstLeg.Turns.Add(lastTurnFirstLeg);

            Turn firstTurnSecondLeg = (Turn)turnFactory.Spawn();
            firstTurnSecondLeg.ThrownPoints = 0;
            firstTurnSecondLeg.TurnState = PlayState.InProgress;


            Leg secondLeg = (Leg)legFactory.Spawn();
            secondLeg.LegState = PlayState.InProgress;
            secondLeg.BeginningPlayer = PlayerEnum.Player2;
            secondLeg.Player1LegScore = 301;
            secondLeg.Player2LegScore = 301;
            secondLeg.Turns.Add(firstTurnSecondLeg);
            firstSet.Legs.Add(secondLeg);

            Assert.IsTrue(secondLeg == match.GetCurrentLeg(), "Expected secondLeg to be the current leg at the moment.");

            //Act
            match.PreviousTurn();

            //Assert
            Assert.IsTrue(firstLeg == match.GetCurrentLeg(), "Expected firstLeg to be the current leg at the moment.");
            Assert.IsTrue(firstLeg.Player1LegScore == 180, "Expected firstleg.Player1LegScore to be 180, but the score was not added.");
            Assert.IsTrue(firstLeg.LegState == PlayState.InProgress, "Expected first leg to be in progress.");
            Assert.IsTrue(secondLeg.LegState == PlayState.NotStarted, "Expected second leg to be not started.");

            Assert.IsTrue(firstTurnSecondLeg.TurnState == PlayState.NotStarted, "Expected first turn of second leg to be not started.");
            Assert.IsTrue(lastTurnFirstLeg.TurnState == PlayState.InProgress,"Expected last turn of first leg to be in progress.");
            Assert.IsTrue(lastTurnFirstLeg.ThrownPoints == 0, "Expected last turn of first leg's thrownpoints to be reset.");

        }

        [TestMethod]
        public void Should_Go_Back_To_Previous_Set()
        {
            Set firstSet = (Set)setFactory.Spawn();
            firstSet.SetState = PlayState.Finished;
            firstSet.NumLegs = match.NumLegs;
            firstSet.Player1LegsWon = match.NumLegs - 1;
            firstSet.Player2LegsWon = 0;
            firstSet.PlayerPoints = 301;
            firstSet.WinningPlayer = PlayerEnum.Player1;

            Set secondSet = (Set)setFactory.Spawn();
            secondSet.SetState = PlayState.InProgress;
            secondSet.NumLegs = match.NumLegs;
            secondSet.Player1LegsWon = 0;
            secondSet.Player2LegsWon = 0;
            secondSet.PlayerPoints = 301;
            secondSet.WinningPlayer = PlayerEnum.None;

            Leg lastLegFirstSet = (Leg)legFactory.Spawn();
            lastLegFirstSet.LegState = PlayState.Finished;
            lastLegFirstSet.BeginningPlayer = PlayerEnum.Player1;
            lastLegFirstSet.Player1LegScore = 0;
            lastLegFirstSet.Player2LegScore = 301;
            firstSet.Legs.Add(lastLegFirstSet);

            Leg firstLegSecondSet = (Leg)legFactory.Spawn();
            firstLegSecondSet.LegState = PlayState.InProgress;
            firstLegSecondSet.BeginningPlayer = PlayerEnum.Player2;
            firstLegSecondSet.Player1LegScore = 301;
            firstLegSecondSet.Player2LegScore = 301;
            secondSet.Legs.Add(firstLegSecondSet);

            Turn lastTurnFirstSet = (Turn)turnFactory.Spawn();
            lastTurnFirstSet.ThrownPoints = 180;
            lastTurnFirstSet.TurnState = PlayState.Finished;
            lastTurnFirstSet.PlayerTurn = PlayerEnum.Player1;
            lastLegFirstSet.Turns.Add(lastTurnFirstSet);

            Turn firstTurnLastSet = (Turn)turnFactory.Spawn();
            firstTurnLastSet.ThrownPoints = 0;
            firstTurnLastSet.TurnState = PlayState.InProgress;
            firstLegSecondSet.Turns.Add(firstTurnLastSet);

            match.Sets.Add(firstSet);
            match.Sets.Add(secondSet);

            Assert.IsTrue(secondSet == match.GetCurrentSet(), "Expected secondset to be the current set at this moment.");
            Assert.IsTrue(firstTurnLastSet == match.GetCurrentTurn(), "Expected first turn of second set to be current turn at this moment.");
            Assert.IsTrue(firstLegSecondSet == match.GetCurrentLeg(), "Expected first leg of second set to be the current leg at this moment");

            //act
            match.PreviousTurn();

            Assert.IsTrue(firstSet == match.GetCurrentSet(), "Expected first set to be current set after previousTurn()");
            Assert.IsTrue(lastTurnFirstSet == match.GetCurrentTurn(), "Expected last turn of first set to be current turn after previousTurn()");
            Assert.IsTrue(lastLegFirstSet == match.GetCurrentLeg(), "Expected last leg of first set to be current set after previousTurn()");
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
            Assert.AreEqual(match.GetCurrentTurn().PlayerTurn, beginningPlayer, "The given PlayerEnum isn't equal to the PlayerEnum in the first turn object.");
        }

        [TestMethod]
        public void Should_Win_Leg_When_LegScore_Is_Zero()
        {
            // Arrange
            match.Start();
            match.GetCurrentLeg().Player1LegScore = 0;


            // Act
            match.ChangeTurn();


            // Assert
            Set firstSet = (Set)match.Sets[0];
            Leg firstLeg = (Leg)firstSet.Legs[0];
            Assert.AreEqual(PlayerEnum.Player1, firstLeg.WinningPlayer, "Expected Player 1 to win, but this was not the case.");
        }

        [TestMethod]
        public void Should_Create_New_Turn_On_ChangeTurn_Call()
        {
            //Arrange
            match.Start();

            //Act
            match.GetCurrentTurn().TurnState = PlayState.Finished;
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(match.GetCurrentLeg().Turns.Count, 2, "New Turn was not created.");
            Assert.AreEqual(match.GetCurrentTurn().PlayerTurn, PlayerEnum.Player2, "It should be Player 2's turn now but it is not.");
        }

        [TestMethod]
        public void Should_Create_New_Leg_On_Leg_Win()
        {
            //Arrange
            match.Start();
            match.GetCurrentLeg().Player1LegScore = 0;
            Set firstSet = (Set)match.Sets[0];

            //Act
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(firstSet.Legs.Count, 2, "New Leg was not created.");
            Assert.AreEqual(((Leg)firstSet.Legs[1]).BeginningPlayer, PlayerEnum.Player2, "Expected Player2 to be the BeginningPlayer for this leg, but this was not the case.");
        }

        [TestMethod]
        public void Should_Create_New_Set_On_Set_Win()
        {
            //Arrange
            match.Start();
            Set firstSet = match.Sets[0] as Set;
            Leg firstLeg = firstSet.Legs[0] as Leg;
            firstLeg.Player1LegScore = 0;
            firstSet.Player1LegsWon = 2;

            //Act
            match.ChangeTurn();

            //Assert
            Assert.AreEqual(match.Sets.Count, 2, "New Set was not created.");
            Assert.AreEqual(firstSet.WinningPlayer, PlayerEnum.Player1, "Expected Player1 to win the Set but this was not the case.");
            Assert.AreEqual(((Set)match.Sets[1]).BeginningPlayer, PlayerEnum.Player2, "Expected Player2 to be the BeginningPlayer for this set, but this was not the case.");
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
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => ProcessThrow.CalculateThrowScore(throwScore, ScoreType.Single));
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

        [TestMethod]
        public void Match_Delete_Should_Delete_All_Nested_Components_Except_Players()
        {
            //ARRANGE
            match.Start();
            match.Post();

            //ACT
            match.Delete();

            //ASSERT
            Assert.AreEqual(ObjectState.Deleted, match.ObjectState);
            Assert.IsNull(matchFactory.Get(match.Id));
            foreach(Set set in match.Sets)
            {
                Assert.AreEqual(ObjectState.Deleted, set.ObjectState);
                Assert.IsNull(new SetFactory().Get(set.Id));
            }
            Assert.AreEqual(ObjectState.Synced, match.Player1.ObjectState);
            Assert.AreEqual(ObjectState.Synced, match.Player2.ObjectState);
            Assert.IsNotNull(playerFactory.Get(((Player)match.Player1).Id));
            Assert.IsNotNull(playerFactory.Get(((Player)match.Player2).Id));
        }

        [TestMethod]
        public void Reading_Match_Properties_Should_Not_Cause_Exceptions()
        {
            match.Start();
            match.Post();
            MatchFactory fact = new MatchFactory();
            match = (Match)fact.Get(match.Id);

            Assert.IsNotNull(match.Id);
            Assert.IsNotNull(match.Player1Id);
            Assert.IsNotNull(match.Player2Id);
            Assert.IsNotNull(match.PointsPerLeg);
            Assert.IsNotNull(match.Player1);
            Assert.IsNotNull(match.Player2);
            Assert.IsNotNull(match.WinningPlayer);
            Assert.IsNotNull(match.BeginningPlayer);
            Assert.IsNotNull(match.MatchState);
            Assert.IsNotNull(match.Sets);
            Assert.IsNotNull(match.NumLegs);
            Assert.IsNotNull(match.NumSets);
            Assert.IsNotNull(match.Player1SetsWon);
            Assert.IsNotNull(match.Player2SetsWon);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DataBaseProvider.Instance.Dispose();
        }
    }
}
