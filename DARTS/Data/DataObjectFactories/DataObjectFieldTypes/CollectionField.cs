using DARTS.Data.DataFactory;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS.Data.DataObjectFactories.DataObjectFieldTypes
{
    public class CollectionField : ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BindingList<DataObjectBase> _value;

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

        public string TargetFieldName
        {
            get;
        }

        public BindingList<DataObjectBase> Value
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

        public CollectionField(string name, Type targetFactory, string targetFieldName, CodeField sourceField)
        {
            Name = name;
            TargetFactory = targetFactory;
            SourceField = sourceField;
            TargetFieldName = targetFieldName;
            if (sourceField != null)
            {
                sourceField.PropertyChanged += SourceField_PropertyChanged;
                sourceField.PropertyChanged += RetrieveValue;
            }
            _value = new BindingList<DataObjectBase>();
            _value.ListChanged += _value_ListChanged;
        }

        private void _value_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                ((BindingList<DataObjectBase>)sender)[e.NewIndex].FieldCollection[TargetFieldName].Value = SourceField.Value;
            }
        }

        private void SourceField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SourceField.Value))
                if (Value != null && Value.Count != 0) SetTargetFieldValuesWhenSourceChanges(((long)((CodeField)sender).Value));
        }

        protected void RetrieveValue(object sender, PropertyChangedEventArgs args)
        {
            if (Value == null || Value.Count == 0)
                if (args.PropertyName == nameof(CodeField.Value))
                {
                    DataObjectFactoryBase dataObjectFactory = (DataObjectFactoryBase)Activator.CreateInstance(TargetFactory, true);

                    Value = new BindingList<DataObjectBase>(dataObjectFactory.Get(TargetFieldName, ((CodeField)sender).Value));
                    Value.ListChanged += _value_ListChanged;
                }
        }

        /// <summary>
        /// Creates a new instance of an CollectionField with the same value as an existing instance
        /// </summary>
        /// <returns>A clone of CollectionField with an empty sourcefield</returns>
        public object Clone()
        {
            return new CollectionField(Name, TargetFactory, TargetFieldName, null);
        }

        /// <summary>
        /// Posts the DataObjects contained in this CollectionField
        /// </summary>
        public void PostValues()
        {
            foreach (DataObjectBase dataObject in Value)
            {
                if (dataObject.ObjectState != ObjectState.Synced) dataObject.Post();
            }
        }

        /// <summary>
        /// Checks if the Values contained in this CollectionField are synchronised with the database
        /// </summary>
        /// <returns>true if synchronised or collection is empty or collection is null</returns>
        public bool ValuesSynced()
        {
            if (Value != null)
            {
                foreach (DataObjectBase dataObject in Value)
                {
                    if (dataObject.ObjectState != ObjectState.Synced) return false;
                }
            }

            return true;
        }

        public void SetSourceField(CodeField field)
        {
            SourceField = field;
            if (SourceField != null)
            {
                SourceField.PropertyChanged += SourceField_PropertyChanged;
                SourceField.PropertyChanged += RetrieveValue;
            }
        }


        private void SetTargetFieldValuesWhenSourceChanges(long value)
        {
            foreach (DataObjectBase dataObject in Value)
            {
                dataObject.FieldCollection[TargetFieldName].Value = value;
            }
        }
    }
}
