using DARTS.Data.DataObjects;
using DARTS.ViewModel;
using System.Collections.Generic;
using System.Windows;

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
            DataContext = new MainMenuViewModel();
        }

        public void ChangeToMainMenu()
        {
            DataContext = new MainMenuViewModel();
        }

        public void ChangeToStartMatch()
        {
            DataContext = new StartMatchViewModel();
        }

        public void ChangeToMatchesOverview()
        {
            DataContext = new MatchesOverviewViewModel(new List<Match>());
        }

        public void ChangeToOptionsMenuView()
        {
            DataContext = new OptionsMenuViewModel();
        }

        public void ChangeToPlayerOverviewView()
        {
            DataContext = new PlayersOverviewViewModel(new List<Player>());
        }

        public void ChangeToScoreInputView(Match match)
        {
            DataContext = new ScoreInputViewModel(match);
        }

        public void ChangeToPlayerMatchStatisticsView(List<Match> matches)
        {
            DataContext = new PlayerMatchStatisticsViewModel(matches);
        }
    }
}
