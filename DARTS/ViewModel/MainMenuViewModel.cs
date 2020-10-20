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
using DARTS.Data.Singletons;
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
            StartMatchButtonClickCommand = new RelayCommand(execute => StartMatchButton_Click());
            PlayerOverviewButtonClickCommand = new RelayCommand(execute => PlayerOverviewButton_Click());
            MatchOverviewButtonClickCommand = new RelayCommand(execute => MatchOverviewButton_Click());
            OptionsButtonClickCommand = new RelayCommand(execute => OptionsButton_Click());
        }

        private void StartMatchButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToStartMatch();
        }

        private void PlayerOverviewButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToPlayerOverviewView();
        }

        private void MatchOverviewButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToMatchesOverview();
        }

        private void OptionsButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToOptionsMenuView();
        }
    }
}
