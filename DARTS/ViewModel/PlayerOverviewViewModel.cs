using DARTS.Data.DataObjects;
using DARTS.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DARTS.ViewModel
{
    class PlayerOverviewViewModel
    {
        private PlayersOverviewView _view; 

        private List<Player> _displayedPlayers = new List<Player>();
        private List<Player> _unfilteredPlayers = new List<Player>();

        public List<Player> DisplayedPlayers
        {
            get { return _displayedPlayers; }
        }

        public ICommand BackButtonClickCommand
        {
            get;
            set;
        }

        public PlayerOverviewViewModel()
        {
            BackButtonClickCommand = new RelayCommand


            //List<Player> players
            //this._unfilteredPlayers = players;
            //ListViewPlayersOverview.ItemsSource = _displayedPlayers;

            GetPlayersOverviewData();

            // UpdatePlayersOverviewWindow();

            
        }

        /*
         view.DataContext = this;
        _view = view;

        _view.ListViewPlayersOverview.ItemsSource = _displayedPlayers;

        _view.BackButton.Click += BackButton_Click;
        _view.ClearFilterButton.Click += ClearFilter_Click;
        _view.FilterTextBox.TextChanged += FilterTextBox_TextChanged;
        _view.ListViewPlayersOverview.PreviewMouseLeftButtonDown += ListViewItem_PreviewMouseLeftButtonDown;

        _view.Show();
        */
        /// <summary>
        /// Updates window for changes in player items to showcase.
        /// </summary>
        private void UpdatePlayersOverviewWindow()
        {
            // AmountOfResultsLabel
            _view.AmountOfResultsLabel.Content = Convert.ToString(_displayedPlayers.Count);
            if (_displayedPlayers.Count != _unfilteredPlayers.Count)
                _view.AmountOfResultsLabel.Content += "-" + Convert.ToString(_unfilteredPlayers.Count);

            // ListView
            _view.ListViewPlayersOverview.Items.Refresh();
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
                p.PlayerType = PlayerEnum.Player1;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {

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

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_view.IsLoaded) return;

            string filterText = ((TextBox)e.Source).Text;

            Filter(filterText);
        }

        public void Filter(string filterText)
        {
            // TODO: temp
            //Player player = new Player();
            //player.Name = "cool username";
            //player.PlayerType = PlayerEnum.Player2;
            //player.ID = 2;
            //_unfilteredPlayers.Add(player);

            if (filterText == "" || filterText == string.Empty)
            {
                _displayedPlayers.Clear();
                _displayedPlayers.AddRange(_unfilteredPlayers);
                _view.ListViewPlayersOverview.ItemsSource = _displayedPlayers;
            }
            else
            {
                FilterPlayers(filterText);
            }

            UpdatePlayersOverviewWindow();
        }

        private void FilterPlayers(string filterText)
        {
            _displayedPlayers.Clear();
            _displayedPlayers.AddRange(_unfilteredPlayers.Where(player => player.Name.ToLower().Contains(filterText.ToLower())));
            _view.ListViewPlayersOverview.ItemsSource = _displayedPlayers;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            _view.FilterTextBox.Clear();
            _displayedPlayers.Clear();
            _displayedPlayers.AddRange(_unfilteredPlayers);
            _view.ListViewPlayersOverview.ItemsSource = _displayedPlayers;
            UpdatePlayersOverviewWindow();
        }

        private bool CanExecuteClearFilterClick()
        {
            return true;
        }
    }
}
