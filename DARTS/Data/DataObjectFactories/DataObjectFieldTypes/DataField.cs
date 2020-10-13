using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace DARTS.Data.DataObjectFactories.DataObjectFieldTypes
{
    public class DataField : ICloneable, INotifyPropertyChanged
    {
        private bool _primaryKey;
        private object _value;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name {get;}

        public SQLiteType Type { get; }

        public bool PrimaryKey 
        {
            get
            {
                return _primaryKey;
            }
            private set
            {
                _primaryKey = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrimaryKey)));
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public DataField(string name, SQLiteType type, bool primaryKey = false)
        {
            Name = name;
            Type = type;
            PrimaryKey = primaryKey;
        }

        public DataField(string name, SQLiteType type, object value, bool primarykey = false)
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
            return new DataField(Name, Type, Value, PrimaryKey);
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
