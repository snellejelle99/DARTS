﻿using DARTS.Data.DataObjects;
using DARTS.View;
using DARTS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DARTS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MatchesOverview_Click(object sender, RoutedEventArgs e)
        {
            MatchesOverviewView matchesOverviewView= new MatchesOverviewView();
            MatchesOverviewViewModel viewModel = new MatchesOverviewViewModel(new List<Match>());
            matchesOverviewView.DataContext = viewModel;
            matchesOverviewView.Show();
            this.Close();
        }
    }
}
