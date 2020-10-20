using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using DARTS.Data;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
using DARTS.View;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    public class MatchesOverviewViewModel : INotifyPropertyChanged
    {
        private List<Match> _displayedMatches = new List<Match>();
        private List<Match> _unfilteredMatches = new List<Match>();

        private string _filterText = "";
        private Match _selectedItem;
        private int _amountOfDisplayedMatches;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackToMainMenuButtonClickCommand { get; }
        public ICommand ClearFilterButtonClickCommand { get; }
        public ICommand OpenMatchClickCommand { get; }

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    FilterTextBox_TextChanged(_filterText);
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterText)));
            }
        }

        public List<Match> DisplayedMatches
        {
            get { return _displayedMatches; }
            set
            {
                _displayedMatches = value;
                AmountOfDisplayedMatches = _displayedMatches.Count();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayedMatches)));
            }
        }

        public Match SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public int AmountOfDisplayedMatches
        {
            get { return _amountOfDisplayedMatches; }
            set
            {
                _amountOfDisplayedMatches = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountOfDisplayedMatches)));
            }
        }
        public MatchesOverviewViewModel(List<Match> matches)
        {
            BackToMainMenuButtonClickCommand = new RelayCommand(execute => BackToMainMenuButton_Click());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilterButton_Click(), canExecute => CanExecuteClearFilterButtonClick());
            OpenMatchClickCommand = new RelayCommand(execute => OpenMatchButton_Click(), canExecute => CanExecuteOpenMatchButtonClick());

            _unfilteredMatches = matches;
            DisplayedMatches = _unfilteredMatches;

            if (matches.Count == 0) GetMatchesOverviewData();
        }

        private void GetMatchesOverviewData()
        {
            //TODO: To be changed to get data from database: Function to add dummy data to matches overview:
            DisplayedMatches = DummyData.TempAddListItems();
            _unfilteredMatches = _displayedMatches;
        }

        private void FilterTextBox_TextChanged(string filterText)
        {
            if (filterText == "" || filterText == string.Empty)
            {
                DisplayedMatches = _unfilteredMatches;
            }
            else
            {
                FilterMatches(filterText);
            }
        }

        private void FilterMatches(string filterText)
        {
            string loweredFilterText = filterText.ToLower();
            DisplayedMatches = _unfilteredMatches.Where(Match =>
                Match.Player1.Name.ToLower().Contains(loweredFilterText) || 
                Match.Player2.Name.ToLower().Contains(loweredFilterText) || 
                Match.Sets.Count.ToString().ToLower().Contains(loweredFilterText)
            ).ToList();
        }

        private void ClearFilterButton_Click()
        {
            FilterText = "";
            DisplayedMatches = _unfilteredMatches;
        }

        private bool CanExecuteClearFilterButtonClick()
        {
            return _filterText != null && _filterText != "";
        }

        private void BackToMainMenuButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToMainMenu();
        }

        private void OpenMatchButton_Click()
        {
            //TODO: Create a new window for match detail information
        }

        private bool CanExecuteOpenMatchButtonClick()
        {
            return _selectedItem != null;
        }
    }
}
