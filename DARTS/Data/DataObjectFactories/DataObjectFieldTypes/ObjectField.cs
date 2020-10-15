using DARTS.Data.DataFactory;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS.Data.DataObjectFactories.DataObjectFieldTypes
{
    public class ObjectField : INotifyPropertyChanged, ICloneable
    {
        protected DataObjectBase _value;

        public virtual event PropertyChangedEventHandler PropertyChanged;

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
            private set;
        }

        public DataObjectBase Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                SourceField.Value = _value?.GetPrimaryKeyField().Value ?? null;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public ObjectField(string name, Type targetFactory, CodeField sourceField)
        {
            Name = name;
            SourceField = sourceField;
            if (sourceField != null) SourceField.PropertyChanged += RetrieveValue;
            TargetFactory = targetFactory;
        }

        protected virtual void RetrieveValue(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(CodeField.Value))
            {
                if (((CodeField)sender).Value != DBNull.Value)
                    if (Value?.GetPrimaryKeyField().Value != ((CodeField)sender).Value)
                    {
                        DataObjectFactoryBase dataObjectFactory = (DataObjectFactoryBase)Activator.CreateInstance(TargetFactory, true);

                        Value = dataObjectFactory.Get((long)((CodeField)sender).Value);
                    }
            }
        }

        /// <summary>
        /// Creates a new instance of an ObjectField with the same value as an existing instance
        /// </summary>
        /// <returns>A clone of ObjectField with an empty sourcefield</returns>
        public virtual object Clone()
        {
            return new ObjectField(Name, TargetFactory, null);
        }

        /// <summary>
        /// Posts the DataObject contained in this Field
        /// </summary>
        public virtual void PostValue()
        {
            Value.Post();
            SourceField.Value = Value.GetPrimaryKeyField().Value;
        }

        /// <summary>
        /// Checks if the Value contained in this Field is synchronised with the database
        /// </summary>
        /// <returns>true if synchronised or null</returns>
        public virtual bool ValueSynced()
        {
            if (Value != null)
            {
                return Value.ObjectState == ObjectState.Synced;
            }
            return true;
        }

        public void SetSourceField(CodeField field)
        {
            SourceField = field;
            if (SourceField != null) SourceField.PropertyChanged += RetrieveValue;
        }
    }
}
