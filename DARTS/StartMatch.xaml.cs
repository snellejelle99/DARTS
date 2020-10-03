using DARTS.Data.DataObjects;
using System.Windows;

namespace DARTS
{
    /// <summary>
    /// Interaction logic for StartMatch.xaml
    /// </summary>
    public partial class StartMatch : Window
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int NumSets { get; set; }
        public int NumLegs { get; set; }
       
        public StartMatch()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;
        }
    }
}
