using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Pictures.PixelPatterns;
using System.Drawing;

namespace smartSprite.Test.smartSuite.smartSprite.Pictures.PixelPatterns
{
    [TestClass]
    public class PixelPatternTest
    {
        [TestMethod]
        public void GetPatternTest()
        {
            #region Scenario setup

            PixelPattern pixelPattern = new PixelPattern();
            pixelPattern.Learn(1, 1, Color.White);
            pixelPattern.Learn(10, 1, Color.White);
            pixelPattern.Learn(10, 15, Color.White);
            pixelPattern.Learn(100, 100, Color.Yellow);
            pixelPattern.Learn(200, 120, Color.White);
            pixelPattern.Learn(201, 1500, Color.White);

            #endregion

            #region Running the tested operation

            var evidence = pixelPattern.GetPattern(100, 110);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(Color.Yellow, evidence);

            #endregion
        }
    }
}
