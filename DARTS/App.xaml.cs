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
            GameInstance.Instance.MainWindow.Show();
        }
    }
}

