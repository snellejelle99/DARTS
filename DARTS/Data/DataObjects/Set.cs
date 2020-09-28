using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Set
    {
        private List<Leg> _legs;

        private PlayerEnum _winnningPlayer, _beginnningPlayer;

        private int _player1LegsWon, _player2LegsWon, _numLegs;

        public List<Leg> Legs
        {
            get => _legs;
            set => _legs = value;
        }

        public PlayerEnum WinningPlayer
        {
            get => _winnningPlayer;
            set => _winnningPlayer = value;
        }

        public PlayerEnum BeginningPlayer
        {
            get => _beginnningPlayer;
            set => _beginnningPlayer = value;
        }

        public int Player1LegsWon
        {
            get => _player1LegsWon;
            set => _player1LegsWon = value;
        }

        public int Player2LegsWon
        {
            get => _player2LegsWon;
            set => _player2LegsWon = value;
        }

        public int NumLegs
        {
            get => _numLegs;
            set => _numLegs = value;
        }

        public void Start()
        {
            Legs = new List<Leg>();

            // TODO: impement factory pattern.
            Leg firstLeg = new Leg();
            firstLeg.BeginningPlayer = BeginningPlayer;
            firstLeg.Player1LegScore = 501;
            firstLeg.Player2LegScore = 501;

            Legs.Add(firstLeg);
            firstLeg.Start();
        }

        public Set()
        {

        }
    }
}
