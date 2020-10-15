using DARTS.Data.DataObjects;
using DARTS_UnitTests.Data.DataFactories.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataObjects.Mock
{
    public class Mock_ProductObject : DataObjectBase
    {
        public long Id
        {
            get => (long)FieldCollection[Mock_ProductObjectFieldNames.Id].Value;
            set => FieldCollection[Mock_ProductObjectFieldNames.Id].Value = value;
        }
        public string Name
        {
            get => (string)FieldCollection[Mock_ProductObjectFieldNames.Name].Value;
            set => FieldCollection[Mock_ProductObjectFieldNames.Name].Value = value;
        }

        public int Price
        {
            get => (int)FieldCollection[Mock_ProductObjectFieldNames.Price].Value;
            set => FieldCollection[Mock_ProductObjectFieldNames.Price].Value = value;
        }
        Mock_ProductObject() : base()
        {
        }
    }
}
