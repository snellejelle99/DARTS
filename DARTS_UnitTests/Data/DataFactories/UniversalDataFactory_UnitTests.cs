using DARTS.Data.DataBase;
using DARTS.Data.DataObject;
using DARTS.Data.DataObjectFactory;
using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace DARTS_UnitTests.Data.DataFactories
{
    [TestClass]
    public class UniversalDataFactory_UnitTests
    {
        static PlayerObjectFactory _factory;
        static SQLiteConnection _dbconnection;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            //Arrange
            _factory = new PlayerObjectFactory();

            _dbconnection = DataBaseProvider.Instance.GetDataBaseConnection();
        }

        [TestMethod]
        public void TestTablecreation()
        {
            //Act
            SQLiteCommand cmd = _dbconnection.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'";

            SQLiteDataReader reader = cmd.ExecuteReader();

            //Assert
            while(reader.Read())
            {
                if ((string)reader["name"] == _factory.TableName) return;
            }
            Assert.Fail("Tablename not found in database");
        }

        [TestMethod]
        public void TestObjectSpawningPostingRetrieving()
        {
            DataObjectBase player = _factory.Spawn();

            Assert.AreEqual(player.GetType(), _factory.TargetObject);
            Assert.AreEqual(player.ObjectState, ObjectState.New);
            Assert.AreEqual(player.ParentFactory, _factory);

            ((Player)player).Name = "Johan Derksen";

            player.Post();
            Assert.AreEqual(player.ObjectState, ObjectState.Synced);
            Assert.IsNotNull(((Player)player).Id);

            DataObjectBase retrievedObject = _factory.Get(((Player)player).Id);

            Assert.AreEqual(((Player)player).Id, ((Player)retrievedObject).Id);
            Assert.AreEqual(((Player)player).Name, ((Player)retrievedObject).Name);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            _dbconnection.Close();
            DataBaseProvider.Instance.Dispose();
        }
    }
}
