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

        public int NumLegs
        {
            get => _numLegs;
            set
            {
                if (value % 2 == 0)
                {
                    throw new ArgumentException("Number of legs cannot be an even number.");
                }
                else _numLegs = value;
            }
        }

        public int Player1LegsWon
        {
            get => _player1LegsWon;
            set
            {
                if (value > NumLegs)
                {
                    throw new ArgumentException("Legs won by a player can not be bigger than the number of legs in the set.");
                }
                else _player1LegsWon = value;
            }
        }

        public int Player2LegsWon
        {
            get => _player2LegsWon;
            set
            {
                if (value > NumLegs)
                {
                    throw new ArgumentException("Legs won by a player can not be bigger than the number of legs in the set.");
                }
                else _player2LegsWon = value;
            }
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

        public PlayerEnum CheckWin()
        {
            PlayerEnum winner = Legs[Legs.Count - 1].CheckWin();

            if (winner != PlayerEnum.None)
            {
                if (winner == PlayerEnum.Player1)
                    Player1LegsWon++;

                else if (winner == PlayerEnum.Player2)
                    Player2LegsWon++;


                if (Player1LegsWon > (NumLegs / 2))
                {
                    WinningPlayer = PlayerEnum.Player1;
                }

                else if (Player2LegsWon > (NumLegs / 2))
                {
                    WinningPlayer = PlayerEnum.Player2;
                }

                else WinningPlayer = PlayerEnum.None;
            }

            else WinningPlayer = PlayerEnum.None;

            return WinningPlayer;
        }

        public void ChangeTurn()
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

        public Set()
        {

        }
    }
}
