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

        public void Start()
        {
            Sets = new List<Set>();

            if (BeginningPlayer.Equals(PlayerEnum.None))
            {
                BeginningPlayer = ChooseRandomPlayer();
            }

            // TODO: impement factory pattern.
            Set firstSet = new Set();
            firstSet.BeginningPlayer = BeginningPlayer;
            firstSet.NumLegs = NumLegs;

            Sets.Add(firstSet);
            firstSet.Start();
        }

        public void ChangeTurn()
        {
            if(Sets.Count > 0)
            {
                if(Sets[Sets.Count - 1].WinningPlayer == PlayerEnum.None)
                {
                    Sets[Sets.Count - 1].ChangeTurn();
                }

                else
                {
                    Set nextSet = new Set();
                    nextSet.BeginningPlayer = Sets[Sets.Count - 1].BeginningPlayer == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
                    nextSet.NumLegs = NumLegs;

                    Sets.Add(nextSet);
                    nextSet.Start();
                }
            }
        }

        private PlayerEnum ChooseRandomPlayer()
        {
            // Choose randomly between PlayerEnum.Player1 and PlayerEnum.Player2.
            Random random = new Random();
            return (PlayerEnum)random.Next((int)PlayerEnum.Player1, (int)PlayerEnum.Player2 + 1);
        }

        public Match()
        {

        }
    }
}
