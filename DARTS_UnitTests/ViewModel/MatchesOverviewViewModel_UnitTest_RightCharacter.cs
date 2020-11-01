using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DARTS.ViewModel;
using DARTS.Data;
using DARTS.Data.DataObjects;
using System.ComponentModel;
using DARTS.Data.DataBase;

namespace DARTS_UnitTests.ViewModel
{
    [TestClass]
    public class MatchesOverviewViewModel_UnitTest_RightCharacter
    {
        private MatchesOverviewViewModel overview;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // Arrange
            DataBaseProvider.Instance.Dispose();
            Mock_DatabaseTestData.AddDatabaseTestData();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            overview = new MatchesOverviewViewModel();
        }

        [TestMethod]
        [DataRow("a", 3)]
        [DataRow("x", 0)]
        [DataRow("PLAYER", 3)]
        [DataRow("0", 2)]
        [DataRow("草", 0)]
        public void Set_FilterText_Should_Return_Filtered_Matches(string filterInput, int expectedAmount)
        {
            // Act
            overview.FilterText = filterInput;

            // Assert
            Assert.AreEqual(expectedAmount, overview.DisplayedMatches.Count);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            DataBaseProvider.Instance.Dispose();
        }
    }
}
