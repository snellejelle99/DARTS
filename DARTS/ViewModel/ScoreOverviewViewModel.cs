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
    class ScoreOverviewViewModel
    {
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

        public ScoreOverviewViewModel()
        {
            Player1 = "Jan";
            Player2 = "Jan2";

            ScorePlayer1 = "100";
            ScorePlayer2 = "100";

            ScoreNeedPlayer1 = "401";
            ScoreNeedPlayer2 = "401";

            SetsPlayer1 = "1";
            SetsPlayer2 = "2";

            LegsPlayer1 = "2";
            LegsPlayer2 = "1";
        }
    }
}
