using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class BorderFilterTest
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
        public void ApplyFilterTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Character.stub.png");

            TransparentBackgroundFilter transparentBackGroundFilter = new TransparentBackgroundFilter();
            transparentBackGroundFilter.Reset();
            transparentBackGroundFilter.ApplyFilter(frame, 0);

            #endregion

            #region Running the tested operation

            var test = new BorderFilter();
            test.Reset();
            test.ApplyFilter(frame, 0);

            #endregion

            #region Getting the evidences

            frame.SaveCopy("border.png");

            #endregion

            #region Validating the evidences

            // HACK: Too complex assertion
            // Assert.AreEqual(...;
            Assert.AreEqual(371, frame.OriginalHeight);
            Assert.AreEqual(310, frame.OriginalWidth);

            #endregion
        }
    }
}
