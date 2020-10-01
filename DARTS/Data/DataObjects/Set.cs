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

        private const int PlayerPoints = 501;

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
            firstLeg.Player1LegScore = PlayerPoints;
            firstLeg.Player2LegScore = PlayerPoints;

            Legs.Add(firstLeg);
            firstLeg.Start();
        }

        public void ChangeTurn()
        {
            if(Legs.Count > 0)
            {
                //If nobody has won the leg yet change the turn.
                if (Legs[Legs.Count - 1].WinningPlayer == PlayerEnum.None)
                {
                    Legs[Legs.Count - 1].ChangeTurn(); 
                }

                //If someone has won it start a new Leg and let the other player begin. Then start the leg.
                else
                {
                    // TODO: impement factory pattern.
                    Leg nextLeg = new Leg();
                    nextLeg.BeginningPlayer = Legs[Legs.Count - 1].BeginningPlayer == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
                    nextLeg.Player1LegScore = PlayerPoints;
                    nextLeg.Player2LegScore = PlayerPoints;

                    Legs.Add(nextLeg);
                    nextLeg.Start();
                }
            }
        }

        public Set()
        {

        }
    }
}
