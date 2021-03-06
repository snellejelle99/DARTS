﻿using System;
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

namespace DARTS.Views
{
    /// <summary>
    /// Interaction logic for ScoreInputView.xaml
    /// </summary>
    public partial class ScoreInputView : UserControl
    {
        public ScoreInputView()
        {
            InitializeComponent();
        }

        private void SelectScore(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }
    }
}
