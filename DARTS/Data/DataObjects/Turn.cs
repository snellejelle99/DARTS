using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Turn
    {
        #region BackingStores
        private PlayerEnum _playerTurn;

        private List<Tuple<int, ScoreType>> _throws;

        private int _thrownPoints;
        #endregion

        #region Properties
        public PlayerEnum PlayerTurn
        {
            get => _playerTurn;
            set => _playerTurn = value;
        }

        public List<Tuple<int, ScoreType>> Throws
        {
            get => _throws;
            set => _throws = value;
        }

        public int ThrownPoints
        {
            get => _thrownPoints;
            set => _thrownPoints = value;
        }
        #endregion

        public Turn()
        {
            
        }
        
        public void CalculateThrownPoints()
        {
            foreach (Tuple<int, ScoreType> dart in Throws)
            {
                ThrownPoints += dart.Item1;
            }
        }
    }
}
