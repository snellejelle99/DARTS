using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    class StartMatchViewModel
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int NumSets { get; set; }
        public int NumLegs { get; set; }
        public PlayerEnum[] PlayerEnums { get; set; }
        public PlayerEnum SelectedPlayerEnum { get; set; }

        public ICommand StartMatchButtonClickCommand { get; }
        public ICommand BackToMainMenuButtonClickCommand { get; }
    
        public StartMatchViewModel()
        {
            StartMatchButtonClickCommand = new RelayCommand(execute => StartMatchButton_Click(), canExecute => CanExecuteStartMatchButtonClick());
            BackToMainMenuButtonClickCommand = new RelayCommand(execute => BackToMainMenuButton_Click(execute), canExecute => CanExecuteBackToMainMenuButtonClick());

            PlayerEnums = (PlayerEnum[])Enum.GetValues(typeof(PlayerEnum));
        }

        private void StartMatchButton_Click()
        {
            // Nav to match screen
        }

        private bool CanExecuteStartMatchButtonClick()
        {
            return Player1 != null && Player2 != null && NumSets != 0 && NumLegs != 0;
        }

        private void BackToMainMenuButton_Click(object parameter)
        {
            MainMenuView mainMenuWindow = new MainMenuView
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            MainMenuViewModel mainMenuWindowModel = new MainMenuViewModel();
            mainMenuWindow.DataContext = mainMenuWindowModel;
            mainMenuWindow.Show();

            (parameter as Window).Close();
        }

        private bool CanExecuteBackToMainMenuButtonClick()
        {
            return true;
        }
    }
}
