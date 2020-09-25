using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DARTS.User_Controls
{
    /// <summary>
    /// Interaction logic for ScoreOverzicht.xaml
    /// </summary>
    public partial class ScoreOverzicht : UserControl
    {
        public ScoreOverzicht()
        {
            InitializeComponent();
            this.DataContext = this;

    

        }
        
        public string Player1 { get; set; }
        public string Player2 { get; set; }

        public int MaxLength { get; set; }
       

    }
}
