using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObject
{
    public abstract class DataObjectBase
    {
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
            get;
            set;
        }

        public Dictionary<string, ObjectField> ObjectFieldCollection
        {
            get;
            set;
        }

        protected DataObjectBase()
        {
            
        }

        public void Post()
        {
            ParentFactory.Post(this);
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
