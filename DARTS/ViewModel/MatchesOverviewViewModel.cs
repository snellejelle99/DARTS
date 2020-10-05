﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DARTS.Data;
using DARTS.Data.DataObjects;
using DARTS.View;
using DARTS.ViewModel.Command;


namespace DARTS.ViewModel
{
    class MatchesOverviewViewModel : INotifyPropertyChanged
    {

        private List<Match> _displayedMatches = new List<Match>();
        private List<Match> _unfilteredMatches = new List<Match>();

        private string _filterText = "";
        private Match _selectedItem;
        private int _amountOfDisplayedMatches;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackButtonClickCommand { get; }
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
        public MatchesOverviewViewModel()
        {

            BackButtonClickCommand = new RelayCommand(execute => BackButton_Click(), canExecute => CanExecuteBackButtonClick());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilter_Click(), canExecute => CanExecuteClearFilterButtonClick());
            OpenMatchClickCommand = new RelayCommand(execute => OpenMatchButton_Click(), canExecute => CanExecuteOpenMatchButtonClick());

            GetMatchesOverviewData();
        }

        private void GetMatchesOverviewData()
        {
            //TODO: To be changed to get data from database: Function to add dummy data to matches overview:
            DisplayedMatches = DummyData.TempAddListItems();
            _unfilteredMatches = _displayedMatches;
        }

        #region Filter
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
            DisplayedMatches = new List<Match>(_unfilteredMatches.Where(Match => Match.Player1.Name.ToLower().Contains(loweredFilterText) || Match.Player2.Name.ToLower().Contains(loweredFilterText) || Match.Sets.Count.ToString().ToLower().Contains(loweredFilterText)));
        }

        private void ClearFilter_Click()
        {
            FilterText = "";
            DisplayedMatches = _unfilteredMatches;
        }
        private bool CanExecuteClearFilterButtonClick()
        {
            return (_filterText != null && _filterText != "");
        }

        private void BackButton_Click()
        {

        }

        private bool CanExecuteBackButtonClick()
        {
            return true;
        }

        private void OpenMatchButton_Click()
        {

        }

        private bool CanExecuteOpenMatchButtonClick()
        {

            if (_selectedItem != null)
                return true;
            else
                return false;
        }

        #endregion
    }
}