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
    public partial class PlayersOverviewWindow : Window
    {
        private List<Player> _displayedPlayers = new List<Player>();
        private List<Player> _unfilteredPlayers = new List<Player>();

        public PlayersOverviewWindow()
        {
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
            TempAddListItems();
        }

        // TODO: Remove temporary function that is for test purposes.
        private void TempAddListItems()
        {
            for (int i = 0; i < 1; i++)
            {
                _displayedPlayers.Add(new Player("player1", PlayerEnum.Player1));
                _displayedPlayers.Add(new Player("player2", PlayerEnum.Player1));
                _displayedPlayers.Add(new Player("player3", PlayerEnum.Player2));
            }
            for (int i = 0; i < _displayedPlayers.Count; i++)
            {
                _displayedPlayers[i].ID = i;
            }

            _unfilteredPlayers.AddRange(_displayedPlayers);
        }

        /// <summary>
        /// Whene back button is pressed, returns user to main window. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Return to main window
            var newMainWindow = new MainWindow();
            newMainWindow.Show();
            this.Close();
        }

        #region Filter
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded) return;
                
            string filterText = ((TextBox)e.Source).Text;
            Debug.WriteLine(filterText);

            // TODO: temp
            _unfilteredPlayers.Add(new Player("cool username", PlayerEnum.Player2));

            if (filterText == "" || filterText == string.Empty)
            {
                _displayedPlayers.Clear();
                _displayedPlayers.AddRange(_unfilteredPlayers);
                ListViewPlayersOverview.ItemsSource = _displayedPlayers;
            } else
            {
                FilterPlayers(filterText);
            }
            
            UpdatePlayersOverviewWindow();
        }

        private void FilterPlayers(string filterText)
        {
            _displayedPlayers.Clear();
            _displayedPlayers.AddRange(_unfilteredPlayers.Where(player => player.Name.Contains(filterText)));
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
