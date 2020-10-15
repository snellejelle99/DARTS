using DARTS.Data.DataBase;
using DARTS.Data.DataObject;
using DARTS.Data.DataObjectFactory;
using DARTS.Data.DataObjects;
using DARTS_UnitTests.Data.DataFactories.Mock;
using DARTS_UnitTests.Data.DataObjects.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DARTS_UnitTests.Data.DataFactories
{
    [TestClass]
    public class UniversalDataFactory_UnitTests
    {
        static Mock_OrderObjectFactory _orderFactory;
        static Mock_OrderLineObjectFactory _orderLineFactory;
        static Mock_ProductObjectFactory _productFactory;
        static SQLiteConnection _dbconnection;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            //Arrange
            _orderFactory = new Mock_OrderObjectFactory();
            _orderLineFactory = new Mock_OrderLineObjectFactory();
            _productFactory = new Mock_ProductObjectFactory();

            _dbconnection = DataBaseProvider.Instance.GetDataBaseConnection();
        }

        [TestMethod]
        public void DataFactory_Should_Have_Created_Database_Table()
        {
            //Act
            SQLiteCommand cmd = _dbconnection.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'";

            SQLiteDataReader reader = cmd.ExecuteReader();

            //Assert
            while(reader.Read())
            {
                if ((string)reader["name"] == _orderFactory.TableName) return;
            }
            Assert.Fail("Tablename not found in database");
        }

        [TestMethod]
        public void DataFactory_Should_Spawn()
        {
            //ACT
            DataObjectBase order = _orderFactory.Spawn();

            //ASSERT
            Assert.AreEqual(order.GetType(), _orderFactory.TargetObject);
            Assert.AreEqual(order.ObjectState, ObjectState.New);
            Assert.AreEqual(order.ParentFactory, _orderFactory);
        }

        [TestMethod]
        public void DataFactory_Should_Post()
        {
            //ARRANGE
            DataObjectBase order = _orderFactory.Spawn();

            ((Mock_OrderObject)order).RecipientName = "Johan Derksen";

            //ACT
            order.Post();

            //ASSERT
            Assert.AreEqual(order.ObjectState, ObjectState.Synced);
            Assert.IsNotNull(((Mock_OrderObject)order).Id);
        }

        [TestMethod]
        public void DataFactory_Should_Retrieve()
        {
            //ARRANGE
            Mock_OrderObject order = (Mock_OrderObject)_orderFactory.Spawn();

            order.RecipientName = "Johan Derksen";

            order.Post();

            //ACT
            Mock_OrderObject retrievedObject = (Mock_OrderObject)_orderFactory.Get(order.Id);

            //ASSERT
            Assert.AreEqual(order.Id, retrievedObject.Id);
            Assert.AreEqual(order.RecipientName, retrievedObject.RecipientName);
        }

        [TestMethod]
        public void DataFactory_Should_Retrieve_All()
        {
            //ARRANGE
            Mock_OrderObject order = (Mock_OrderObject)_orderFactory.Spawn();
            Mock_OrderObject order2 = (Mock_OrderObject)_orderFactory.Spawn();

            order.RecipientName = "Jantje";
            order2.RecipientName = "Klaas";

            order.Post();
            order2.Post();

            //ACT
            List<DataObjectBase> retrievedObjectList = _orderFactory.Get();

            //ASSERT
            Assert.IsTrue(retrievedObjectList.Contains(order), "Object not found in database");
            Assert.IsTrue(retrievedObjectList.Contains(order2), "Object not found in database");
        }

        [TestMethod]
        public void DataObject_Should_Change_CodeField_After_Nested_In_Collection()
        {
            //ARRANGE
            Mock_OrderObject order = (Mock_OrderObject)_orderFactory.Spawn();
            Mock_OrderLineObject orderLine = (Mock_OrderLineObject)_orderLineFactory.Spawn();

            order.Orderlines.Add(orderLine);

            //ACT
            order.Post();

            //ASSERT
            Assert.AreEqual(order.Id, orderLine.ParentOrderId);
        }

        [TestMethod]
        public void DataObject_Should_Change_CodeField_After_DataObject_Added_To_ObjectField()
        {
            //ARRANGE
            Mock_ProductObject product = (Mock_ProductObject)_productFactory.Spawn();
            Mock_OrderLineObject orderLine = (Mock_OrderLineObject)_orderLineFactory.Spawn();

            orderLine.Product = product;

            //ACT
            orderLine.Post();

            //ASSERT
            Assert.AreEqual(orderLine.ProductId, product.Id);
        }

        [TestMethod]
        public void DataObject_Should_Change_State_After_DataFieldValue_Change()
        {
            //ARRANGE
            Mock_OrderObject order = (Mock_OrderObject)_orderFactory.Spawn();
            order.Post();
            Assert.AreEqual(ObjectState.Synced, order.ObjectState);

            //ACT
            order.RecipientName = "Johan";

            //ASSERT
            Assert.AreEqual(ObjectState.Desynced, order.ObjectState);
        }

        [TestMethod]
        public void DataObject_Should_Change_State_After_ObjectFieldValue_Change()
        {
            //ARRANGE
            Mock_OrderLineObject orderLine = (Mock_OrderLineObject)_orderLineFactory.Spawn();
            orderLine.Post();
            Assert.AreEqual(ObjectState.Synced, orderLine.ObjectState);

            //ACT
            orderLine.Product = (Mock_ProductObject)_productFactory.Spawn(); 

            //ASSERT
            Assert.AreEqual(ObjectState.Desynced, orderLine.ObjectState);
        }

        [TestMethod]
        public void DataObject_Should_Change_State_After_CollectionFieldValue_Change()
        {
            //ARRANGE
            Mock_OrderObject order = (Mock_OrderObject)_orderFactory.Spawn();
            order.Post();
            Assert.AreEqual(ObjectState.Synced, order.ObjectState);

            //ACT
            order.Orderlines.Add((Mock_OrderLineObject)_orderLineFactory.Spawn());

            //ASSERT
            Assert.AreEqual(ObjectState.Desynced, order.ObjectState);
        }

        [TestMethod]
        public void DataFactory_Should_Post_DataObject_With_Nested_Object()
        {
            //ARRANGE
            Mock_OrderLineObject orderLine = (Mock_OrderLineObject)_orderLineFactory.Spawn();
            orderLine.Product = (Mock_ProductObject)_productFactory.Spawn();

            //ACT            
            orderLine.Post();

            //ASSERT
            Assert.AreEqual(ObjectState.Synced, orderLine.ObjectState);
            Assert.AreEqual(ObjectState.Synced, orderLine.Product.ObjectState);
        }

        [TestMethod]
        public void DataFactory_Should_Post_DataObject_With_Nested_Collection()
        {
            //ARRANGE
            Mock_OrderObject order = (Mock_OrderObject)_orderFactory.Spawn();
            order.Orderlines.Add((Mock_OrderLineObject)_orderLineFactory.Spawn());
            order.Orderlines.Add((Mock_OrderLineObject)_orderLineFactory.Spawn());

            //ACT
            order.Post();

            //ASSERT
            Assert.AreEqual(ObjectState.Synced, order.ObjectState);
            for(int i = 0; i < order.Orderlines.Count(); i++)
            {
                Assert.AreEqual(ObjectState.Synced, order.Orderlines[i].ObjectState);
            }            
        }

        [TestMethod]
        public void DataFactory_Should_Get_DataObject_With_Nested_Object()
        {
            //ARRANGE
            Mock_OrderLineObject orderLine = (Mock_OrderLineObject)_orderLineFactory.Spawn();
            orderLine.Product = (Mock_ProductObject)_productFactory.Spawn();
            orderLine.Post();

            //ACT
            Mock_OrderLineObject retrievedOrderLine = (Mock_OrderLineObject)_orderLineFactory.Get(orderLine.Id);

            //ASSERT
            Assert.AreEqual(orderLine.Id, retrievedOrderLine.Id);
            Assert.AreEqual(((Mock_ProductObject)orderLine.Product).Id, ((Mock_ProductObject)retrievedOrderLine.Product).Id);
            Assert.AreEqual(ObjectState.Synced, retrievedOrderLine.ObjectState);
            Assert.AreEqual(ObjectState.Synced, retrievedOrderLine.Product.ObjectState);
        }

        [TestMethod]
        public void DataFactory_Should_Get_DataObject_With_Nested_Collection()
        {
            //ARRANGE
            Mock_OrderObject order = (Mock_OrderObject)_orderFactory.Spawn();
            order.Orderlines.Add((Mock_OrderLineObject)_orderLineFactory.Spawn());
            order.Orderlines.Add((Mock_OrderLineObject)_orderLineFactory.Spawn());
            order.Post();

            //ACT
            Mock_OrderObject retrievedOrder = (Mock_OrderObject)_orderFactory.Get(order.Id);

            //ASSERT
            Assert.AreEqual(order.Id, retrievedOrder.Id);
            for (int i = 0; i < order.Orderlines.Count(); i++)
            {
                Assert.AreEqual(((Mock_OrderLineObject)order.Orderlines[i]).Id, ((Mock_OrderLineObject)retrievedOrder.Orderlines[i]).Id);
            }

            Assert.AreEqual(ObjectState.Synced, retrievedOrder.ObjectState);
            for (int i = 0; i < order.Orderlines.Count(); i++)
            {
                Assert.AreEqual(ObjectState.Synced, retrievedOrder.Orderlines[i].ObjectState);
            }
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            _dbconnection.Close();
            DataBaseProvider.Instance.Dispose();
        }
    }
}
