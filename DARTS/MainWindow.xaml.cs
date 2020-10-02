using DARTS.Data.DataObjects;
using DARTS.ViewModel;
using DARTS.View;
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
        }

        //TEMP: to show PlayersOverviewWindow
        private void TempButtonToPlayersOverview_Click(object sender, RoutedEventArgs e)
        {
            PlayersOverviewView window = new PlayersOverviewView(); //create your new window.
            PlayerOverviewViewModel viewModel = new PlayerOverviewViewModel();
            window.DataContext = viewModel;
            window.Show(); //show the new window.
            this.Close();
        }
    }
}
