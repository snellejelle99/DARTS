using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;
using System.Linq;

namespace DARTS.Data.DataBase
{
    public sealed class DataBaseProvider : IDisposable
    {
        private static DataBaseProvider _instance = null;
        private static readonly object padLock = new object();

        private SQLiteConnection _dbConnection;

        private static readonly bool IsRunningFromUnittest =
        AppDomain.CurrentDomain.GetAssemblies().Any(
            a => a.FullName.ToLowerInvariant().StartsWith("mstest.framework"));

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
            if (!File.Exists("DARTS_DATABASE.sqlite") && IsRunningFromUnittest == false)
            {
                SQLiteConnection.CreateFile("DARTS_DATABASE.sqlite");
            }
            else if (_dbConnection == null || _dbConnection.State != System.Data.ConnectionState.Open)
            {
                string datasource = IsRunningFromUnittest ? "MyDatabase.sqlite" : ":memory:";
                string connectionstring = $"Data Source={datasource};Version=3;";

                _dbConnection = new SQLiteConnection(connectionstring);
                _dbConnection.Open();
            }
            return _dbConnection;
        }

        public void Dispose()
        {
            lock (padLock)
            {
                _instance = null;
            }
        }
    }
}
