using DARTS.View;
using DARTS.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DARTS
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainMenuView window = new MainMenuView();
            MainMenuViewModel viewModel = new MainMenuViewModel(window);
        }
    }
}