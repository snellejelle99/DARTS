using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories.DataObjectFieldTypes
{
    public class CodeField : DataField, ICloneable
    {
        public CodeField(string name , bool primaryKey = false) : base(name, SQLiteType.INTEGER, primaryKey)
        {

        }

        public override object Clone()
        {
            return new CodeField(Name, PrimaryKey);
        }
    }
}
