using DARTS.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DARTS.ViewModel
{
    public class ScoreInputViewModel
    {
        public string[] ThrowTypes { get; set; }

        public int ScoreInput { get; set; }

        public string SelectedThrowType { get; set; }

        public ICommand SubmitScoreButtonClickCommand { get; }

        public ScoreInputViewModel()
        {
            ThrowTypes = new string[] { "Single", "Double", "Triple" };
            SubmitScoreButtonClickCommand = new RelayCommand(execute => SubmitScoreButtonClick(), canExecute => CanExecuteSubmitScoreButtonClick());
        }

        private void SubmitScoreButtonClick()
        {
            //submit score
        }

        private bool CanExecuteSubmitScoreButtonClick()
        {
            return ScoreInput >= 0;
        }
    }
}
