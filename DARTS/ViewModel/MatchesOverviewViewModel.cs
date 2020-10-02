using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DARTS.Data;
using DARTS.Data.DataObjects;
using DARTS.View;

namespace DARTS.ViewModel
{
    class MatchesOverviewViewModel
    {
        private MatchesOverviewView _view;

        private List<Match> _displayedMatches = new List<Match>();
        private List<Match> _unfilteredMatches = new List<Match>();

        public List<Match> DisplayedMatches
        {
            get { return _displayedMatches; }
            set { _displayedMatches = value; }
        }

        public MatchesOverviewViewModel(MatchesOverviewView view)
        {
            view.DataContext = this;
            _view = view;

            _view.BackButton.Click += BackButton_Click;
            _view.FilterTextBox.TextChanged += FilterTextBox_TextChanged;
            _view.ClearFilterButton.Click += ClearFilter_Click;
            _view.ListViewMatchesOverview.PreviewMouseLeftButtonDown += ListViewItem_PreviewMouseLeftButtonDown;

            GetMatchesOverviewData();
            UpdateMatchesOverviewWindow();
        }

        private void UpdateMatchesOverviewWindow()
        {
            _view.AmountOfResultsLabel.Content = Convert.ToString(_displayedMatches.Count);
            if (_displayedMatches.Count != _unfilteredMatches.Count)
                _view.AmountOfResultsLabel.Content += "-" + Convert.ToString(_unfilteredMatches.Count);

            _view.ListViewMatchesOverview.Items.Refresh();
        }

        private void GetMatchesOverviewData()
        {
            //TODO: To be changed to get data from database: Function to add dummy data to matches overview:
            _displayedMatches.AddRange(DummyData.TempAddListItems());
            _unfilteredMatches.AddRange(_displayedMatches);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected && item.Content is Match)
            {
                Match m = (Match)item.Content;
            }
        }

        #region Filter
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_view.IsLoaded) return;

            string filterText = ((TextBox)e.Source).Text.ToLower();

            if (filterText == "" || filterText == string.Empty)
            {
                _displayedMatches.Clear();
                _displayedMatches.AddRange(_unfilteredMatches);
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
            _displayedMatches.AddRange(_unfilteredMatches.Where(Match => Match.Player1.Name.ToLower().Contains(filterText) || Match.Player2.Name.ToLower().Contains(filterText) || Match.Sets.Count.ToString().ToLower().Contains(filterText)));
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            _view.FilterTextBox.Clear();
            _displayedMatches.Clear();
            _displayedMatches.AddRange(_unfilteredMatches);
            UpdateMatchesOverviewWindow();
        }
        #endregion
    }
}
