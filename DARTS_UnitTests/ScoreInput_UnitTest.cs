using DARTS.Data.DataObjects;
using DARTS.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.ViewModel;
using System.Globalization;
using DARTS.ValidationRules;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace DARTS_UnitTests
{
    [TestClass]
    public class ScoreInput_UnitTest
    {
        private ThrowInputValidationRule validator = new ThrowInputValidationRule();
        private Match match;
        private ScoreInputViewModel viewModel;
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

            match = new Match();
            match.Player1 = player1;
            match.Player2 = player2;
            match.NumSets = numSets;
            match.NumLegs = numLegs;
            match.BeginningPlayer = beginningPlayer;
            match.MatchState = PlayState.InProgress;

            viewModel = new ScoreInputViewModel(match);
        }

        [TestMethod]
        public void ScoreType_Should_Match_Score()
        {     
            viewModel.Throws = new int[] { 0, 25, 50 };
            viewModel.ThrowTypes = new ScoreType[] { ScoreType.Miss, ScoreType.Bull, ScoreType.Bullseye };
            ScoreType[] wrongTypes = new ScoreType[] { ScoreType.Single, ScoreType.Bullseye, ScoreType.Bull };
          
            Assert.IsTrue(viewModel.SubmitScoreButtonClickCommand.CanExecute(null), "One or more throw and throwtypes did not match");

            viewModel.ThrowTypes = wrongTypes;
            Assert.IsFalse(viewModel.SubmitScoreButtonClickCommand.CanExecute(null), "All matches approved.");        
        }
    
        [TestMethod]
        public void Should_Allow_Only_Numbers()
        {
            Assert.IsTrue(validator.Validate(1, null).IsValid);
            Assert.IsFalse(validator.Validate("test", null).IsValid);
        }
       
        [TestMethod]
        public void Should_Allow_Numbers_0_To_20()
        {
            Assert.IsTrue(validator.Validate(0, null).IsValid);
            Assert.IsTrue(validator.Validate(20, null).IsValid);
            Assert.IsFalse(validator.Validate(21, null).IsValid);
            Assert.IsFalse(validator.Validate(-1, null).IsValid);
        }

        [TestMethod]
        public void Should_Allow_Bull_Cases()
        {
            Assert.IsTrue(validator.Validate(50, null).IsValid);
            Assert.IsTrue(validator.Validate(25, null).IsValid);
            Assert.IsFalse(validator.Validate(23, null).IsValid);
        }


    }
}
