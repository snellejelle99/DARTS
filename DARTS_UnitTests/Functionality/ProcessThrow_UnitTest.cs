using Microsoft.VisualStudio.TestTools.UnitTesting;
using DARTS.Functionality;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.Data.DataObjects;
using System.Security.Cryptography.X509Certificates;

namespace DARTS_UnitTests.Functionality
{
    [TestClass]
    public class ProcessThrow_UnitTest
    {
        [TestMethod]
        public void TestValidateScoreWin()
        {
            // Arrange
            int Throwscore = 18;
            ScoreType type = ScoreType.Double;
            int legscore = 36;
            ProcessThrow processThrow = new ProcessThrow();



            //Act
            int newlegscore = processThrow.ValidateScore(Throwscore, type, legscore);

            //Assert
            Assert.AreEqual(0, newlegscore);
        }

        [TestMethod]
        public void TestValidateScore0NotWin()
        {
            // Arrange
            int Throwscore = 18;
            ScoreType type = ScoreType.Single;
            int legscore = 18;
            ProcessThrow processThrow = new ProcessThrow();

            //Act
            int newlegscore = processThrow.ValidateScore(Throwscore, type, legscore);

            //Assert
            Assert.AreEqual(18, newlegscore);
        }

        [TestMethod]
        public void TestValidateScoreDecreaseNotWin()
        {
            // Arrange
            int Throwscore = 18;
            ScoreType type = ScoreType.Single;
            int legscore = 501;
            ProcessThrow processThrow = new ProcessThrow();

            //Act
            int newlegscore = processThrow.ValidateScore(Throwscore, type, legscore);

            //Assert
            Assert.AreEqual(483, newlegscore);
        }

    }
}
