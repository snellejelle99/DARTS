using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories.DataObjectFieldTypes
{
    public class CodeField : DataField, ICloneable
    {
        public CodeField(string name) : base(name, SQLiteType.INTEGER, false)
        {

        }


        public override object Clone()
        {
            return new CodeField(Name);
        }
    }
}
