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

        private PlayerEnum ChooseRandomPlayer()
        {
            // Store the values of PlayerEnum in a list and remove the Player.None entry.
            List<PlayerEnum> values = new List<PlayerEnum>((PlayerEnum[])Enum.GetValues(typeof(PlayerEnum)));
            values.Remove(PlayerEnum.None);

            // Choose random index of values list.
            Random random = new Random();
            return values[random.Next(values.Count)];
        }

        public Match()
        {

        }
    }
}
