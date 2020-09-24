using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;

namespace DARTS.Data.DataBase
{
    public sealed class DataBaseProvider
    {
        private static DataBaseProvider _instance = null;
        private static readonly object padLock = new object();

        private SQLiteConnection _dbConnection;

        private DataBaseProvider()
        { 
        }

        public static DataBaseProvider Instance
        {
            get
            {
                lock(padLock)
                {
                    if(_instance == null)
                    {
                        _instance = new DataBaseProvider();
                    }
                    return _instance;
                }
            }
        }

        public SQLiteConnection GetDataBaseConnection()
        {
            if (!File.Exists("DARTS_DATABASE.sqlite"))
            {
                SQLiteConnection.CreateFile("DARTS_DATABASE.sqlite");
            }
            else if (_dbConnection.State != System.Data.ConnectionState.Open)
            {
                _dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;").OpenAndReturn();
            }
            return _dbConnection;
        }
    }
}
