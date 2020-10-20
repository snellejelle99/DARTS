using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories
{
    public class PlayerFactory : DataObjectFactoryBase
    {
        protected override void InitializeFields()
        {
            CodeField idField = new CodeField("Id", true);
            _fieldCollection.Add("Id", idField);

            _fieldCollection.Add("Name", new DataField("Name", SQLiteType.TEXT));
        }

        protected override void SetNameAndTarget()
        {
            TableName = "PLAYER";
            TargetObject = typeof(Player);
        }
    }
}
