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
        public ICommand OptionsButtonClickCommand { get; }

        public MainMenuViewModel()
        {
            StartMatchButtonClickCommand = new RelayCommand(execute => StartMatchButton_Click(execute), canExecute => CanExecuteStartMatchButtonClick());
            PlayerOverviewButtonClickCommand = new RelayCommand(execute => PlayerOverviewButton_Click(execute), canExecute => CanExecutePlayerOverviewButtonClick());
            MatchOverviewButtonClickCommand = new RelayCommand(execute => MatchOverviewButton_Click(execute), canExecute => CanExecuteMatchOverviewButtonClick());
            OptionsButtonClickCommand = new RelayCommand(execute => OptionsButton_Click(execute), canExecute => CanExecuteOptionsButtonClick());
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

        private void MatchOverviewButton_Click(object parameter)
        {
            MatchesOverviewView matchesOverviewWindow = new MatchesOverviewView
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            MatchesOverviewViewModel matchesOverviewViewModel = new MatchesOverviewViewModel(new List<Match>());
            matchesOverviewWindow.DataContext = matchesOverviewViewModel;
            matchesOverviewWindow.Show();

            (parameter as Window)?.Close();
        }

        private bool CanExecuteMatchOverviewButtonClick()
        {
            return true; 
        }

        private void OptionsButton_Click(object parameter)
        {
            OptionsMenuView optionsMenuWindow = new OptionsMenuView
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            OptionsMenuViewModel optionsMenuViewModel = new OptionsMenuViewModel();
            optionsMenuWindow.DataContext = optionsMenuViewModel;
            optionsMenuWindow.Show();

            (parameter as Window)?.Close();
        }

        private bool CanExecuteOptionsButtonClick()
        {
            return true;
        }
    }
}
