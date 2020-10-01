using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DARTS.Windows
{
    /// <summary>
    /// Interaction logic for PlayersOverviewWindow.xaml
    /// </summary>
    ///
    public partial class PlayersOverviewWindow : Window
    {
        public List<Player> _displayedPlayers = new List<Player>();
        private List<Player> _unfilteredPlayers = new List<Player>();

        public PlayersOverviewWindow(List<Player> players)
        {
            this._unfilteredPlayers = players;
            InitializeComponent();
            ListViewPlayersOverview.ItemsSource = _displayedPlayers;

            GetPlayersOverviewData();

            UpdatePlayersOverviewWindow();
        }

        /// <summary>
        /// Updates window for changes in player items to showcase.
        /// </summary>
        private void UpdatePlayersOverviewWindow()
        {
            // AmountOfResultsLabel
            AmountOfResultsLabel.Content = Convert.ToString(_displayedPlayers.Count);
            if (_displayedPlayers.Count != _unfilteredPlayers.Count) 
                AmountOfResultsLabel.Content += "-" + Convert.ToString(_unfilteredPlayers.Count);
            
            // ListView
            ListViewPlayersOverview.Items.Refresh();
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
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Return to main window
            MainWindow newMainWindow = new MainWindow();
            newMainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// When list item is double-clicked, reate a new window with player information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        #region Filter
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded) return;
                
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
                ListViewPlayersOverview.ItemsSource = _displayedPlayers;
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
            ListViewPlayersOverview.ItemsSource = _displayedPlayers;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Clear();
            _displayedPlayers.Clear();
            _displayedPlayers.AddRange(_unfilteredPlayers);
            ListViewPlayersOverview.ItemsSource = _displayedPlayers;
            UpdatePlayersOverviewWindow();
        }
        #endregion
    }

}
