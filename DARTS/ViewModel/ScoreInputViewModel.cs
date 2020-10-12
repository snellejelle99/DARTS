using DARTS.Data.DataObjects;
using DARTS.ViewModel.Command;
using System;
using System.Linq;
using System.Windows.Input;

namespace DARTS.ViewModel
{
    public class ScoreInputViewModel
    {
        public ICommand SubmitScoreButtonClickCommand { get; }

        public int[] Throws { get; set; }

        public ScoreType[] ThrowTypes { get; set; }

        public ScoreType[] ScoreTypes { get; }

        private Match Match { get; }

        public string Player1Name
        {
            get => Match.Player1.Name;
        }

        public string Player2Name
        {
            get => Match.Player2.Name;
        }

        public int Player1SetsWon
        {
            get => Match.Player1SetsWon;
        }

        public int Player2SetsWon
        {
            get => Match.Player2SetsWon;
        }

        public int Player1LegsWon
        {
            get => Match.Sets.Last().Player1LegsWon;
        }

        public int Player2LegsWon
        {
            get => Match.Sets.Last().Player2LegsWon;
        }

        public uint Player1Score
        {
            get => Match.Sets.Last().Legs.Last().Player1LegScore;
        }

        public uint Player2Score
        {
            get => Match.Sets.Last().Legs.Last().Player2LegScore;
        }

        public string IsPlayer1Turn
        {
            get
            {
                switch (Match.Sets.Last().Legs.Last().Turns.Last().PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        return "Visable";
                    case PlayerEnum.Player2:
                        return "Hidden";
                    default:
                        return "Hidden";
                }
            }
        }

        public string IsPlayer2Turn
        {
            get
            {
                switch (Match.Sets.Last().Legs.Last().Turns.Last().PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        return "Hidden";
                    case PlayerEnum.Player2:
                        return "Visable";
                    default:
                        return "Hidden";
                }
            }
        }

        public string SetsToWin
        {
            get
            {
                return string.Format("First to {0}", Match.NumSets / 2 + 1);
            }
        }

        public string CurrentLeg
        {
            get
            {
                return string.Format("Leg {0} of {1} in Set {2} of {3}", Match.Sets.Last().Legs.Count(), Match.NumLegs, Match.Sets.Count(), Match.NumSets);
            }
        }

        public ScoreInputViewModel(Match match)
        {
            Throws = new int[3];
            ThrowTypes = new ScoreType[3];
            ScoreTypes = (ScoreType[])Enum.GetValues(typeof(ScoreType));
            Match = match;

            SubmitScoreButtonClickCommand = new RelayCommand(execute => SubmitScoreButtonClick(), canExecute => CanExecuteSubmitScoreButtonClick());
        }

        private void SubmitScoreButtonClick()
        {
            //submit score
        }

        /// <summary>
        /// Checks if the special throw cases (Miss, bull, bullseye) have the correct associated score.
        /// </summary>
        /// <returns>Whether or not if all the throws have te correct ScoreType.</returns>
        private bool CanExecuteSubmitScoreButtonClick()
        {
            for (int i = 0; i < Throws.Length; i++)
            {
                switch (ThrowTypes[i])
                {
                    case ScoreType.Miss:
                        if (Throws[i] != 0) return false;
                        break;
                    case ScoreType.Bull:
                        if (Throws[i] != 25) return false;
                        break;
                    case ScoreType.Bullseye:
                        if (Throws[i] != 50) return false;
                        break;
                }
            }

            return true;
        }
    }
}
