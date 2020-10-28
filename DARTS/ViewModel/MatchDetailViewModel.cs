using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DARTS.Data.DataObjects;
using DARTS.ViewModel.Command;
using DARTS.Data.Singletons;

namespace DARTS.ViewModel
{
    public class MatchDetailViewModel : INotifyPropertyChanged
    {
        private Match specifiedMatch;
        private BindingList<DataObjectBase> _specifiedLegs = new BindingList<DataObjectBase>();
        private BindingList<DataObjectBase> _specifiedTurns = new BindingList<DataObjectBase>();
        private Set _selectedSet;
        private Leg _selectedLeg;

        private string player1Name;
        private string player2Name;
        private string setsWon;

        private int totalAmount180s = 0;
        private int player1Amount180s = 0;
        private int player2Amount180s = 0;
        private int player1AverageScore = 0;
        private int player2AverageScore = 0;

        public ICommand OpenSetDetailsClickCommand { get; }
        public ICommand OpenLegDetailsClickCommand { get; }
        public ICommand BackToOverviewButtonClickCommand { get; }
        public ICommand ClearLegsClickCommand { get; }
        public ICommand ClearTurnsClickCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MatchDetailViewModel(Match match)
        {
            specifiedMatch = match;
            Player1Name = ((Player)specifiedMatch.Player1).Name;
            Player2Name = ((Player)specifiedMatch.Player2).Name;
            SetsWon = specifiedMatch.Player1SetsWon + " - " + specifiedMatch.Player2SetsWon;
            BackToOverviewButtonClickCommand = new RelayCommand(execute => BackToOverviewButton_Click());
            OpenSetDetailsClickCommand = new RelayCommand(execute => OpenSetDetailsButton_Click(), canExecute => CanExecuteSetDetailsButtonClick());
            OpenLegDetailsClickCommand = new RelayCommand(execute => OpenLegDetailsButton_Click(), canExecute => CanExecuteLegDetailsButtonClick());
            ClearLegsClickCommand = new RelayCommand(execute => ClearLegsButton_Click(), canExecute => CanExecuteClearLegsButtonClick());
            ClearTurnsClickCommand = new RelayCommand(execute => ClearTurnsButton_Click(), canExecute => CanExecuteClearTurnsButtonClick());
            CalculateAmountof180s();
            AverageScorePerPlayer();
        }

        public string SetsWon
        {
            get { return setsWon; }
            set
            {
                setsWon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SetsWon)));
            }
        }

        public int TotalAmount180s
        {
            get { return totalAmount180s; }
            set
            {
                totalAmount180s = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalAmount180s)));
            }
        }

        public int Player1Amount180s
        {
            get { return player1Amount180s; }
            set
            {
                player1Amount180s = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player1Amount180s)));
            }
        }

        public int Player2Amount180s
        {
            get { return player2Amount180s; }
            set
            {
                player2Amount180s = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player2Amount180s)));
            }
        }

        public int Player1AverageScore
        {
            get { return player1AverageScore; }
            set
            {
                player1AverageScore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player1AverageScore)));
            }
        }

        public int Player2AverageScore
        {
            get { return player2AverageScore; }
            set
            {
                player2AverageScore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player2AverageScore)));
            }
        }

        public string Player1Name
        {
            get { return player1Name; }
            set
            {
                player1Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Player1Name));
            }
        }

        public string Player2Name
        {
            get { return player2Name; }
            set
            {
                player2Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Player2Name));
            }
        }

        public Match SpecifiedMatch
        {
            get { return specifiedMatch; }
        }

        public BindingList<DataObjectBase> SpecifiedLegs
        {
            get { return _specifiedLegs; }
            set
            {
                _specifiedLegs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SpecifiedLegs)));
            }
        }

        public BindingList<DataObjectBase> SpecifiedTurns
        {
            get { return _specifiedTurns; }
            set
            {
                _specifiedTurns = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SpecifiedTurns)));
            }
        }

        public Set SelectedSet
        {
            get { return _selectedSet; }
            set
            {
                _selectedSet = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedSet)));
            }
        }

        public Leg SelectedLeg
        {
            get { return _selectedLeg; }
            set
            {
                _selectedLeg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedLeg)));
            }
        }

        private void AverageScorePerPlayer()
        {
            int player1TotalScore = 0;
            int player1AmountOfThrows = 0;
            int player2TotalScore = 0;
            int player2AmountOfThrows = 0;
            for (int i = 0; i < specifiedMatch.Sets.Count; i++)
            {
                for (int j = 0; j < ((Set)specifiedMatch.Sets[i]).Legs.Count; j++)
                {
                    for (int k = 0; k < ((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns.Count; k++)
                    {
                        player1TotalScore += ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).PlayerTurn == PlayerEnum.Player1 ? ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).ThrownPoints : 0;
                        player1AmountOfThrows += ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).PlayerTurn == PlayerEnum.Player1 ? 1 : 0;
                        player2TotalScore += ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).PlayerTurn == PlayerEnum.Player2 ? ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).ThrownPoints : 0;
                        player2AmountOfThrows += ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).PlayerTurn == PlayerEnum.Player2 ? 1 : 0;
                    }
                }
            }
            if (player1AmountOfThrows != 0)
                player1AverageScore = player1TotalScore / player1AmountOfThrows;
            if (player2AmountOfThrows != 0)
                player2AverageScore = player2TotalScore / player2AmountOfThrows;
        }

        private void CalculateAmountof180s()
        {
            for (int i = 0; i < specifiedMatch.Sets.Count; i++)
            {
                for (int j = 0; j < ((Set)specifiedMatch.Sets[i]).Legs.Count; j++)
                {
                    for (int k = 0; k < ((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns.Count; k++)
                    {
                        if (((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).ThrownPoints == 180)
                        {
                            player1Amount180s += ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).PlayerTurn == PlayerEnum.Player1 ? 1 : 0;
                            player2Amount180s += ((Turn)((Leg)((Set)specifiedMatch.Sets[i]).Legs[j]).Turns[k]).PlayerTurn == PlayerEnum.Player2 ? 1 : 0;
                            totalAmount180s = player1Amount180s + player2Amount180s;
                        }
                    }
                }
            }
        }

        private void BackToOverviewButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToMatchesOverview(new BindingList<DataObjectBase>());
        }

        private void OpenSetDetailsButton_Click()
        {
            SpecifiedLegs = _selectedSet.Legs;
        }

        private bool CanExecuteSetDetailsButtonClick()
        {
            return _selectedSet != null;
        }

        private void ClearLegsButton_Click()
        {
            SpecifiedLegs = new BindingList<DataObjectBase>();
            SpecifiedTurns = new BindingList<DataObjectBase>();
        }

        private bool CanExecuteClearLegsButtonClick()
        {
            return SpecifiedLegs.Count() != 0;
        }

        private void ClearTurnsButton_Click()
        {
            SpecifiedTurns = new BindingList<DataObjectBase>();
        }

        private bool CanExecuteClearTurnsButtonClick()
        {
            return SpecifiedTurns.Count() != 0;
        }

        private void OpenLegDetailsButton_Click()
        {
            SpecifiedTurns = _selectedLeg.Turns;
        }

        private bool CanExecuteLegDetailsButtonClick()
        {
            return _selectedLeg != null;
        }
    }
}
