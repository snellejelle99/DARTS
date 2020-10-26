using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Throw : DataObjectBase
    {
        public long Id
        {
            get => (long)FieldCollection[ThrowFieldNames.Id].Value;
            set => FieldCollection[ThrowFieldNames.Id].Value = value;
        }

        public long TurnId
        {
            get => (long)FieldCollection[ThrowFieldNames.TurnId].Value;
            set => FieldCollection[ThrowFieldNames.TurnId].Value = value;
        }

        public int Score
        {
            get => Convert.ToInt32(FieldCollection[ThrowFieldNames.Score].Value);
            set => FieldCollection[ThrowFieldNames.Score].Value = value;
        }

        public ScoreType ScoreType
        {
            get => (ScoreType)Convert.ToInt32(FieldCollection[ThrowFieldNames.ScoreType].Value);
            set => FieldCollection[ThrowFieldNames.ScoreType].Value = (int)value;
        }
    }
}
