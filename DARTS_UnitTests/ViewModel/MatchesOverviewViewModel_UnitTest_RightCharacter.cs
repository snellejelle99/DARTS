using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DARTS.ViewModel;
using DARTS.Data;
using DARTS.Data.DataObjects;
using System.ComponentModel;

namespace DARTS_UnitTests.ViewModel
{
    [TestClass]
    public class MatchesOverviewViewModel_UnitTest_RightCharacter
    {
        private static MatchesOverviewViewModel overview;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // Arrange
            //TEMP: should be done with database system and TestInitialize
            BindingList<DataObjectBase> dummyMatches = DummyData.TempAddListItems();
            overview = new MatchesOverviewViewModel(dummyMatches);
        }

        [TestMethod]
        [DataRow("a", 3)]
        [DataRow("x", 0)]
        [DataRow("PLAYER", 3)]
        [DataRow("0", 1)]
        [DataRow("草", 0)]
        public void Set_FilterText_Should_Return_Filtered_Matches(string filterInput, int expectedAmount)
        {
            // Act
            overview.FilterText = filterInput;

            // Assert
            Assert.AreEqual(expectedAmount, overview.DisplayedMatches.Count);
        }

        [TestCleanup]
        public void Test_Cleanup()
        {
            overview.FilterText = string.Empty;
        }
    }
}
