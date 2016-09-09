using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Effects.Infra;
using smartSprite.Pictures.ColorPattern;

namespace smartSprite.SpriteEffectModule.Test.Infra
{
    [TestClass]
    public class ColorBufferTest
    {
        [TestMethod]
        public void RegisterTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4, 0);
            test.Register(System.Drawing.Color.Blue);
            test.Register(System.Drawing.Color.Green);
            test.Register(System.Drawing.Color.Yellow);
            test.Register(System.Drawing.Color.White);

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterUpToBoundTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4, 0);
            test.Register(System.Drawing.Color.Blue);
            test.Register(System.Drawing.Color.BlueViolet);
            test.Register(System.Drawing.Color.Green);
            test.Register(System.Drawing.Color.Yellow);
            test.Register(System.Drawing.Color.White);

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterTooCloseColorTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4, 25);
            for (int i = 0; i < 4; i++)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterNotSoCloseColorTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4, 0);
            for (int i = 0; i < 255; i += 255/4)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterDistantColorTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(0, 0);
            for (int i = 0; i < 4; i++)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }

        [TestMethod]
        public void GetSimilarColorTest()
        {
            #region Scenario setup

            ColorBuffer test = new ColorBuffer(4, 0);
            for (int i = 0; i < 255; i += 255 / 4)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
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

        [TestMethod]
        public void ClearTest()
        {
            #region Scenario setup

            ColorBuffer test = new ColorBuffer(4, 0);
            for (int i = 0; i < 255; i += 255 / 4)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Running the tested operation

            test.Clear();

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, evidence);

            #endregion
        }
    }
}
