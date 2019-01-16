using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Pictures;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.Pictures.ColorPattern
{
    [TestClass]
    public class PenColorComparerTest
    {

        [TestMethod]
        public void AssociatePureBlackTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.Black);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateDarkSlateGrayTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.DarkSlateGray);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateBrownTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.Brown);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateWhiteTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.White);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateRedTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.Red);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateYellowTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.Yellow);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateGreenTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.Green);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateBlueTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.Blue);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void AssociateGrayTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            PenColorComparer test = new PenColorComparer();
            var evidence =
                test.IsPencilColor(System.Drawing.Color.Gray);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void CalculatePointRadioTest()
        {
            Assert.Inconclusive();
            #region Scenario setup

            var test = new PenColorComparer();

            #endregion

            #region Running the tested operation

            Picture picture = Picture.GetInstance(PictureStubs.square, test);
            var evidence =
                test.CalculatePointRadio();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4.95562124252319f, evidence);

            #endregion
        }

        [TestMethod]
        public void CalculatePointRadio2Test()
        {
            Assert.Inconclusive();
            #region Scenario setup

            var test = new PenColorComparer();

            #endregion

            #region Running the tested operation

            Picture picture = Picture.GetInstance(PictureStubs.square_2, test);
            var evidence =
                test.CalculatePointRadio();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(8.613009f, evidence);

            #endregion
        }

        [TestMethod]
        public void CalculatePointRadio3Test()
        {
            Assert.Inconclusive("Y is not been considered properly");

            #region Scenario setup

            var test = new PenColorComparer();

            #endregion

            #region Running the tested operation

            Picture picture = Picture.GetInstance(PictureStubs.straight, test);
            var evidence =
                test.CalculatePointRadio();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(5f, evidence);

            #endregion
        }
    }
}
