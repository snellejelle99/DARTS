using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.Data.DataObjects;
using DARTS
using System.Windows.Controls;
using System.Linq;
using System.Runtime.InteropServices;
using DARTS.View;

namespace DARTS_UnitTests.ViewModel
{
    [TestClass]
    class MatchesOverviewTests
    {
        private relay
        private int playerAmount = 3;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            //TEMP: should be done with database system and TestInitialize
            List<Player> players = new List<Player>();
            for (int i = 0; i < playerAmount; i++)
            {
                Player p = new Player();
                p.Name = "player" + Convert.ToString(i);
                p.ID = i;
                players.Add(p);
            }

            players.Add(new Player() { Name = "草", ID = 4 });
            overview = new PlayersOverviewViewModel(players);
        }

        [TestMethod]
        public void Set_Filter_Should_Find_Three_Players()
        {
            // Act
            string character = "a";
            overview.FilterTextBoxText = character;

            // Assert
            Assert.AreEqual(playerAmount, overview.DisplayedPlayers.Count);
        }

        [TestMethod]
        public void Set_Filter_Should_Find_Zero_Players()
        {
            // Act
            string character = "x";
            overview.FilterTextBoxText = character;

            // Assert
            Assert.AreEqual(0, overview.DisplayedPlayers.Count);
        }

        [TestMethod]
        public void Set_filter_Should_Accept_All_Characters()
        {
            // Act
            string character = "草";
            overview.FilterTextBoxText = character;

            // Assert
            Assert.AreEqual(1, overview.DisplayedPlayers.Count);
        }
    }
}
