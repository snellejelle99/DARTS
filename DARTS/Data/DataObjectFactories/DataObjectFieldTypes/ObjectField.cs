using DARTS.Data.DataFactory;
using DARTS.Data.DataObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories.DataObjectFieldTypes
{
    public class ObjectField
    {
        public string Name
        {
            get;
        }
        public Type TargetFactory
        {
            get;
        }

        public CodeField SourceField
        {
            get;
        }

        public DataObjectBase Value
        {
            get;
            set;
        }

        public ObjectField(string name, Type targetFactory, CodeField sourceField)
        {
            Name = name;
            SourceField = sourceField;
            SourceField.ValueChangedEvent += RetrieveValue;
            TargetFactory = targetFactory;
            Value = null;
        }

        private void RetrieveValue(object sender, object value)
        {
            DataObjectFactoryBase dataObjectFactory = (DataObjectFactoryBase)Activator.CreateInstance(TargetFactory, true);

            Value = dataObjectFactory.Get((long)value);
        }
    }
}
