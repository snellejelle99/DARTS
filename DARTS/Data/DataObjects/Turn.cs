using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Turn : DataObjectBase
    {
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

        public BindingList<DataObjectBase> Throws
        {
            get => CollectionFieldCollection[TurnFieldNames.Throws].Value;
            set => CollectionFieldCollection[TurnFieldNames.Throws].Value = value;
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
            foreach (Throw dart in Throws)
            {
                ThrownPoints += dart.Score;
            }
        }
    }
}
