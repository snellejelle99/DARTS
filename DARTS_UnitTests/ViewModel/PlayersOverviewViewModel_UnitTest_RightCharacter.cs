using DARTS.Data.DataBase;
using DARTS.Data.DataObjectFactories;
using DARTS.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DARTS_UnitTests.ViewModel
{
    [TestClass]
    public class PlayersOverviewViewModel_UnitTest_RightCharacter
    {
        static PlayersOverviewViewModel overview;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            //Arrange
            Mock_DatabaseTestData.AddDatabaseTestData();
            overview = new PlayersOverviewViewModel();
        }

        [TestMethod]
        [DataRow("player", 6)]
        [DataRow("PLAYER", 6)]
        [DataRow("player1", 1)]
        [DataRow("PLAYER1", 1)]
        [DataRow("1", 1)]
        [DataRow("草", 0)]
        public void Set_FilterText_Should_Return_Filtered_Matches(string filterInput, int expectedAmount)
        {
            // Act
            overview.FilterTextBoxText = filterInput;

            // Assert
            Assert.AreEqual(expectedAmount, overview.DisplayedPlayers.Count);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            DataBaseProvider.Instance.Dispose();
        }
    }
}
