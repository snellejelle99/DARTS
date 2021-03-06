﻿using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
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
    public class PlayersOverviewViewModel : INotifyPropertyChanged
    {
        static PlayerFactory _playerFactory;

        private List<Player> _displayedPlayers = new List<Player>();
        private List<Player> _unfilteredPlayers = new List<Player>();
        private Player _selectedItem;
        private string _amountOfResultsLabelText = "";
        private string _filterTextBoxText = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackButtonClickCommand { get; }
        public ICommand ClearFilterButtonClickCommand { get; }
        public ICommand OpenPlayerMatchClickCommand { get; }

        public List<Player> DisplayedPlayers
        {
            get { return _displayedPlayers; }
            set
            {
                _displayedPlayers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayedPlayers)));

                AmountOfResultsLabelText = Convert.ToString(_displayedPlayers.Count);
            }
        }

        public Player SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
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

        public PlayersOverviewViewModel()
        {
            // view commands:
            BackButtonClickCommand = new RelayCommand(execute => BackButtonClick());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilterButtonClick(), canExecute => CanExecuteClearFilterButtonClick());
            OpenPlayerMatchClickCommand = new RelayCommand(execute => OpenPlayerMatchButtonClick(), canExecute => CanExecuteOpenPlayerMatchButtonClick());

            // viewModel data:
            _playerFactory = new PlayerFactory();

            _unfilteredPlayers = _playerFactory.Get().Cast<Player>().ToList();

            DisplayedPlayers = _unfilteredPlayers;
        }

        private void BackButtonClick()
        {
            GameInstance.Instance.MainWindow.ChangeToMainMenu();
        }

        private void FilterTextBoxTextChanged()
        {
            Filter(_filterTextBoxText);
        }

        public void Filter(string filterText)
        {
            if (filterText == "" || filterText == string.Empty)
            {
                DisplayedPlayers = _unfilteredPlayers;
            }
            else
            {
                DisplayedPlayers = _unfilteredPlayers.Where(player => player.Name.ToLower().Contains(filterText.ToLower())).ToList();
            }
        }

        private void ClearFilterButtonClick()
        {
            DisplayedPlayers = _unfilteredPlayers;
            FilterTextBoxText = "";
        }

        private bool CanExecuteClearFilterButtonClick()
        {
            return _filterTextBoxText != "";
        }

        private void OpenPlayerMatchButtonClick()
        {
            GameInstance.Instance.MainWindow.ChangeToPlayerMatchStatisticsView(_selectedItem.GetMatches().Cast<Match>().ToList());
        }

        private bool CanExecuteOpenPlayerMatchButtonClick()
        {
            return _selectedItem != null;
        }
    }
}
