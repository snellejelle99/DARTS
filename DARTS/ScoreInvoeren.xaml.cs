using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DARTS
{
    /// <summary>
    /// Interaction logic for ScoreInvoeren.xaml
    /// </summary>
    public partial class ScoreInvoeren : Window
    {
        public string[] ThrowTypes { get; set; }
        public ScoreInvoeren()
        {
            InitializeComponent();
            ThrowTypes = new string[] { "Single", "Double", "Triple", "Misser" };
            DataContext = this;
        }

        private void submitScore_Click(object sender, RoutedEventArgs e)
        {
            //save score
        }

        private void scoreInputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //accept only numbers 0-9
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        
    }

   
}
