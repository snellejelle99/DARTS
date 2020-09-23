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

        PlayerEnum _playerEnum;
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

        public PlayerEnum PlayerType
        {
            get => _playerEnum;
            set => _playerEnum = value;
        }
        #endregion

        public Player(string name, PlayerEnum playerType)
        {
            Name = name;
            PlayerType = playerType;
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
