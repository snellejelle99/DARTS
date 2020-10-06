using DARTS.View;
using DARTS.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DARTS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ScoreInputView window = new ScoreInputView
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            ScoreInputViewModel viewModel = new ScoreInputViewModel();

            window.DataContext = viewModel;
            window.Show();       
        }
    }
}
