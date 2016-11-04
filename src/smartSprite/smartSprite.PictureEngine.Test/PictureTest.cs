using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures;
using System.Linq;
using System.Drawing;

namespace smartSpriteFX.PictureEngine.Test
{
    [TestClass]
    public class PictureTest
    {
        [TestInitialize]
        public void Setup()
        {
            // Picture.ClearCache();
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

            Assert.IsTrue(evidenceList.Count() > 1);

            #endregion
        }
    }
}
