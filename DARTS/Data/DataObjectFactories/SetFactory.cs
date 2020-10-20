using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories
{
    public class SetFactory : DataObjectFactoryBase
    {
        protected override void InitializeFields()
        {
            CodeField idField = new CodeField(SetFieldNames.Id, true);
            _fieldCollection.Add(SetFieldNames.Id, idField);

            
            _collectionFieldCollection.Add(SetFieldNames.Legs, new CollectionField(SetFieldNames.Legs, typeof(LegFactory), LegFieldNames.SetId, idField));

            _fieldCollection.Add(SetFieldNames.MatchId, new CodeField(SetFieldNames.MatchId));
            _fieldCollection.Add(SetFieldNames.NumLegs, new DataField(SetFieldNames.NumLegs, SQLiteType.INTEGER));
            _fieldCollection.Add(SetFieldNames.Player1LegsWon, new DataField(SetFieldNames.Player1LegsWon, SQLiteType.INTEGER));
            _fieldCollection.Add(SetFieldNames.Player2LegsWon, new DataField(SetFieldNames.Player2LegsWon, SQLiteType.INTEGER));
            _fieldCollection.Add(SetFieldNames.SetState, new DataField(SetFieldNames.SetState, SQLiteType.INTEGER));
            _fieldCollection.Add(SetFieldNames.BeginningPlayer, new DataField(SetFieldNames.BeginningPlayer, SQLiteType.INTEGER));
            _fieldCollection.Add(SetFieldNames.WinningPlayer, new DataField(SetFieldNames.WinningPlayer, SQLiteType.INTEGER));
        }

        protected override void SetNameAndTarget()
        {
            TableName = "SET";
            TargetObject = typeof(Set);
        }
    }

    public static class SetFieldNames
    {
        public const string Id = "Id";
        public const string MatchId = "MatchId";
        public const string Legs = "Legs";
        public const string WinningPlayer = "WinningPlayer";
        public const string BeginningPlayer = "BeginningPlayer";
        public const string SetState = "SetState";
        public const string NumLegs = "NumLegs";
        public const string Player1LegsWon = "Player1LegsWon";
        public const string Player2LegsWon = "Player2LegsWon";

    }
}
