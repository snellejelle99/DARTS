using DARTS.Data.DataObjectFactories;
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

        public List<Player> DisplayedPlayers
        {
            get { return _displayedPlayers; }
            set
            {
                _displayedPlayers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayedPlayers)));

                string newAmountOfResultsLabelText = Convert.ToString(_displayedPlayers.Count);
                if (_displayedPlayers.Count != _unfilteredPlayers.Count)
                    newAmountOfResultsLabelText += " out of " + Convert.ToString(_unfilteredPlayers.Count);
                AmountOfResultsLabelText = newAmountOfResultsLabelText;
            }
        }

        public Player SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null && _displayedPlayers.Count() > 0)
                    OpenPlayerDetailsViewClick();
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

        public PlayersOverviewViewModel(List<Player> players)
        {
            // view commands:
            BackButtonClickCommand = new RelayCommand(execute => BackButtonClick());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilterButtonClick(), canExecute => CanExecuteClearFilterButtonClick());

            // TEMP: SetListItems #29
            _unfilteredPlayers.AddRange(players);
            DisplayedPlayers = players;

            // view data:
            if (players.Count == 0) GetPlayersOverviewData();
            // TODO: Retrieve players to display #29:
            //_unfilteredPlayers = get list of players to display...;
            //DisplayedPlayers = _unfilteredPlayers;
        }

        // TEMP: until data retrieval implementation is finished.
        private void GetPlayersOverviewData()
        {
            PlayerFactory factory = new PlayerFactory();
            for (int i = 0; i < 3; i++)
            {
                Player p = (Player)factory.Spawn();
                p.Name = "player" + Convert.ToString(i);
                
                _displayedPlayers.Add(p);
                _displayedPlayers.Add(p);
            }
            _unfilteredPlayers.AddRange( _displayedPlayers);
        }

        private void BackButtonClick()
        {
            GameInstance.Instance.MainWindow.ChangeToMainMenu();
        }

        private void OpenPlayerDetailsViewClick()
        {
            //TODO: Create a new window for player detail information, with _selectedItem as argument #28
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
    }
}
