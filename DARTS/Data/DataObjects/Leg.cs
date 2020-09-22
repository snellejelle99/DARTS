﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Leg
    {
        #region BackingStores
        private List<Turn> _turns;

        private PlayerEnum _winnningPlayer;

        private int _player1LegScore, _player2LegScore;
        #endregion

        #region Properties
        public List<Turn> Turns
        {
            get => _turns;
            set => _turns = value;
        }

        public PlayerEnum WinningPlayer
        {
            get => _winnningPlayer;
            set => _winnningPlayer = value;
        }

        public int Player1LegScore
        {
            get => _player1LegScore;
            set => _player1LegScore = value;
        }

        public int Player2LegScore
        {
            get => _player2LegScore;
            set => _player2LegScore = value;
        }
        #endregion

        public Leg(PlayerEnum beginningPlayer)
        {
            Turns = new List<Turn>();

            Turns.Add(new Turn(beginningPlayer));
        }
    }
}