using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DARTS.Data;
using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    public class MatchesOverviewViewModel : INotifyPropertyChanged
    {
        private List<DataObjectBase> _displayedMatches = new List<DataObjectBase>();
        private List<DataObjectBase> _unfilteredMatches = new List<DataObjectBase>();

        private string _filterText = "";
        private Match _selectedItem;
        private int _amountOfDisplayedMatches;

        private MatchFactory matchFactory = new MatchFactory();

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

        public List<DataObjectBase> DisplayedMatches
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
        public MatchesOverviewViewModel(List<DataObjectBase> matches)
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
            DisplayedMatches = matchFactory.Get();
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
            for(int i = 0; i < _unfilteredMatches.Count; i++)
            {
                if (((Player)((Match)_unfilteredMatches[i]).Player1).Name.ToLower().Contains(loweredFilterText) ||
                ((Player)((Match)_unfilteredMatches[i]).Player2).Name.ToLower().Contains(loweredFilterText) ||
                ((Match)_unfilteredMatches[i]).Sets.Count.ToString().ToLower().Contains(loweredFilterText))
                {
                    DisplayedMatches.Add(_unfilteredMatches[i]);
                }
            }
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
            Match specifiedmatch = SelectedItem;
            GameInstance.Instance.MainWindow.ChangeToMatchDetailView(specifiedmatch);
        }

        private bool CanExecuteOpenMatchButtonClick()
        {
            return _selectedItem != null;
        }
    }
}
