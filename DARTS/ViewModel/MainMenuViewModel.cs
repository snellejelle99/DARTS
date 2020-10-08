using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DARTS.Data;
using DARTS.Data.DataObjects;
using DARTS.View;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    class MainMenuViewModel
    {
        public ICommand StartMatchButtonClickCommand { get; }
        public ICommand PlayerOverviewButtonClickCommand { get; }
        public ICommand MatchOverviewButtonClickCommand { get; }

        public MainMenuViewModel()
        {
            StartMatchButtonClickCommand = new RelayCommand(execute => StartMatchButton_Click(execute), canExecute => CanExecuteStartMatchButtonClick());
            PlayerOverviewButtonClickCommand = new RelayCommand(execute => PlayerOverviewButton_Click(execute), canExecute => CanExecutePlayerOverviewButtonClick());
            MatchOverviewButtonClickCommand = new RelayCommand(execute => MatchOverviewButton_Click(), canExecute => CanExecuteMatchOverviewButtonClick());
        }

        private void StartMatchButton_Click(object parameter)
        {
            StartMatchView startMatchWindow = new StartMatchView
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            StartMatchViewModel startMatchViewModel = new StartMatchViewModel();
            startMatchWindow.DataContext = startMatchViewModel;
            startMatchWindow.Show();

            (parameter as Window)?.Close();
        }

        private bool CanExecuteStartMatchButtonClick()
        {
            return true;
        }

        private void PlayerOverviewButton_Click(object parameter)
        {
            PlayersOverviewView playerOverviewWindow = new PlayersOverviewView
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            PlayersOverviewViewModel playerOverviewViewModel = new PlayersOverviewViewModel(new List<Player>());
            playerOverviewWindow.DataContext = playerOverviewViewModel;
            playerOverviewWindow.Show();

            (parameter as Window)?.Close();
        }

        private bool CanExecutePlayerOverviewButtonClick()
        {
            return true;
        }

        private void MatchOverviewButton_Click()
        {

        }

        private bool CanExecuteMatchOverviewButtonClick()
        {
            return true; 
        }
    }
}
