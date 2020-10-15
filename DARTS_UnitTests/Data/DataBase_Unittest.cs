using DARTS.Data.DataBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace DARTS_UnitTests.Data
{
    [TestClass]
    public class DataBase_Unittest
    {
        static SQLiteConnection _dbconnection;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _dbconnection = DataBaseProvider.Instance.GetDataBaseConnection();
        }

        [TestMethod]
        public void TestConnection()
        {
            SQLiteCommand cmd = _dbconnection.CreateCommand();
            cmd.CommandText = "SELECT SQLITE_VERSION()";

            string version = cmd.ExecuteScalar().ToString();

            Assert.AreEqual("3.32.1", version);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            DataBaseProvider.Instance.Dispose();
        }
    }
}
