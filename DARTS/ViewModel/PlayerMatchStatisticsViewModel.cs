using DARTS.Data;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
using DARTS.View;
using DARTS.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DARTS.ViewModel
{
    public class PlayerMatchStatisticsViewModel : INotifyPropertyChanged
    {
        private List<Match> _displayedMatches = new List<Match>();
        private List<Match> _unfilteredMatches = new List<Match>();
        private string _amountOfResultsLabelText = "";
        private string _filterTextBoxText = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackButtonClickCommand { get; }
        public ICommand ClearFilterButtonClickCommand { get; }

        public List<Match> DisplayedMatches
        {
            get { return _displayedMatches; }
            set
            {
                _displayedMatches = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayedMatches)));

                string newAmountOfResultsLabelText = Convert.ToString(_displayedMatches.Count);
                if (_displayedMatches.Count != _unfilteredMatches.Count)
                    newAmountOfResultsLabelText += " out of " + Convert.ToString(_unfilteredMatches.Count);
                AmountOfResultsLabelText = newAmountOfResultsLabelText;
            }
        }

        public string AmountOfResultsLabelText
        {
            get { return _amountOfResultsLabelText; }
            set
            {
                _amountOfResultsLabelText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountOfResultsLabelText)));
            }
        }

        public string FilterTextBoxText
        {
            get { return _filterTextBoxText; }
            set 
            { 
                _filterTextBoxText = value;
                FilterTextBoxTextChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterTextBoxText)));
            }
        } 

        public PlayerMatchStatisticsViewModel(List<Match> matches)
        {
            // view commands:
            BackButtonClickCommand = new RelayCommand(execute => BackButtonClick(), canExecute => CanExecuteBackButtonClick());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilterButtonClick(), canExecute => CanExecuteClearFilterButtonClick());

            // TEMP: SetListItems #29
            _unfilteredMatches.AddRange(matches);
            DisplayedMatches = matches;

            // view data:
            if (matches.Count == 0) GetPlayersMatchStatisticsData();
            // TODO: Retrieve players to display #29:
            //_unfilteredPlayers = get list of players to display...;
            //DisplayedPlayers = _unfilteredPlayers;
        }

        // TEMP: until data retrieval implementation is finished.
        private void GetPlayersMatchStatisticsData()
        {
            _unfilteredMatches = DummyData.TempAddListItems();
            DisplayedMatches = _unfilteredMatches;
        }

        private void BackButtonClick()
        {
            GameInstance.Instance.MainWindow.ChangeToPlayerOverviewView();
        }

        private bool CanExecuteBackButtonClick()
        {
            return true;
        }

        private void FilterTextBoxTextChanged()
        {
            Filter(_filterTextBoxText);
        }

        public void Filter(string filterText)
        {
            if (filterText == "" || filterText == string.Empty)
            {
                DisplayedMatches = _unfilteredMatches;
            }
            else
            {
                DisplayedMatches = _unfilteredMatches.Where(match => match.Player1.Name.ToLower().Contains(filterText.ToLower()) || match.Player2.Name.ToLower().Contains(filterText.ToLower())).ToList();
            }
        }

        private void ClearFilterButtonClick()
        {
            DisplayedMatches = _unfilteredMatches;
            FilterTextBoxText = "";
        }

        private bool CanExecuteClearFilterButtonClick()
        {
            return _filterTextBoxText != "";
        }
    }
}
