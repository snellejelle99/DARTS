using DARTS.Data.DataObject;
using DARTS_UnitTests.Data.DataFactories.Mock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS_UnitTests.Data.DataObjects.Mock
{
    public class Mock_OrderObject : DataObjectBase
    {
        public long Id
        {
            get => (long)FieldCollection[Mock_OrderObjectFieldNames.Id].Value;
            set => FieldCollection[Mock_OrderObjectFieldNames.Id].Value = value;
        }

        public string RecipientName
        {
            get => (string)FieldCollection[Mock_OrderObjectFieldNames.RecipientName].Value;
            set => FieldCollection[Mock_OrderObjectFieldNames.RecipientName].Value = value;
        }

        public BindingList<DataObjectBase> Orderlines
        {
            get => (BindingList<DataObjectBase>)CollectionFieldCollection[Mock_OrderObjectFieldNames.OrderLines].Value;
            set => CollectionFieldCollection[Mock_OrderObjectFieldNames.OrderLines].Value = value;
        }

        private Mock_OrderObject() : base()
        {

        }
    }
}
