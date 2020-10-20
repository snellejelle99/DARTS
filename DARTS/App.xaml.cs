using System.Windows;
using DARTS.Data.Singletons;

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


            ScoreInputView view = new ScoreInputView();

            ScoreInputViewModel viewModel = new ScoreInputViewModel(DummyData.GetDummyMatch());

            view.DataContext = viewModel;
            view.Show();
            GameInstance.Instance.MainWindow.Show();
        }
    }
}

