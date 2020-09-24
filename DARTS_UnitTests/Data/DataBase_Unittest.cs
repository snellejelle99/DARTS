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
        [TestMethod]
        public void TestConnection()
        {
            SQLiteConnection dbconnection = DataBaseProvider.Instance.GetDataBaseConnection();

            SQLiteCommand cmd = dbconnection.CreateCommand();
            cmd.CommandText = "SELECT SQLITE_VERSION()";

            string version = cmd.ExecuteScalar().ToString();

            Assert.AreEqual("3.32.1",version);
        }
    }
}
