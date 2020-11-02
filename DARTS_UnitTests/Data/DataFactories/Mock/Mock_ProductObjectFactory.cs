using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS_UnitTests.Data.DataObjects.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataFactories.Mock
{
    public class Mock_ProductObjectFactory : DataObjectFactoryBase
    {
        public Mock_ProductObjectFactory() : base()
        {
        }

        protected override void SetNameAndTarget()
        {
            TableName = "PRODUCT";
            TargetObject = typeof(Mock_ProductObject);

        }

        protected override void InitializeFields()
        {
            CodeField idField = new CodeField(Mock_ProductObjectFieldNames.Id, true);
            _fieldCollection.Add(Mock_ProductObjectFieldNames.Id, idField);
            _fieldCollection.Add(Mock_ProductObjectFieldNames.Name, new DataField(Mock_ProductObjectFieldNames.Name, SQLiteType.TEXT));
            _fieldCollection.Add(Mock_ProductObjectFieldNames.Price, new DataField(Mock_ProductObjectFieldNames.Price, SQLiteType.INTEGER));
        }
    }

    public static class Mock_ProductObjectFieldNames
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Price = "Price";
    }
}
