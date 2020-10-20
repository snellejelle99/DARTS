using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Turn : DataObjectBase
    {
        #region BackingStores
        private PlayerEnum _playerTurn;

        private List<Tuple<int, ScoreType>> _throws;

        private int _thrownPoints;
        #endregion

        public long Id
        {
            get => (int)FieldCollection[TurnFieldNames.Id].Value;
            set => FieldCollection[TurnFieldNames.Id].Value = value;
        }

        public long LegId
        {
            get => (int)FieldCollection[TurnFieldNames.LegId].Value;
            set => FieldCollection[TurnFieldNames.LegId].Value = value;
        }

        #region Properties
        public PlayerEnum PlayerTurn
        {
            get
            {
                int intVal = (int)FieldCollection[TurnFieldNames.PlayerTurn].Value;
                return (PlayerEnum)intVal;
            }
            set => FieldCollection[TurnFieldNames.PlayerTurn].Value = (int)value; 
        }

        public List<Tuple<int, ScoreType>> Throws
        {
            get => _throws;
            set => _throws = value;
        }

        public int ThrownPoints
        {
            get => (int)FieldCollection[TurnFieldNames.ThrownPoints].Value;
            set => FieldCollection[TurnFieldNames.ThrownPoints].Value = value;
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
