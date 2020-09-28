using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace DARTS.Data.DataObjects
{
    public class Match
    {
        #region BackingStores
        private PlayerEnum _winnningPlayer, _beginningPlayer;

        private Player _player1;

        private Player _player2;

        private List<Set> _sets;

        private int _numSets, _numLegs;
        #endregion

        #region Properties
        public Player Player1
        {
            get => _player1;
            set => _player1 = value;
        }

        public Player Player2
        {
            get => _player2;
            set => _player2 = value;
        }

        public PlayerEnum WinningPlayer
        {
            get => _winnningPlayer;
            set => _winnningPlayer = value;
        }

        public PlayerEnum BeginningPlayer
        {
            get => _beginningPlayer;
            set => _beginningPlayer = value;
        }

        public List<Set> Sets
        {
            get => _sets;
            set => _sets = value;
        }

        public int NumSets
        {
            get => _numSets;
            set => _numSets = value;
        }

        public int NumLegs
        {
            get => _numLegs;
            set => _numLegs = value;
        }
        #endregion

        public Match()
        {

        }
    }
}
