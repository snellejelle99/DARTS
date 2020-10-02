using DARTS.Data.DataObjects;
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
    public class PlayersOverviewViewModel : INotifyPropertyChanged
    {
        private List<Player> _displayedPlayers = new List<Player>();
        private List<Player> _unfilteredPlayers = new List<Player>();
        private Player _selectedItem;
        private string _amountOfResultsLabelText = "";
        private string _filterTextBoxText = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackButtonClickCommand { get; }
        public ICommand ClearFilterButtonClickCommand { get; }
        public ICommand OpenPlayerDetailsClickCommand { get; }

        public List<Player> DisplayedPlayers
        {
            get { return _displayedPlayers; }
            set
            {
                _displayedPlayers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayedPlayers"));
            }
        }

        public Player SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        public string AmountOfResultsLabelText
        {
            get { return _amountOfResultsLabelText; }
            set
            {
                _amountOfResultsLabelText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AmountOfResultsLabelText"));
            }
        }

        public string FilterTextBoxText
        {
            get { return _filterTextBoxText; }
            set 
            { 
                _filterTextBoxText = value;
                FilterTextBoxTextChanged();
            }
        } 

        public PlayersOverviewViewModel(List<Player> players)
        {
            // view commands:
            BackButtonClickCommand = new RelayCommand(execute => BackButtonClick(), canExecute => CanExecuteBackButtonClick());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilterButtonClick(), canExecute => CanExecuteClearFilterButtonClick());
            OpenPlayerDetailsClickCommand = new RelayCommand(execute => OpenPlayerDetailsClick(), canExecute => CanExecuteOpenPlayerDetailsClick());

            // SetListItems
            SetListItems(players);

            // get view data:
            GetPlayersOverviewData();

            UpdateAmountOfResultsLabelText();
        }

        /// <summary>
        /// Updates window for changes in player items to showcase.
        /// </summary>
        private void UpdateAmountOfResultsLabelText()
        {
            AmountOfResultsLabelText = Convert.ToString(_displayedPlayers.Count);
            if (_displayedPlayers.Count != _unfilteredPlayers.Count)
                AmountOfResultsLabelText += " - " + Convert.ToString(_unfilteredPlayers.Count);
        }

        private void GetPlayersOverviewData()
        {
            // TODO: Retrieve players to display.
            //_unfilteredPlayers = get list of players to display...;
            //_displayedPlayers.Clear();
            //_displayedPlayers.AddRange(_unfilteredPlayers);

            // TODO: Temporary until data retrieval implementation is finished.
            for (int i = 0; i < 3; i++)
            {
                Player p = new Player();
                p.Name = "player" + Convert.ToString(i);
                p.ID = i;
                _displayedPlayers.Add(p);
            }
            SetListItems(_displayedPlayers);
        }

        // TODO: Remove temporary function that is for test purposes.
        private void SetListItems(List<Player> player)
        {
            _unfilteredPlayers.AddRange(player);
        }

        /// <summary>
        /// Whene back button is pressed, returns user to main window. 
        /// </summary>
        private void BackButtonClick()
        {
            //TODO: Go to main menu
        }

        private bool CanExecuteBackButtonClick()
        {
            return true;
        }
        private void OpenPlayerDetailsClick()
        {
            //TODO: Create a new window for player detail information, with _selectedItem as argument
        }

        private bool CanExecuteOpenPlayerDetailsClick()
        {
            return _selectedItem != null && _displayedPlayers.Count() > 0;
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
                FilterPlayers(filterText);
            }

            UpdateAmountOfResultsLabelText();
        }

        private void FilterPlayers(string filterText)
        {
            DisplayedPlayers = _unfilteredPlayers.Where(player => player.Name.ToLower().Contains(filterText.ToLower())).ToList();
        }

        private void ClearFilterButtonClick()
        {
            DisplayedPlayers = _unfilteredPlayers;

            UpdateAmountOfResultsLabelText();
        }

        private bool CanExecuteClearFilterButtonClick()
        {
            return _displayedPlayers.Count != _unfilteredPlayers.Count || _filterTextBoxText != "";
        }
    }
}
