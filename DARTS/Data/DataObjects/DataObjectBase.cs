using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public abstract class DataObjectBase : IEquatable<DataObjectBase>
    {
        private Dictionary<string, DataField> _fieldCollection;
        private Dictionary<string, ObjectField> _objectFieldCollection;
        private Dictionary<string, CollectionField> _collectionFieldCollection;

        public ObjectState ObjectState
        {
            get;
            set;
        }

        public DataObjectFactoryBase ParentFactory
        {
            get;
            set;
        }

        public Dictionary<string, DataField> FieldCollection
        {
            get
            {
                return _fieldCollection;
            }
            set
            {
                _fieldCollection = value;
                SubscribeToDataFieldChanges(_fieldCollection);
            }
        }

        public Dictionary<string, ObjectField> ObjectFieldCollection
        {
            get
            {
                return _objectFieldCollection;
            }
            set
            {
                _objectFieldCollection = value;
                SubscribeToObjectFieldChanges(_objectFieldCollection);
            }
        }

        public Dictionary<string, CollectionField> CollectionFieldCollection
        {
            get
            {
                return _collectionFieldCollection;
            }
            set
            {
                _collectionFieldCollection = value;
                SubscribeToCollectionFieldChanges(_collectionFieldCollection);
            }
        }

        protected DataObjectBase()
        {

        }

        /// <summary>
        /// Posts this DataObject through its ParentFactory
        /// </summary>
        public void Post()
        {
            ParentFactory.Post(this);
        }

        public void Delete()
        {
            ParentFactory.Delete(this);
        }

        /// <summary>
        /// Gets the primarykeyField for this DataObject
        /// </summary>
        /// <returns>A DataField object represeniting a primary key</returns>
        public DataField GetPrimaryKeyField()
        {
            foreach (KeyValuePair<string, DataField> entry in FieldCollection)
            {
                if (entry.Value.PrimaryKey == true)
                {
                    return entry.Value;
                }
            }
            return null;
        }

        private void SubscribeToDataFieldChanges(Dictionary<string, DataField> fields)
        {
            foreach (KeyValuePair<string, DataField> entry in fields)
            {
                entry.Value.PropertyChanged += (sender, args) =>
                {
                    if(ObjectState != ObjectState.New) ObjectState = ObjectState.Desynced;
                };
            }
        }

        private void SubscribeToObjectFieldChanges(Dictionary<string, ObjectField> fields)
        {
            foreach (KeyValuePair<string, ObjectField> entry in fields)
            {
                entry.Value.PropertyChanged += (sender, args) =>
                {
                    if (ObjectState != ObjectState.New) ObjectState = ObjectState.Desynced;
                };
            }
        }

        private void SubscribeToCollectionFieldChanges(Dictionary<string, CollectionField> fields)
        {
            foreach (KeyValuePair<string, CollectionField> entry in fields)
            {
                entry.Value.PropertyChanged += (sender, args) =>
                {
                    if (ObjectState != ObjectState.New) ObjectState = ObjectState.Desynced;
                    entry.Value.Value.ListChanged += (sender, args) =>
                    {
                        if (ObjectState != ObjectState.New) ObjectState = ObjectState.Desynced;
                    };
                };
            }
        }

        public bool Equals([AllowNull] DataObjectBase other)
        {
            if (other == null) return false;
            else if (this.GetType() != other.GetType()) return false;
            else
            {
                //check if fields are the same
                if (FieldCollection.Count == other.FieldCollection.Count)
                {
                    for (int counter = 0; counter < FieldCollection.Count; counter++)
                    {
                        if (!FieldCollection.ElementAt(counter).Value.Name.Equals(other.FieldCollection.ElementAt(counter).Value.Name)
                            || !FieldCollection.ElementAt(counter).Value.PrimaryKey.Equals(other.FieldCollection.ElementAt(counter).Value.PrimaryKey)
                            || !FieldCollection.ElementAt(counter).Value.Type.Equals(other.FieldCollection.ElementAt(counter).Value.Type)
                            || !FieldCollection.ElementAt(counter).Value.Value.Equals(other.FieldCollection.ElementAt(counter).Value.Value))
                            return false;
                    }
                }
                //check if objectfields are the same
                if (ObjectFieldCollection.Count == other.ObjectFieldCollection.Count && ObjectFieldCollection.Count != 0)
                {
                    for (int counter = 0; counter < ObjectFieldCollection.Count; counter++)
                    {
                        if (!ObjectFieldCollection.ElementAt(counter).Value.Name.Equals(other.ObjectFieldCollection.ElementAt(counter).Value.Name)
                            || !ObjectFieldCollection.ElementAt(counter).Value.Value.Equals(other.ObjectFieldCollection.ElementAt(counter).Value.Value))
                            return false;
                    }
                }
                //check if collectionfields are the same
                if (CollectionFieldCollection.Count == other.CollectionFieldCollection.Count && CollectionFieldCollection.Count != 0)
                {
                    for (int counter = 0; counter < CollectionFieldCollection.Count; counter++)
                    {
                        if (!CollectionFieldCollection.ElementAt(counter).Value.Name.Equals(other.CollectionFieldCollection.ElementAt(counter).Value.Name)
                            || !CollectionFieldCollection.ElementAt(counter).Value.Value.SequenceEqual(other.CollectionFieldCollection.ElementAt(counter).Value.Value))
                            return false;
                    }
                }
            }
            return true;
        }
    }

    public enum ObjectState
    {
        New,
        Synced,
        Desynced,
        Deleted
    }
}
