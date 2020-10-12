using System;
using DARTS.View;
using DARTS.ViewModel;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DARTS.Data;

namespace DARTS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //MainMenuView window = new MainMenuView
            //{
            //    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            //};
            //MainMenuViewModel viewModel = new MainMenuViewModel();

            ScoreInputView window = new ScoreInputView
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            ScoreInputViewModel viewModel = new ScoreInputViewModel(DummyData.GetDummyMatch());

            window.DataContext = viewModel;

            window.Show();
        }
    }
}

