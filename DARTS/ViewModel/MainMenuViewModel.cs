using System.Linq;
using System.Windows.Input;
using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using DARTS.Data.Singletons;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    class MainMenuViewModel
    {
        public ICommand StartMatchButtonClickCommand { get; }
        public ICommand PlayerOverviewButtonClickCommand { get; }
        public ICommand MatchOverviewButtonClickCommand { get; }
        public ICommand OptionsButtonClickCommand { get; }

        public MainMenuViewModel()
        {
            StartMatchButtonClickCommand = new RelayCommand(execute => StartMatchButton_Click());
            PlayerOverviewButtonClickCommand = new RelayCommand(execute => PlayerOverviewButton_Click());
            MatchOverviewButtonClickCommand = new RelayCommand(execute => MatchOverviewButton_Click());
            OptionsButtonClickCommand = new RelayCommand(execute => OptionsButton_Click());
        }

        private void StartMatchButton_Click()
        {
            MatchFactory factory = new MatchFactory();
            Match notFinished = (Match)factory.Get(MatchFieldNames.MatchState, PlayState.InProgress).FirstOrDefault();

            if (notFinished != null) GameInstance.Instance.MainWindow.ChangeToScoreInputView(notFinished);
            else GameInstance.Instance.MainWindow.ChangeToStartMatch();
        }

        private void PlayerOverviewButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToPlayerOverviewView();
        }

        private void MatchOverviewButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToMatchesOverview();
        }

        private void OptionsButton_Click()
        {
            GameInstance.Instance.MainWindow.ChangeToOptionsMenuView();
        }
    }
}
