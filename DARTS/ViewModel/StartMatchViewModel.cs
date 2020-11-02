using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DARTS.Data;
using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    class StartMatchViewModel
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int NumSets { get; set; }
        public int NumLegs { get; set; }
        public bool Is501LegScore { get; set; }
        public PlayerEnum[] PlayerEnums { get; set; }
        public PlayerEnum SelectedPlayerEnum { get; set; }

        public ICommand StartMatchButtonClickCommand { get; }
        public ICommand BackToMainMenuButtonClickCommand { get; }
    
        private PlayerFactory PlayerFactory { get; set; }
        private MatchFactory MatchFactory { get; set; }

        public StartMatchViewModel()
        {
            StartMatchButtonClickCommand = new RelayCommand(execute => StartMatchButton_Click(), canExecute => CanExecuteStartMatchButtonClick());
            BackToMainMenuButtonClickCommand = new RelayCommand(execute => BackToMainMenuButton_Click());

            Is501LegScore = true;
            PlayerEnums = (PlayerEnum[])Enum.GetValues(typeof(PlayerEnum));
            PlayerFactory = new PlayerFactory();
            MatchFactory = new MatchFactory();
        }

        private void StartMatchButton_Click()
        {
            Player player1, player2;

            List<DataObjectBase> results = PlayerFactory.Get("Name", Player1);
            results.AddRange(PlayerFactory.Get("Name", Player2));
            player1 = (Player)results.Find(x => ((Player)x).Name == Player1);
            player2 = (Player)results.Find(x => ((Player)x).Name == Player2);

            if (player1 == default)
            {
                player1 = (Player)PlayerFactory.Spawn();
                player1.Name = Player1;
            }

            if (player2 == default)
            {
                player2 = (Player)PlayerFactory.Spawn();
                player2.Name = Player2;
            }

            Match match = (Match)MatchFactory.Spawn();
            match.Player1 = player1;
            match.Player2 = player2;
            match.BeginningPlayer = SelectedPlayerEnum;
            match.MatchState = PlayState.InProgress;
            match.NumSets = NumSets;
            match.NumLegs = NumLegs;
            match.PointsPerLeg = Is501LegScore ? 501 : 301;

            match.Start();
            GameInstance.Instance.MainWindow.ChangeToScoreInputView(match);
        }

        private bool CanExecuteStartMatchButtonClick()
        {
            return Player1 != null && Player2 != null && NumSets != 0 && NumLegs != 0;
        }

        private void BackToMainMenuButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToMainMenu();
        }
    }
}
