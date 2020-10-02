using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactory
{
    public class PlayerObjectFactory : DataObjectFactoryBase
    {

        public PlayerObjectFactory() : base()
        {
        }

        protected override void SetNameAndTarget()
        {
            TableName = "PLAYER";
            TargetObject = typeof(Player);
           
        }

        protected override void InitializeFields()
        {
            base.InitializeFields();
            _fieldCollection.Add(PlayerFieldNames.Name, new DataField(PlayerFieldNames.Name, SQLiteType.TEXT, false));
            _fieldCollection.Add(PlayerFieldNames.Country, new DataField(PlayerFieldNames.Country, SQLiteType.TEXT, false));
        }

    }

    public static class PlayerFieldNames
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Country = "Country";
    }
}
