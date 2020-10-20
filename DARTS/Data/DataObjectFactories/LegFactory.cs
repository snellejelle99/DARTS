using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories
{
    class LegFactory : DataObjectFactoryBase
    {

        public LegFactory() : base()
        {

        }

        protected override void InitializeFields()
        {
            CodeField idField = new CodeField("Id", true);
            _fieldCollection.Add("Id", idField);

            _fieldCollection.Add(LegFieldNames.WinningPlayer, new DataField(LegFieldNames.WinningPlayer, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.BeginningPlayer, new DataField(LegFieldNames.BeginningPlayer, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.BeginningPlayer, new DataField(LegFieldNames.LegState, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.Player1LegScore, new DataField(LegFieldNames.Player1LegScore, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.Player2LegScore, new DataField(LegFieldNames.Player2LegScore, SQLiteType.INTEGER));

            _collectionFieldCollection.Add(LegFieldNames.Turns, new CollectionField(LegFieldNames.Turns, typeof(TurnFactory), TurnFieldNames.LegId, idField));
        }

        protected override void SetNameAndTarget()
        {
            TableName = "LEG";
            TargetObject = typeof(Leg);
        }

    }

    public static class LegFieldNames
    {
        public const string WinningPlayer = "WinningPlayer";
        public const string Turns = "Turns";
        public const string BeginningPlayer = "BeginningPlayer";
        public const string LegState = "LegState";
        public const string Player1LegScore = "Player1LegScore";
        public const string Player2LegScore = "Player2LegScore";
    }
}
