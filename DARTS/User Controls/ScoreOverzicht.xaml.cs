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
        public string ScorePlayer1 { get; set; }
        public string ScoreNeedPlayer1 { get; set; }
        public string SetsPlayer1 { get; set; }
        public string LegsPlayer1 { get; set; }

        public string Player2 { get; set; }
        public string ScorePlayer2 { get; set; }
        public string ScoreNeedPlayer2 { get; set; }
        public string SetsPlayer2 { get; set; }
        public string LegsPlayer2 { get; set; }
       
    }
}
