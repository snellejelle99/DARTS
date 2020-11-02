using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Data;

namespace DARTS.Data.DataBase
{
    public sealed class DataBaseProvider : IDisposable
    {
        private static DataBaseProvider _instance = null;
        private static readonly object padLock = new object();

        private SQLiteConnection _dbConnection;        

        private static readonly bool IsRunningFromUnittest =
            IsRunningFromUnittest = AppDomain.CurrentDomain.GetAssemblies().Any(
            a => a.FullName.ToLowerInvariant().StartsWith("testhost"));

        public const string DBFileName = "DARTS_DATABASE.sqlite";
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
                        Trace.WriteLine("Creating new DataBaseProvider");
                        _instance = new DataBaseProvider();
                        Trace.WriteLine($"Running from unit tests = {IsRunningFromUnittest}");
                    }
                    return _instance;
                }
            }
        }

        public SQLiteConnection GetDataBaseConnection()
        {            
            if (!File.Exists(DBFileName) && IsRunningFromUnittest == false)
            {
                SQLiteConnection.CreateFile(DBFileName);
            }

            if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
            {
                string datasource = IsRunningFromUnittest ? ":memory:" : DBFileName;
                string connectionstring = $"Data Source={datasource};Version=3;";

                _dbConnection = new SQLiteConnection(connectionstring);
                _dbConnection.Open();
            }
            return _dbConnection;
        }

        public void DeleteDatabase()
        {
            if (File.Exists(DBFileName))
            {
                if(_dbConnection != null && _dbConnection.State == ConnectionState.Open) _dbConnection.Close();

                File.Delete(DBFileName);
            }
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
