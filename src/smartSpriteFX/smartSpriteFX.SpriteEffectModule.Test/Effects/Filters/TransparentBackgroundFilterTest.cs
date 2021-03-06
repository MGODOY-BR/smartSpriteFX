﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class TransparentBackgroundFilterTest
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
            Assert.AreEqual(371, frame.OriginalHeight);
            Assert.AreEqual(310, frame.OriginalWidth);

            #endregion
        }
    }
}
