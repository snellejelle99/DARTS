using DARTS.Data.DataObject;
using DARTS.Data.DataObjectFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Player : DataObjectBase
    {
        #region BackingStores
        PlayerEnum _playerEnum;
        #endregion

        #region Properties
        public string Name
        {
            get => (string)FieldCollection[PlayerFieldNames.Name].Value;
            set => FieldCollection[PlayerFieldNames.Name].Value = value;
        }

        public long Id
        {
            get => (long)FieldCollection[PlayerFieldNames.Id].Value;
            set => FieldCollection[PlayerFieldNames.Id].Value = value;
        }

        #endregion

        private Player() : base()
        {

        }
    }

    #region Enums
    public enum PlayerEnum
    {
        None,
        Player1,
        Player2
    }
    #endregion
}
