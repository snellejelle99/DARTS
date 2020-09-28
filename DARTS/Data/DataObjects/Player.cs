using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Player
    {
        #region BackingStores
        private string _name;

        private int _id;

        #endregion

        #region Properties
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        #endregion

        public Player()
        {

        }
    }

    #region Enums
    public enum PlayerEnum
    {
        Player1,
        Player2
    }
    #endregion
}
