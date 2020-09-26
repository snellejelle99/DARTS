using DARTS.Data.DataObjects;
using System.Windows;

namespace DARTS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Placeholder match object om validation te testen
            Match m = new Match("Mark", "Koos", 2, 2, PlayerEnum.Player1);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Matchobject wordt gebind aan main canvas in MainWindow.xaml
            MainCanvas.DataContext = m;
        }
    }
}
