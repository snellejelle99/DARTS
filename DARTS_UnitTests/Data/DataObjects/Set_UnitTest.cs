using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataObjects
{
    [TestClass]
    public class Set_UnitTest
    {
        [TestMethod]
        public void Set_Id_Should_Not_Be_Null()
        {
            // Arrange.
            SetFactory setFactory = new SetFactory();
            Set set = (Set)setFactory.Spawn();

            // Act.
            set.Post();

            // Assert.
            Assert.IsNotNull(set.Id);
        }

        [TestMethod]
        public void Set_MatchId_Should_Not_Be_Null()
        {
            // Arrange.
            SetFactory setFactory = new SetFactory();
            Set set = (Set)setFactory.Spawn();

            MatchFactory matchFactory = new MatchFactory();
            Match match = (Match)matchFactory.Spawn();
            match.Sets.Add(set);

            // Act.
            match.Post();

            // Assert.
            Assert.IsNotNull(set.MatchId);
        }
    }
}
