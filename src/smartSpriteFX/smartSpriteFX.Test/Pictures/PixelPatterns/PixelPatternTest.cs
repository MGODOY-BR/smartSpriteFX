using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures.PixelPatterns;
using System.Drawing;

namespace smartSuite.smartSpriteFX.Test.smartSuite.smartSpriteFX.Pictures.PixelPatterns
{
    [TestClass]
    public class PixelPatternTest
    {
        [TestMethod]
        public void GetPatternTest()
        {
            #region Scenario setup

            PixelPattern pixelPattern = new PixelPattern();
            for (int y = 1; y < 1500; y++)
            {
                for (int x = 1; x < 300; x++)
                {
                    if (y == 109)
                    {
                        pixelPattern.Learn(x, y, Color.Yellow);
                    }
                    else
                    {
                        pixelPattern.Learn(x, y, Color.White);
                    }
                }
            }

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
