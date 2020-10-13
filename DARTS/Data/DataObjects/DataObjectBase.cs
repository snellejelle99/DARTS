using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObject
{
    public abstract class DataObjectBase
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
    }

    public enum ObjectState
    {
        New,
        Synced,
        Desynced,
        Deleted
    }
}
