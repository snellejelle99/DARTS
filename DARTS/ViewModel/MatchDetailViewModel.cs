using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Accessibility;
using DARTS.Data;
using DARTS.Data.DataObjects;
using DARTS.View;
using DARTS.ViewModel.Command;

namespace DARTS.ViewModel
{
    public class MatchDetailViewModel : INotifyPropertyChanged
    {
        private Match specifiedMatch;
        List<string> setDetails = new List<string>();
        List<string> legDetails = new List<string>();
        private string _selectedItem;
        private string player1Name;
        private string player2Name;
        private string setsWon;
        private int totalAmount180s = 0;
        private int player1Amount180s = 0;
        private int player2Amount180s = 0;
        private int player1AverageScore = 0;
        private int player2AverageScore = 0;
        public ICommand OpenSetDetailsClickCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        public MatchDetailViewModel(Match match)
        {
            specifiedMatch = match;
            Player1Name = specifiedMatch.Player1.Name;
            Player2Name = specifiedMatch.Player2.Name;
            SetsWon = specifiedMatch.Player1SetsWon + " - " + specifiedMatch.Player2SetsWon;
            OpenSetDetailsClickCommand = new RelayCommand(execute => OpenSetDetailsButton_Click(), canExecute => CanExecuteSetDetailsButtonClick());
            CalculateAmountof180s();
            AverageScorePerPlayer();
            SetDetailsSetup();
        }

        public string SetsWon
        {
            get { return setsWon; }
            set
            {
                setsWon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SetsWon)));
            }
        }
        public int TotalAmount180s
        {
            get { return totalAmount180s; }
            set
            {
                totalAmount180s = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalAmount180s)));
            }
        }
        public int Player1Amount180s
        {
            get { return player1Amount180s; }
            set
            {
                player1Amount180s = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player1Amount180s)));
            }
        }
        public int Player2Amount180s
        {
            get { return player2Amount180s; }
            set
            {
                player2Amount180s = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player2Amount180s)));
            }
        }
        public int Player1AverageScore
        {
            get { return player1AverageScore; }
            set
            {
                player1AverageScore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player1AverageScore)));
            }
        }
        public int Player2AverageScore
        {
            get { return player2AverageScore; }
            set
            {
                player2AverageScore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player2AverageScore)));
            }
        }
        public string Player1Name
        {
            get { return player1Name; }
            set
            {
                player1Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Player1Name));
            }
        }

        public string Player2Name
        {
            get { return player2Name; }
            set
            {
                player2Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Player2Name));
            }
        }

        public Match SpecifiedMatch
        {
            get { return specifiedMatch; }
        }

        public List<string> SetDetails
        {
            get { return setDetails; }
        }
        public List<string> LegDetails
        {
            get { return legDetails; }
        }
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        private void AverageScorePerPlayer()
        {
            int player1TotalScore = 0;
            int player1AmountOfThrows = 0;
            int player2TotalScore = 0;
            int player2AmountOfThrows = 0;
            for (int i = 0; i < specifiedMatch.Sets.Count; i++)
            {
                for (int j = 0; j < specifiedMatch.Sets[i].Legs.Count; j++)
                {
                    for (int k = 0; k < specifiedMatch.Sets[i].Legs[j].Turns.Count; k++)
                    {
                        player1TotalScore += specifiedMatch.Sets[i].Legs[j].Turns[k].PlayerTurn == PlayerEnum.Player1 ? specifiedMatch.Sets[i].Legs[j].Turns[k].ThrownPoints : 0;
                        player1AmountOfThrows += specifiedMatch.Sets[i].Legs[j].Turns[k].PlayerTurn == PlayerEnum.Player1 ? 1 : 0;
                        player2TotalScore += specifiedMatch.Sets[i].Legs[j].Turns[k].PlayerTurn == PlayerEnum.Player2 ? specifiedMatch.Sets[i].Legs[j].Turns[k].ThrownPoints : 0;
                        player2AmountOfThrows += specifiedMatch.Sets[i].Legs[j].Turns[k].PlayerTurn == PlayerEnum.Player2 ? 1 : 0;
                    }
                }
            }
            if (player1AmountOfThrows != 0)
                player1AverageScore = player1TotalScore / player1AmountOfThrows;
            if (player2AmountOfThrows != 0)
                player2AverageScore = player2TotalScore / player2AmountOfThrows;
        }

        private void CalculateAmountof180s()
        {
            for (int i = 0; i < specifiedMatch.Sets.Count; i++)
            {
                for (int j = 0; j < specifiedMatch.Sets[i].Legs.Count; j++)
                {
                    for (int k = 0; k < specifiedMatch.Sets[i].Legs[j].Turns.Count; k++)
                    {
                        if (specifiedMatch.Sets[i].Legs[j].Turns[k].ThrownPoints == 180)
                        {
                            player1Amount180s += specifiedMatch.Sets[i].Legs[j].Turns[k].PlayerTurn == PlayerEnum.Player1 ? 1 : 0;
                            player2Amount180s += specifiedMatch.Sets[i].Legs[j].Turns[k].PlayerTurn == PlayerEnum.Player2 ? 1 : 0;
                            totalAmount180s = player1Amount180s + player2Amount180s;
                        }
                    }
                }
            }
        }

        private void SetDetailsSetup()
        {
            int totalThrown = 0;
            int amountOfThrows = 0;
            for (int i = 0; i < specifiedMatch.Sets.Count; i++)
            {
                for (int j = 0; j < specifiedMatch.Sets[i].Legs.Count; j++)
                {
                    for (int k = 0; k < specifiedMatch.Sets[i].Legs[j].Turns.Count; k++)
                    {
                        totalThrown += specifiedMatch.Sets[i].Legs[j].Turns[k].ThrownPoints;
                        amountOfThrows += 1;
                    }
                }
                setDetails.Add(string.Format("Set {0}: {1}-{2}: avg. thrown {3}", i + 1, specifiedMatch.Sets[i].Player1LegsWon, specifiedMatch.Sets[i].Player2LegsWon, totalThrown / amountOfThrows));
            }
        }
        private void OpenSetDetailsButton_Click()
        {
            //logic for displaying set details in leg listview
        }
        private bool CanExecuteSetDetailsButtonClick()
        {
            return _selectedItem != null;
        }
    }
}
