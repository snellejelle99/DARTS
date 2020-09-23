using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Set
    {
        private List<Leg> _legs;

        private PlayerEnum _winnningPlayer;

        private int _player1LegsWon, _player2LegsWon;

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

        public Set(int numLegs, PlayerEnum beginningPlayer)
        {
            Legs = new List<Leg>();

            for (int i = 0; i < numLegs; i++)
            {
                Legs.Add(new Leg(beginningPlayer));
            }
        }
    }
}
