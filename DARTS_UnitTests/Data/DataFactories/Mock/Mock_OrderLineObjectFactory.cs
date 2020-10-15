using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS_UnitTests.Data.DataObjects.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataFactories.Mock
{
    public class Mock_OrderLineObjectFactory : DataObjectFactoryBase
    {
        public Mock_OrderLineObjectFactory() : base()
        {
        }

        protected override void SetNameAndTarget()
        {
            TableName = "ORDERLINE";
            TargetObject = typeof(Mock_OrderLineObject);

        }

        protected override void InitializeFields()
        {
            CodeField idField = new CodeField(Mock_OrderLineObjectFieldNames.Id, true);
            CodeField productId = new CodeField(Mock_OrderLineObjectFieldNames.ProductId);

            _fieldCollection.Add(Mock_OrderLineObjectFieldNames.Id, idField);            
            _fieldCollection.Add(Mock_OrderLineObjectFieldNames.ParentOrderId, new CodeField(Mock_OrderLineObjectFieldNames.ParentOrderId));
            _fieldCollection.Add(Mock_OrderLineObjectFieldNames.ProductId, productId);

            _objectFieldCollection.Add(Mock_OrderLineObjectFieldNames.Product, new ObjectField(Mock_OrderLineObjectFieldNames.Product, typeof(Mock_ProductObjectFactory), productId));
        }

    }

    public static class Mock_OrderLineObjectFieldNames
    {
        public const string Id = "Id";
        public const string ParentOrderId = "ParentOrderId";
        public const string ParentOrder = "ParentOrder";
        public const string ProductId = "ProductId";
        public const string Product = "Product";
    }
}
