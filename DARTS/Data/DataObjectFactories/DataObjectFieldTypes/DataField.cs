using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DARTS.Data.DataObjectFactories.DataObjectFieldTypes
{
    public class DataField : ICloneable
    {
        public string Name {get;}
        public SQLiteType Type { get; }

        public bool PrimaryKey { get; }
        public object Value
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
                ValueChangedEvent?.Invoke(this, Value);
            }
        }

        public event EventHandler<object> ValueChangedEvent;


        public DataField(string name, SQLiteType type, bool primaryKey)
        {
            Name = name;
            Type = type;
            PrimaryKey = primaryKey;
            Value = null;
        }

        public DataField(string name, SQLiteType type, bool primarykey, object value)
        {
            Name = name;
            Type = type;
            PrimaryKey = primarykey;
            Value = value;
        }

        public string TypeToSQLString()
        {
            switch(Type)
            {
                case SQLiteType.NULL: return "NULL";
                case SQLiteType.INTEGER: return "INTEGER";
                case SQLiteType.REAL: return "REAL";
                case SQLiteType.TEXT: return "TEXT";
                case SQLiteType.BLOB: return "BLOB";
                default: throw new NotImplementedException();
            }

        }

        public string PrimaryKeyToSQLString()
        {
            if (PrimaryKey == true)
                return "PRIMARY KEY";
            else
                return "";
        }

        public virtual object Clone()
        {
            return new DataField(Name, Type, PrimaryKey, Value);
        }
    }

    public enum SQLiteType
    {
        NULL,
        INTEGER,
        REAL,
        TEXT,
        BLOB
    }
}
