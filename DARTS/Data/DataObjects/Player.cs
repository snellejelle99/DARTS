using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Player : DataObjectBase
    {
        #region BackingStores
        private string _name;

        private int _id;

        #endregion

        #region Properties
        public string Name
        {
            get => (string)FieldCollection["Name"].Value;
            set => FieldCollection["Name"].Value = value;
        }

        public long Id
        {
            get => (long)FieldCollection["Id"].Value;
            set => FieldCollection["Id"].Value = value;
        }

        #endregion

        private Player() : base()
        {

        }
    }
}
