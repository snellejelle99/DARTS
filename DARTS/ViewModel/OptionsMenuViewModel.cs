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
    class OptionsMenuViewModel
    {
        public ICommand ResetDatabaseButtonClickCommand { get; }
        public ICommand GoBackButtonClickCommand { get; }

        public OptionsMenuViewModel()
        {
            ResetDatabaseButtonClickCommand = new RelayCommand(execute => ResetDatabaseButton_Click(), canExecute => CanExecuteResetDatabaseButtonClick());
            GoBackButtonClickCommand = new RelayCommand(execute => GoBackButton_Click());
        }

        private void ResetDatabaseButton_Click()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset the database? \nThis action will delete all saved data.", "Reset database", MessageBoxButton.OKCancel);
            switch(result)
            {
                case MessageBoxResult.OK:
                    //TO DO:
                    //Add database reset action. 
                    MessageBox.Show("Database has been reset.");
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        public bool CanExecuteResetDatabaseButtonClick()
        {
            //TO DO:
            //Disable button when database is empty.
            return false;
        }

        private void GoBackButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToMainMenu();
        }
    }
}
