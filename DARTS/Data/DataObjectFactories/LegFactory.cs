using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories
{
    public class LegFactory : DataObjectFactoryBase
    {

        public LegFactory() : base()
        {

        }

        protected override void InitializeFields()
        {
            CodeField idField = new CodeField(LegFieldNames.Id, true);
            _fieldCollection.Add(LegFieldNames.Id, idField);   
            
            _fieldCollection.Add(LegFieldNames.SetId, new CodeField(LegFieldNames.SetId));
            _fieldCollection.Add(LegFieldNames.WinningPlayer, new DataField(LegFieldNames.WinningPlayer, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.BeginningPlayer, new DataField(LegFieldNames.BeginningPlayer, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.LegState, new DataField(LegFieldNames.LegState, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.Player1LegScore, new DataField(LegFieldNames.Player1LegScore, SQLiteType.INTEGER));
            _fieldCollection.Add(LegFieldNames.Player2LegScore, new DataField(LegFieldNames.Player2LegScore, SQLiteType.INTEGER));

            _collectionFieldCollection.Add(LegFieldNames.Turns, new CollectionField(LegFieldNames.Turns, typeof(TurnFactory), TurnFieldNames.LegId, idField));
        }

        protected override void SetNameAndTarget()
        {
            TableName = "LEG";
            TargetObject = typeof(Leg);
        }

        public override void Delete(DataObjectBase objectBase)
        {
            Leg leg = (Leg)objectBase;
            leg.CollectionFieldCollection[LegFieldNames.Turns].DeleteValues();
            base.Delete(objectBase);
        }
    }

    public static class LegFieldNames
    {
        public const string Id = "Id";
        public const string SetId = "SetId";
        public const string WinningPlayer = "WinningPlayer";
        public const string Turns = "Turns";
        public const string BeginningPlayer = "BeginningPlayer";
        public const string LegState = "LegState";
        public const string Player1LegScore = "Player1LegScore";
        public const string Player2LegScore = "Player2LegScore";
    }
}
