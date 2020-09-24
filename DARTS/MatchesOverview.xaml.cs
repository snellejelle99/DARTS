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
    /// Interaction logic for MatchesOverviewWindow.xaml
    /// </summary>
    public partial class MatchesOverviewWindow : Window
    {
        private List<Match> _displayedMatches = new List<Match>();
        private List<Match> _unfilteredMatches = new List<Match>();

        public MatchesOverviewWindow()
        {
            InitializeComponent();
            ListViewMatchesOverview.ItemsSource = _displayedMatches;

            GetMatchesOverviewData();

            UpdateMatchesOverviewWindow();
        }

        /// <summary>
        /// Updates window for changes in match items to showcase.
        /// </summary>
        private void UpdateMatchesOverviewWindow()
        {
            // AmountOfResultsLabel
            AmountOfResultsLabel.Content = Convert.ToString(_displayedMatches.Count);
            if (_displayedMatches.Count != _unfilteredMatches.Count)
                AmountOfResultsLabel.Content += "-" + Convert.ToString(_unfilteredMatches.Count);

            // ListView
            ListViewMatchesOverview.Items.Refresh();
        }

        private void GetMatchesOverviewData()
        {
            // TODO: Retrieve matches to display.
            //_unfilteredMatches = get list of matches to display...;
            //_displayedMatches.Clear();
            //_displayedMatches.AddRange(_unfilteredMatches);

            // TODO: Temporary until data retrieval implementation is finished.
            TempAddListItems();
        }

        // TODO: Remove temporary function that is for test purposes.
        private void TempAddListItems()
        {
            for (int i = 0; i < 1; i++)
            {
                _displayedMatches.Add(new Match("Jaap", "Klaas", 2, 2, PlayerEnum.Player1));
                _displayedMatches.Add(new Match("Gerard", "Joop", 3, 3, PlayerEnum.Player2));
                _displayedMatches.Add(new Match("Sjaak", "Koos", 1, 1, PlayerEnum.Player1));
            }

            _unfilteredMatches.AddRange(_displayedMatches);
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

        /// <summary>
        /// When list item is double-clicked, reate a new window with match information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected && item.Content is Match)
            {
                Match m = (Match)item.Content;

                //TODO: Create a new window for match information 
                //var newMatchWindow = new MatchWindow(p.ID);
                //newMatchWindow.Show();
                //this.Close();
            }
        }

        #region Filter
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded) return;

            string filterText = ((TextBox)e.Source).Text;

            // TODO: temp
            _unfilteredMatches.Add(new Match("Jozef", "Nikolaas", 1, 1, PlayerEnum.Player2));

            if (filterText == "" || filterText == string.Empty)
            {
                _displayedMatches.Clear();
                _displayedMatches.AddRange(_unfilteredMatches);
                ListViewMatchesOverview.ItemsSource = _displayedMatches;
            }
            else
            {
                FilterMatches(filterText);
            }

            UpdateMatchesOverviewWindow();
        }

        private void FilterMatches(string filterText)
        {
            _displayedMatches.Clear();
            _displayedMatches.AddRange(_unfilteredMatches.Where(Match => Match.Player1.Name.Contains(filterText)));
            ListViewMatchesOverview.ItemsSource = _displayedMatches;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Clear();
            _displayedMatches.Clear();
            _displayedMatches.AddRange(_unfilteredMatches);
            ListViewMatchesOverview.ItemsSource = _displayedMatches;
            UpdateMatchesOverviewWindow();
        }
        #endregion
    }

}
