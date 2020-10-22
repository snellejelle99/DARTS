using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.ViewModel;
using DARTS.Data.DataObjects;
using System.Windows.Controls;
using System.Linq;
using System.Runtime.InteropServices;
using DARTS.Data.DataObjectFactories;

namespace DARTS_UnitTests.ViewModel
{
    [TestClass]
    public class PlayersOverviewViewModel_UnitTest_RightCharacter
    {
        private PlayersOverviewViewModel overview;
        private int playerAmount = 3;
        private PlayerFactory playerFactory = new PlayerFactory();

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            //TEMP: should be done with database system and TestInitialize
            List<Player> players = new List<Player>();
            for (int i = 0; i < playerAmount; i++)
            {
                Player p = (Player)playerFactory.Spawn();
                p.Name = "player" + Convert.ToString(i);
                p.Id = i;
                players.Add(p);
            }

            Player player = (Player)playerFactory.Spawn();
            player.Name = "草";
            players.Add(player);

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
