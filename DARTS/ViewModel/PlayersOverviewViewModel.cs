using DARTS.Data.DataObjects;
using DARTS.View;
using DARTS.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DARTS.ViewModel
{
    public class PlayersOverviewViewModel
    {

        private List<Player> _displayedPlayers = new List<Player>();
        private List<Player> _unfilteredPlayers = new List<Player>();
        private string _amountOfResultsLabelText = "";
        private string _filterTextBoxText = "";

        public List<Player> DisplayedPlayers
        {
            get { return _displayedPlayers; }
        }

        public string AmountOfResultsLabelText
        {
            get { return _amountOfResultsLabelText; }
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

        public ICommand BackButtonClickCommand
        {
            get;
        }

        public ICommand ClearFilterButtonClickCommand
        {
            get;
        }



        //private void ClearFilter_Click(object sender, RoutedEventArgs e)
        //{
        //    _view.FilterTextBox.Clear();
        //    _displayedPlayers.Clear();
        //    _displayedPlayers.AddRange(_unfilteredPlayers);
        //    _view.ListViewPlayersOverview.ItemsSource = _displayedPlayers;
        //    UpdatePlayersOverviewWindow();
        //}

        //private bool CanExecuteClearFilterClick()

        public PlayersOverviewViewModel(List<Player> players)
        {
            // view commands:
            BackButtonClickCommand = new RelayCommand(execute => BackButtonClick(), canExecute => CanExecuteBackButtonClick());
            ClearFilterButtonClickCommand = new RelayCommand(execute => ClearFilterButtonClick(), canExecute => CanExecuteClearFilterButtonClick());

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
            _amountOfResultsLabelText = Convert.ToString(_displayedPlayers.Count);
            if (_displayedPlayers.Count != _unfilteredPlayers.Count)
                _amountOfResultsLabelText += "-" + Convert.ToString(_unfilteredPlayers.Count);
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

        /// <summary>
        /// When list item is double-clicked, reate a new window with player information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItemPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null && item.IsSelected && item.Content is Player)
            {
                Player player = (Player)item.Content;

                //TODO: Create a new window for player information 
                //var newPlayerWindow = new playerWindow(p.ID);
                //newPlayerWindow.Show();
                //this.Close();
            }
        }

        private bool CanExecuteListViewItemPreviewMouseLeftButtonDown()
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
                _displayedPlayers.Clear();
                _displayedPlayers.AddRange(_unfilteredPlayers);
            }
            else
            {
                FilterPlayers(filterText);
            }

            UpdateAmountOfResultsLabelText();
        }

        private void FilterPlayers(string filterText)
        {
            _displayedPlayers.Clear();
            _displayedPlayers.AddRange(_unfilteredPlayers.Where(player => player.Name.ToLower().Contains(filterText.ToLower())));
        }

        private void ClearFilterButtonClick()
        {
            _displayedPlayers.Clear();
            _displayedPlayers.AddRange(_unfilteredPlayers);

            UpdateAmountOfResultsLabelText();
        }

        private bool CanExecuteClearFilterButtonClick()
        {
            return _displayedPlayers.Count != _unfilteredPlayers.Count;
        }
    }
}
