using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Turn : DataObjectBase
    {
        #region Properties
        public long Id
        {
            get => (long)FieldCollection[TurnFieldNames.Id].Value;
            set => FieldCollection[TurnFieldNames.Id].Value = value;
        }

        public long LegId
        {
            get => (long)FieldCollection[TurnFieldNames.LegId].Value;
            set => FieldCollection[TurnFieldNames.LegId].Value = value;
        }
        
        public PlayerEnum PlayerTurn
        {
            get => (PlayerEnum)Convert.ToInt32(FieldCollection[TurnFieldNames.PlayerTurn].Value);
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

        private Turn() : base()
        {
            
        }
        
        public void CalculateThrownPoints()
        {
            foreach (Throw thrownDart in Throws)
            {
                ThrownPoints += thrownDart.Score;
            }
        }
    }
}
