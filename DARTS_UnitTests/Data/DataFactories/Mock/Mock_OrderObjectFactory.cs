using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS_UnitTests.Data.DataObjects.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataFactories.Mock
{
    public class Mock_OrderObjectFactory : DataObjectFactoryBase
    {
        public Mock_OrderObjectFactory() : base()
        {
        }

        protected override void SetNameAndTarget()
        {
            TableName = "ORDER";
            TargetObject = typeof(Mock_OrderObject);

        }

        protected override void InitializeFields()
        {
            CodeField idField = new CodeField("Id", true);
            _fieldCollection.Add("Id", idField);
            _fieldCollection.Add(Mock_OrderObjectFieldNames.RecipientName, new DataField(Mock_OrderObjectFieldNames.RecipientName, SQLiteType.TEXT));

            _collectionFieldCollection.Add(Mock_OrderObjectFieldNames.OrderLines, new CollectionField(Mock_OrderObjectFieldNames.OrderLines, typeof(Mock_OrderLineObjectFactory), Mock_OrderLineObjectFieldNames.ParentOrderId, idField));
        }
    }

    public static class Mock_OrderObjectFieldNames
    {
        public const string Id = "Id";
        public const string RecipientName = "RecipientName";
        public const string OrderLines = "OrderLines";
    }
}

