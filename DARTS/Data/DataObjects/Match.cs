using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace DARTS.Data.DataObjects
{
    public class Match
    {
        #region BackingStores
        private PlayerEnum _winnningPlayer;

        private Player _player1;

        private Player _player2;

        private List<Set> _sets;
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

        public List<Set> Sets
        {
            get => _sets;
            set => _sets = value;
        }
        #endregion

        public Match(string player1Name, string player2Name, int numSets, int numLegs, PlayerEnum beginningPlayer)
        {
            Player1 = new Player(player1Name, PlayerEnum.Player1);
            Player2 = new Player(player2Name, PlayerEnum.Player2);
            Sets = new List<Set>();

            for(int i = 0; i < numSets; i++)
            {
                Sets.Add(new Set(numLegs, beginningPlayer));
            }
        }
    }
}
