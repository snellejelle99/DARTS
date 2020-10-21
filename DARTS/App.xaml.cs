using System.Windows;
using DARTS.Data;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
using DARTS.View;
using DARTS.ViewModel;

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

            //window.DataContext = viewModel;

            //window.Show();

            GameInstance.Instance.MainWindow.Show();
        }
    }
}

