using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.Windows;
using DARTS.Data.DataObjects;
using System.Windows.Controls;
using System.Linq;
using System.Runtime.InteropServices;

namespace DARTS_UnitTests.Windows
{
    [TestClass]
    public class PlayersOverviewWindow_UnitTest
    {
        [TestMethod]

        public void CreatePlayerOverviewFilterTests()
        {
            // Arrange
            List<Player> players = new List<Player>();
            for (int i = 0; i < 3; i++)
            {
                Player p = new Player();
                p.Name = "player" + Convert.ToString(i);
                p.PlayerType = PlayerEnum.Player1;
                p.ID = i;
                players.Add(p);
            }

            PlayersOverviewWindow newOverview = new PlayersOverviewWindow(players);
            newOverview.Show();

            // Act
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            int index = random.Next(0, chars.Length);
            char character = chars[index];
            newOverview.Filter(character.ToString());
            int playerAmount = newOverview._displayedPlayers.Count;

            // Assert
            Assert.AreEqual(playerAmount, 3);
        }
    }
}
