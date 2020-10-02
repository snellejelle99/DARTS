using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.ViewModel;
using DARTS.Data.DataObjects;
using System.Windows.Controls;
using System.Linq;
using System.Runtime.InteropServices;

namespace DARTS_UnitTests.ViewModel
{
    [TestClass]
    public class PlayersOverviewViewModel_UnitTest
    {
        [TestMethod]

        public void CreatePlayerOverviewFilterTests()
        {
            // Arrange
            //TEMP: should be done with database system and TestInitialize
            List<Player> players = new List<Player>();
            for (int i = 0; i < 3; i++)
            {
                Player p = new Player();
                p.Name = "player" + Convert.ToString(i);
                p.ID = i;
                players.Add(p);
            }

            PlayersOverviewViewModel newOverview = new PlayersOverviewViewModel(players);

            // Act
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            int index = random.Next(0, chars.Length);
            char character = chars[index];
            newOverview.Filter(character.ToString());
            int playerAmount = newOverview.DisplayedPlayers.Count;

            // Assert
            if ("player".Contains(character))
            {
                Assert.AreEqual(playerAmount, 3);
            }
            else 
            {
                Assert.AreEqual(playerAmount, 0);
            }
        }
    }
}
