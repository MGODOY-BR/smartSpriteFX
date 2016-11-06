using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Infra
{
    [TestClass]
    public class ColorBufferTest
    {
        [TestMethod]
        public void GetSimilarColorTest()
        {
            #region Scenario setup

            ColorBuffer test = new ColorBuffer(4, 0);
            for (int i = 0; i < 255; i += 255 / 4)
            {
                //test.Register(
                //    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Running the tested operation

            var evidenceA =
                test.GetSimilarColor(
                    System.Drawing.Color.FromArgb(255, 225, 64));

            var evidenceB =
                test.GetSimilarColor(
                    System.Drawing.Color.FromArgb(255, 225, 65));

            var evidenceC =
                test.GetSimilarColor(
                    System.Drawing.Color.FromArgb(255, 225, 80));

            var evidenceD =
                test.GetSimilarColor(
                    System.Drawing.Color.FromArgb(255, 225, 100));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            ColorEqualityComparer comparer = new ColorEqualityComparer(0);

            Assert.IsTrue(
                comparer.Equals(evidenceA, evidenceB));

            Assert.IsTrue(
                comparer.Equals(evidenceA, evidenceC));

            Assert.IsTrue(
                comparer.Equals(evidenceA, evidenceD));

            Assert.IsTrue(
                comparer.Equals(evidenceB, evidenceC));

            Assert.IsTrue(
                comparer.Equals(evidenceA, evidenceC));

            #endregion
        }

    }
}
