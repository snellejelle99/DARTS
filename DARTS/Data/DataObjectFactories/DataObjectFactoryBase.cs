using DARTS.Data.DataBase;
using DARTS.Data.DataObject;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace DARTS.Data.DataFactory
{
    public abstract class DataObjectFactoryBase
    {
        protected Dictionary<string, DataField> _fieldCollection = new Dictionary<string, DataField>();
        protected Dictionary<string, ObjectField> _objectFieldCollection = new Dictionary<string, ObjectField>();

        public string TableName
        {
            get;
            protected set;
        }

        public Type TargetObject
        {
            get;
            protected set;
        }

        protected DataObjectFactoryBase()
        {
            SetNameAndTarget();
            InitializeFields();
            TableInitialize();
        }

        protected abstract void SetNameAndTarget();

        protected virtual void InitializeFields()
        {
            _fieldCollection.Add("Id", new DataField("Id", SQLiteType.INTEGER , true));
        }

        protected void TableInitialize()
        {
            SQLiteConnection dbConnection = DataBaseProvider.Instance.GetDataBaseConnection();
            SQLiteCommand cmd = dbConnection.CreateCommand();

            StringBuilder querryBuilder = new StringBuilder();
            querryBuilder.Append($"CREATE TABLE IF NOT EXISTS {TableName} (");

            foreach(KeyValuePair<string, DataField> entry in _fieldCollection)
            {
                querryBuilder.Append($"{entry.Value.Name} {entry.Value.TypeToSQLString()} {entry.Value.PrimaryKeyToSQLString()},");
            }
            querryBuilder.Replace(',', ')', querryBuilder.Length - 1, 1);

            cmd.CommandText = querryBuilder.ToString();

            cmd.ExecuteNonQuery();
        }

        public virtual DataObjectBase Spawn()
        {
            DataObjectBase dataObject = (DataObjectBase)Activator.CreateInstance(TargetObject, true);

            dataObject.ObjectState = ObjectState.New;
            dataObject.ParentFactory = this;

            Dictionary<string, DataField> clonedFieldCollection = new Dictionary<string, DataField>();
            foreach (KeyValuePair<string, DataField> entry in _fieldCollection)
            {
                clonedFieldCollection.Add(entry.Key, (DataField)entry.Value.Clone());
            }

            dataObject.FieldCollection = clonedFieldCollection;

            Dictionary<string, ObjectField> clonedObjectFieldCollection = new Dictionary<string, ObjectField>();
            foreach (KeyValuePair<string, ObjectField> entry in _objectFieldCollection)
            {
                clonedObjectFieldCollection.Add(entry.Key, new ObjectField(entry.Value.Name, entry.Value.TargetFactory, (CodeField)clonedFieldCollection[entry.Value.SourceField.Name]));
            }

            dataObject.ObjectFieldCollection = clonedObjectFieldCollection;

            return dataObject;
        }

        public List<DataObjectBase> SpawnAmount(int amount)
        {
            List<DataObjectBase> resultList = new List<DataObjectBase>(amount);
            for(int counter = 0; counter < amount; counter++)
            {
                resultList.Add(this.Spawn());
            }
            return resultList;
        }

        public List<DataObjectBase> Get(string fieldName, object value)
        {
            SQLiteConnection dbConnection = DataBaseProvider.Instance.GetDataBaseConnection();
            SQLiteCommand cmd = dbConnection.CreateCommand();

            StringBuilder querryBuilder = new StringBuilder();
            querryBuilder.Append($"SELECT ");
            foreach (KeyValuePair<string, DataField> entry in _fieldCollection)
            {
                querryBuilder.Append($"{entry.Value.Name},");
            }
            querryBuilder.Replace(',', ' ', querryBuilder.Length - 1, 1);

            querryBuilder.Append($"FROM {TableName} ");

            querryBuilder.Append("WHERE ");
            querryBuilder.Append($"{_fieldCollection[fieldName].Name} = @{_fieldCollection[fieldName].Name}");

            cmd.CommandText = querryBuilder.ToString();
            cmd.Parameters.Add(new SQLiteParameter($"@{_fieldCollection[fieldName].Name}", value));

            SQLiteDataReader reader =  cmd.ExecuteReader();
            if(reader.HasRows)
            {
                List<DataObjectBase> resultList = new List<DataObjectBase>(reader.StepCount);
                while (reader.Read())
                {
                    DataObjectBase resultobject = this.Spawn();
                    foreach (KeyValuePair<string, DataField> entry in resultobject.FieldCollection)
                    {
                        entry.Value.Value = reader[entry.Key];
                    }
                    resultobject.ObjectState = ObjectState.Synced;
                    resultList.Add(resultobject);
                }
                return resultList;
            }
            else
            {
                reader.Close();
                return new List<DataObjectBase>(); ;
            }
        }

        public DataObjectBase Get(long primaryKey)
        {
            foreach (KeyValuePair<string, DataField> entry in _fieldCollection)
            {
                if (entry.Value.PrimaryKey == true)
                {
                    List<DataObjectBase> result = Get(entry.Value.Name, primaryKey);
                    if(result.Count == 1)
                    {
                        return result[0];
                    }
                    break;
                }
            }
            return null;
        }

        public void Post(DataObjectBase objectBase)
        {
            SQLiteConnection dbConnection = DataBaseProvider.Instance.GetDataBaseConnection();
            SQLiteCommand cmd = dbConnection.CreateCommand();

            StringBuilder querryBuilder = new StringBuilder();
            querryBuilder.Append($"INSERT OR REPLACE INTO {TableName} (");

            foreach (KeyValuePair<string, DataField> entry in objectBase.FieldCollection)
            {
                querryBuilder.Append($"{entry.Value.Name},");
            }
            querryBuilder.Replace(',', ')', querryBuilder.Length - 1, 1);

            querryBuilder.Append("VALUES (");

            foreach (KeyValuePair<string, DataField> entry in objectBase.FieldCollection)
            {
                querryBuilder.Append($"@{entry.Value.Name},");
            }
            querryBuilder.Replace(',', ')', querryBuilder.Length - 1, 1);

            cmd.CommandText = querryBuilder.ToString();
            foreach (KeyValuePair<string, DataField> entry in objectBase.FieldCollection)
            {
                cmd.Parameters.Add(new SQLiteParameter($"@{entry.Value.Name}", entry.Value.Value));
            }

            int affectedRows = cmd.ExecuteNonQuery();

            if (affectedRows == 1)
            {
                cmd = dbConnection.CreateCommand();
                cmd.CommandText = "SELECT last_insert_rowid()";
                long LastRowID64 = (long)cmd.ExecuteScalar();
                foreach(KeyValuePair<string, DataField> entry in objectBase.FieldCollection)
                {
                    if(entry.Value.PrimaryKey == true)
                    {
                        entry.Value.Value = LastRowID64;
                        break;
                    }
                }

                objectBase.ObjectState = ObjectState.Synced;
            }
        }
    }
}
