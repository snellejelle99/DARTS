using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;

namespace DARTS.Data.DataObjectFactories
{
    public class TurnFactory : DataObjectFactoryBase
    {
        protected override void InitializeFields()
        {
            CodeField idField = new CodeField(TurnFieldNames.Id, true);
            _fieldCollection.Add(TurnFieldNames.Id, idField);

            _fieldCollection.Add(TurnFieldNames.LegId, new CodeField(TurnFieldNames.LegId));
            _fieldCollection.Add(TurnFieldNames.PlayerTurn, new DataField(TurnFieldNames.PlayerTurn, SQLiteType.INTEGER));
            _fieldCollection.Add(TurnFieldNames.ThrownPoints, new DataField(TurnFieldNames.ThrownPoints, SQLiteType.INTEGER));

            _collectionFieldCollection.Add(TurnFieldNames.Throws, new CollectionField(TurnFieldNames.Throws, typeof(ThrowFactory), ThrowFieldNames.TurnId, idField));
        }

        protected override void SetNameAndTarget()
        {
            TableName = "TURN";
            TargetObject = typeof(Turn);
        }

        public override void Delete(DataObjectBase objectBase)
        {
            Turn turn = (Turn)objectBase;
            turn.CollectionFieldCollection[TurnFieldNames.Throws].DeleteValues();
            base.Delete(objectBase);
        }
    }

    public static class TurnFieldNames
    {
        public const string Id = "Id";
        public const string LegId = "LegId";
        public const string PlayerTurn = "PlayerTurn";
        public const string Throws = "Throws";
        public const string ThrownPoints = "ThrownPoints";
    }
}