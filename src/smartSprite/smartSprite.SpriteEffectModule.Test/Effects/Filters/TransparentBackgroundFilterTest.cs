using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Pictures;
using smartSuite.smartSprite.Effects.Filters;

namespace smartSprite.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class TransparentBackgroundFilterTest
    {
        [TestInitialize]
        public void Setup()
        {
            Picture.ClearCache();
        }

        [TestMethod]
        public void ApplyFilterTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Character.stub.png");
            int originalWidth = frame.Width;
            int originalHeight = frame.Height;

            #endregion

            #region Running the tested operation

            TransparentBackgroundFilter test = new TransparentBackgroundFilter();
            test.Reset();
            var appliedEvidence = test.ApplyFilter(frame, 0);

            #endregion

            #region Getting the evidences

            frame.SaveCopy("transparent.png");
            var evidenceColor = test.TransparentColor;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(64, evidenceColor.R);
            Assert.AreEqual(64, evidenceColor.G);
            Assert.AreEqual(64, evidenceColor.B);

            #endregion
        }
    }
}
