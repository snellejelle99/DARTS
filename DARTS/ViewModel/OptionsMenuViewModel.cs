using System.IO;
using System.Windows;
using System.Windows.Input;
using DARTS.Data.DataBase;
using DARTS.Data.Singletons;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    class OptionsMenuViewModel
    {
        public ICommand ResetDatabaseButtonClickCommand { get; }
        public ICommand GoBackButtonClickCommand { get; }

        public OptionsMenuViewModel()
        {
            ResetDatabaseButtonClickCommand = new RelayCommand(execute => ResetDatabaseButton_Click(), canExecute => File.Exists(DataBaseProvider.Instance.DBFileName));
            GoBackButtonClickCommand = new RelayCommand(execute => GoBackButton_Click());
        }

        private void ResetDatabaseButton_Click()
        {
            MessageBoxResult result = MessageBox.Show(GameInstance.Instance.MainWindow, "Are you sure you want to reset the database? \nThis action will delete all saved data.", "Reset database", MessageBoxButton.OKCancel);
            switch(result)
            {
                case MessageBoxResult.OK:
                    DataBaseProvider.Instance.DeleteDatabase();
                    MessageBox.Show(GameInstance.Instance.MainWindow, "Database has been reset.", "Operation Completed");
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void GoBackButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToMainMenu();
        }
    }
}
