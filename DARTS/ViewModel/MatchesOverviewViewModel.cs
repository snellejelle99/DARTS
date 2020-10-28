using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    public class MatchesOverviewViewModel : INotifyPropertyChanged
    {
        private BindingList<DataObjectBase> _displayedMatches = new BindingList<DataObjectBase>();
        private BindingList<DataObjectBase> _unfilteredMatches = new BindingList<DataObjectBase>();

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

        public BindingList<DataObjectBase> DisplayedMatches
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

        public MatchesOverviewViewModel(BindingList<DataObjectBase> matches)
        {
            BackToMainMenuButtonClickCommand = new RelayCommand(execute => BackToMainMenuButton_Click());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilterButton_Click(), canExecute => CanExecuteClearFilterButtonClick());
            OpenMatchClickCommand = new RelayCommand(execute => OpenMatchButton_Click(), canExecute => CanExecuteOpenMatchButtonClick());

            _unfilteredMatches = matches;
            DisplayedMatches = new BindingList<DataObjectBase>(_unfilteredMatches);

            if (matches.Count == 0)
            {
                GetMatchesOverviewData();
            }
        }

        private void GetMatchesOverviewData()
        {
            _unfilteredMatches = new BindingList<DataObjectBase>(matchFactory.Get());
            DisplayedMatches = new BindingList<DataObjectBase>(_unfilteredMatches);
        }

        private void FilterTextBox_TextChanged(string filterText)
        {
            if (filterText == "" || filterText == string.Empty)
            {
                DisplayedMatches = new BindingList<DataObjectBase>(_unfilteredMatches);
            }
            else
            {
                DisplayedMatches = new BindingList<DataObjectBase>(_unfilteredMatches.Where(
                    match => ((Player)((Match)match).Player1).Name.ToLower().Contains(filterText.ToLower()) ||
                             ((Player)((Match)match).Player2).Name.ToLower().Contains(filterText.ToLower()) ||
                             ((Match)match).Sets.Count.ToString().ToLower().Contains(filterText.ToLower()))
                    .ToList());
            }
        }

        private void ClearFilterButton_Click()
        {
            FilterText = "";
            DisplayedMatches = new BindingList<DataObjectBase>(_unfilteredMatches);
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
