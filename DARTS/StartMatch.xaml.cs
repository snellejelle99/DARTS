using DARTS.Data.DataObjects;
using System.Windows;

namespace DARTS
{
    /// <summary>
    /// Interaction logic for StartMatch.xaml
    /// </summary>
    public partial class StartMatch : Window
    {
        public StartMatch()
        {
            InitializeComponent();
            // Placeholder match object om validation te testen
            Match m = new Match();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Matchobject wordt gebind aan main canvas in MainWindow.xaml
            MainCanvas.DataContext = m;
        }
    }
}
