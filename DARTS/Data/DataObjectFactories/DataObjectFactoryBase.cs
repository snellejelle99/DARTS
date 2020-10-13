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
        protected Dictionary<string, CollectionField> _collectionFieldCollection = new Dictionary<string, CollectionField>();

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

        /// <summary>
        /// Sets the Tablename And targetObject for this factory
        /// </summary>
        protected abstract void SetNameAndTarget();

        /// <summary>
        /// Initilializes the fields for all objects created by this factory
        /// </summary>
        protected virtual void InitializeFields()
        {

        }

        protected void TableInitialize()
        {
            SQLiteConnection dbConnection = DataBaseProvider.Instance.GetDataBaseConnection();
            SQLiteCommand cmd = dbConnection.CreateCommand();

            StringBuilder querryBuilder = new StringBuilder();
            querryBuilder.Append($"CREATE TABLE IF NOT EXISTS \"{TableName}\" (");

            foreach (KeyValuePair<string, DataField> entry in _fieldCollection)
            {
                querryBuilder.Append($"{entry.Value.Name} {entry.Value.TypeToSQLString()} {entry.Value.PrimaryKeyToSQLString()},");
            }
            querryBuilder.Replace(',', ')', querryBuilder.Length - 1, 1);

            cmd.CommandText = querryBuilder.ToString();

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Spawn an empty DataObject of the type defined in this factory.
        /// </summary>
        /// <returns>An empty DataObject</returns>
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
                clonedObjectFieldCollection.Add(entry.Key, (ObjectField)entry.Value.Clone());
                clonedObjectFieldCollection[entry.Key].SetSourceField((CodeField)clonedFieldCollection[entry.Value.SourceField.Name]); //set the new reference of the sourcefield
            }

            Dictionary<string, CollectionField> clonedCollectionFieldCollection = new Dictionary<string, CollectionField>();
            foreach (KeyValuePair<string, CollectionField> entry in _collectionFieldCollection)
            {
                clonedCollectionFieldCollection.Add(entry.Key, (CollectionField)entry.Value.Clone());
                clonedCollectionFieldCollection[entry.Key].SetSourceField((CodeField)clonedFieldCollection[entry.Value.SourceField.Name]); //set the new reference of the sourcefield
            }

            dataObject.ObjectFieldCollection = clonedObjectFieldCollection;
            dataObject.CollectionFieldCollection = clonedCollectionFieldCollection;

            return dataObject;
        }

        /// <summary>
        /// Spawns a specified ammount of empty DataObjects of the type defined in this factory.
        /// </summary>
        /// <param name="amount">The amount of objects to spawn</param>
        /// <returns>A List of empty DataObjects</returns>
        public List<DataObjectBase> SpawnAmount(int amount)
        {
            List<DataObjectBase> resultList = new List<DataObjectBase>(amount);
            for (int counter = 0; counter < amount; counter++)
            {
                resultList.Add(this.Spawn());
            }
            return resultList;
        }

        /// <summary>
        /// Retrieves one or more DataObject(s) by the value of a specified field
        /// </summary>
        /// <param name="fieldName">The field to retrieve by</param>
        /// <param name="value">The value of the field</param>
        /// <returns>A list containing one or more DataObject(s) or containing nothing when no DataObject is found</returns>
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

            querryBuilder.Append($"FROM \"{TableName}\" ");

            querryBuilder.Append("WHERE ");
            querryBuilder.Append($"{_fieldCollection[fieldName].Name} = @{_fieldCollection[fieldName].Name}");

            cmd.CommandText = querryBuilder.ToString();
            cmd.Parameters.Add(new SQLiteParameter($"@{_fieldCollection[fieldName].Name}", value));

            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                List<DataObjectBase> resultList = new List<DataObjectBase>(reader.StepCount);
                while (reader.Read())
                {
                    DataObjectBase newObject = this.Spawn();
                    foreach (KeyValuePair<string, DataField> entry in newObject.FieldCollection)
                    {
                        entry.Value.Value = reader[entry.Key];
                    }
                    newObject.ObjectState = ObjectState.Synced;
                    resultList.Add(newObject);
                }
                return resultList;
            }
            else
            {
                reader.Close();
                return new List<DataObjectBase>(); ;
            }
        }

        /// <summary>
        /// Retrieves an object by its primary key value
        /// </summary>
        /// <param name="primaryKey">The value of the primary key</param>
        /// <returns>The DataObject retrieved from database or null if no DataObject is found</returns>
        public DataObjectBase Get(long primaryKey)
        {
            foreach (KeyValuePair<string, DataField> entry in _fieldCollection)
            {
                if (entry.Value.PrimaryKey == true)
                {
                    List<DataObjectBase> result = Get(entry.Value.Name, primaryKey);
                    if (result.Count == 1)
                    {
                        return result[0];
                    }
                    break;
                }
            }
            return null;
        }

        /// <summary>
        /// Posts an object to the database
        /// </summary>
        /// <param name="objectBase">The DataObject to Post</param>
        public void Post(DataObjectBase objectBase)
        {
            //First post all objects in the received object
            foreach (KeyValuePair<string, ObjectField> entry in objectBase.ObjectFieldCollection)
            {
                if (!entry.Value.ValueSynced())
                {
                    entry.Value.PostValue();
                }
            }

            SQLiteConnection dbConnection = DataBaseProvider.Instance.GetDataBaseConnection();
            SQLiteCommand cmd = dbConnection.CreateCommand();

            StringBuilder querryBuilder = new StringBuilder();
            querryBuilder.Append($"INSERT OR REPLACE INTO \"{TableName}\" (");

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
                foreach (KeyValuePair<string, DataField> entry in objectBase.FieldCollection)
                {
                    if (entry.Value.PrimaryKey == true)
                    {
                        entry.Value.Value = LastRowID64;
                        foreach (KeyValuePair<string, CollectionField> collectionFieldEntry in objectBase.CollectionFieldCollection)
                        {
                            if (!collectionFieldEntry.Value.ValuesSynced()) collectionFieldEntry.Value.PostValues();
                        }
                        break;
                    }
                }

                objectBase.ObjectState = ObjectState.Synced;
            }
        }
    }
}
