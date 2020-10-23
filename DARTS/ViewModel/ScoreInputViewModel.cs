using DARTS.Data.DataObjects;
using DARTS.Functionality;
using DARTS.ViewModel.Command;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DARTS.ViewModel
{
    public class ScoreInputViewModel : INotifyPropertyChanged
    {
        public ICommand SubmitScoreButtonClickCommand { get; }
        public ICommand PreviousTurnButtonClickCommand { get; }
        public ICommand StopMatchButtonClickCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int[] Throws { get; set; }
        public ScoreType[] ThrowTypes { get; set; }
        public ScoreType[] ScoreTypes { get; }

        private Match Match { get; }
        private Task AsyncPostTask { get; set; }

        #region Match object bindings
        public string Player1Name
        {
            get => ((Player)Match.Player1).Name;
        }

        public string Player2Name
        {
            get => ((Player)Match.Player2).Name;
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
            get => Match.GetCurrentSet().Player1LegsWon;
        }

        public int Player2LegsWon
        {
            get => Match.GetCurrentSet().Player2LegsWon;
        }

        public uint Player1Score
        {
            get => Match.GetCurrentLeg().Player1LegScore;
        }

        public uint Player2Score
        {
            get => Match.GetCurrentLeg().Player2LegScore;
        }

        public string IsPlayer1Turn
        {
            get
            {
                switch (Match.GetCurrentTurn().PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        return "Visible";
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
                switch (Match.GetCurrentTurn().PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        return "Hidden";
                    case PlayerEnum.Player2:
                        return "Visible";
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
                return string.Format("Leg {0} of {1} in Set {2} of {3}", Match.GetCurrentSet().Legs.Count(), Match.NumLegs, Match.Sets.Count(), Match.NumSets);
            }
        }
        #endregion

        public ScoreInputViewModel(Match match)
        {
            Throws = new int[3];
            ThrowTypes = new ScoreType[3];
            ScoreTypes = (ScoreType[])Enum.GetValues(typeof(ScoreType));
            Match = match;

            SubmitScoreButtonClickCommand = new RelayCommand(execute => SubmitScoreButtonClick(), canExecute => CanExecuteSubmitScoreButtonClick());
            PreviousTurnButtonClickCommand = new RelayCommand(execute => PreviousTurnButtonClick(), canExecute => CanExecutePreviousTurnButtonClick());
            StopMatchButtonClickCommand = new RelayCommand(execute => StopMatchButtonClick(), canExecute => true);
        }

        private void ResetScreen()
        {
            Throws = new int[3];
            ThrowTypes = new ScoreType[] { 0, 0, 0 };
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }

        private void StopMatchButtonClick()
        {
            throw new NotImplementedException();
        }

        private void PreviousTurnButtonClick()
        {
            throw new NotImplementedException();
        }

        private void SubmitScoreButtonClick()
        {
            for (int i = 0; i < Throws.Count(); i++)
            {
                Match.GetCurrentTurn().Throws.Add(ProcessThrow.CalculateThrowScore(Throws[i], ThrowTypes[i]));
            }

            Match.GetCurrentTurn().CalculateThrownPoints();
            Match.GetCurrentLeg().SubtractScore();
            Match.ChangeTurn();
            AsyncPostTask = new Task(() => Match.Post());
            AsyncPostTask.Start();

            ResetScreen();
        }

        #region CanExecute Functions
        /// <summary>
        /// Checks if there are more than one turn in the current leg.
        /// </summary>
        /// <returns>True when there are more than one turn in the current leg.</returns>
        private bool CanExecutePreviousTurnButtonClick()
        {
            return (Match.GetCurrentLeg().Turns.Count > 1);
        }

        /// <summary>
        /// Checks if the special throw cases (Miss, bull, bullseye) have the correct associated score.
        /// </summary>
        /// <returns>True when all the throws have the correct ScoreType.</returns>
        private bool CanExecuteSubmitScoreButtonClick()
        {
            if (AsyncPostTask != null && !AsyncPostTask.IsCompleted) return false;

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
                    default:
                        if (!(Throws[i] > 0 && Throws[i] <= 20)) return false;
                        break;
                }
            }

            return true;
        }
        #endregion
    }
}
