using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures;
using System.Linq;
using System.Drawing;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;

namespace smartSuite.smartSpriteFX.PictureEngine.Test
{
    [TestClass]
    public class PictureTest
    {
        [TestInitialize]
        public void Setup()
        {
            // Picture.ClearCache();
        }

        [TestCleanup]
        public void TearDown()
        {
            PictureDatabase.Clear();
        }

        [TestMethod]
        public void ListBorderTest()
        {
            #region Scenario setup

            Picture test = Picture.GetInstance(@"Stubs\Character.stub.png");
            test.TransparentColor = Color.FromArgb(255, 64, 64, 64);

            #endregion

            #region Running the tested operation

            var borderList = 
                test.ListBorder();

            #endregion

            #region Getting the evidences

            var evidenceList = from item in borderList
                               where
                               (
                                   item.X == 70 &&
                                   item.Y == 143
                               ) ||
                               (
                                   item.X == 205 &&
                                   item.Y == 297
                               )
                               select item;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(5, evidenceList.Count());
            Assert.AreEqual(371, test.OriginalHeight);
            Assert.AreEqual(310, test.OriginalWidth);

            #endregion
        }

        [TestMethod]
        public void GetLinesTest()
        {
            #region Scenario setup

            Picture test = Picture.GetInstance(@"Stubs\Character.stub.png");

            #endregion

            #region Running the tested operation

            var evidence = test.GetLines();

            #endregion

            #region Getting the evidences


            #endregion

            #region Validating the evidences

            Assert.AreEqual(371, evidence.Count());
            Assert.AreEqual(371, test.OriginalHeight);
            Assert.AreEqual(310, test.OriginalWidth);

            #endregion
        }

        [TestMethod]
        public void GetColumnsTest()
        {
            #region Scenario setup

            Picture test = Picture.GetInstance(@"Stubs\Character.stub.png");

            #endregion

            #region Running the tested operation

            var evidence = test.GetColumns();

            #endregion

            #region Getting the evidences


            #endregion

            #region Validating the evidences

            Assert.AreEqual(310, evidence.Count());
            Assert.AreEqual(371, test.OriginalHeight);
            Assert.AreEqual(310, test.OriginalWidth);

            #endregion
        }
    }
}
