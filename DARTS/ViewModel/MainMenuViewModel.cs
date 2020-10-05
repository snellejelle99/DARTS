using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Text;
using System.Windows;
using DARTS.View;

namespace DARTS.ViewModel
{
    public class MainMenuViewModel
    {
        private MainMenuView _view;

        public MainMenuViewModel(MainMenuView view)
        {
            view.DataContext = this;
            _view = view;

            _view.StartMatchButton.Click += ButtonClick;

            _view.Show(); 
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            StartMatch startMatchWindow = new StartMatch();
            startMatchWindow.Show();
            _view.Close();
=======
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
            StartMatchButtonClickCommand = new RelayCommand(execute => StartMatchButton_Click(), canExecute => CanExecuteStartMatchButtonClick());
            PlayerOverviewButtonClickCommand = new RelayCommand(execute => PlayerOverviewButton_Click(), canExecute => CanExecutePlayerOverviewButtonClick());
            MatchOverviewButtonClickCommand = new RelayCommand(execute => MatchOverviewButton_Click(), canExecute => CanExecuteMatchOverviewButtonClick());

        }

        private void StartMatchButton_Click()
        {

        }

        private bool CanExecuteStartMatchButtonClick()
        {
            return true;
        }

        private void PlayerOverviewButton_Click()
        {

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
>>>>>>> master
        }
    }
}
