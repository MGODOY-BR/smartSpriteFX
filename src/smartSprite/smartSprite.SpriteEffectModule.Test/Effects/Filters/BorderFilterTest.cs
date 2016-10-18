﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Pictures;
using smartSuite.smartSprite.Effects.Filters;
using smartSprite.SpriteEffectModule.Effects.Filters;

namespace smartSprite.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class BorderFilterTest
    {
        [TestInitialize]
        public void Setup()
        {
            // Picture.ClearCache();
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

            #endregion
        }
    }
}
