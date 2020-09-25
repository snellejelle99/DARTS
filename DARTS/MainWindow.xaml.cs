using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
