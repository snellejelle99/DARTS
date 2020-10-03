using System;
using System.Collections.Generic;
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
        }
    }
}
