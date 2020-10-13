using DARTS.Data.DataObject;
using DARTS_UnitTests.Data.DataFactories.Mock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS_UnitTests.Data.DataObjects.Mock
{
    public class Mock_OrderLineObject : DataObjectBase
    {
        public long Id
        {
            get => (long)FieldCollection[Mock_OrderLineObjectFieldNames.Id].Value;
            set => FieldCollection[Mock_OrderLineObjectFieldNames.Id].Value = value;
        }
        public long ParentOrderId
        {
            get => (long)FieldCollection[Mock_OrderLineObjectFieldNames.ParentOrderId].Value;
        }

        public long ProductId
        {
            get => (long)FieldCollection[Mock_OrderLineObjectFieldNames.ProductId].Value;
        }

        public DataObjectBase Product
        {
            get => (DataObjectBase)ObjectFieldCollection[Mock_OrderLineObjectFieldNames.Product].Value;
            set => ObjectFieldCollection[Mock_OrderLineObjectFieldNames.Product].Value = value;
        }

        private Mock_OrderLineObject() : base()
        {

        }
    }
}
