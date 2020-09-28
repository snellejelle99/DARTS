using DARTS.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS_UnitTests.Data.DataObjects
{
    [TestClass]
    public class Player_UnitTest
    {
        [TestMethod]
        // Tests if the PlayerEnum.Player2 is the Enum value after PlayerEnum.Player1. 
        // This logic is used for choosing the random beginning player in Match.
        public void PlayerEnumTest()
        {
            // Assert
            Assert.AreEqual(PlayerEnum.Player2, PlayerEnum.Player1 + 1, "PlayerEnum.Player2 isn't the Enum value after PlayerEnum.Player1.");
        }
    }
}
