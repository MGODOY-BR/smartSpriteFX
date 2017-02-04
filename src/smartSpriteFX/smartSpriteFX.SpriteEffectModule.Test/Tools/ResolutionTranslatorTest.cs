﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Effects.Tools;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Tools
{
    [TestClass]
    public class ResolutionTranslatorTest
    {
        [TestCleanup]
        public void TearDown()
        {
            PictureDatabase.Clear();
        }

        [TestMethod]
        public void CreatedTranslatedPictureTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Character.stub.png");

            #endregion

            #region Running the tested operation

            ResolutionTranslator test = new ResolutionTranslator(frame, 1366, 768, 320, 224, 512);
            var evidence = test.CreatedTranslatedPicture();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(frame.Width, evidence.Width);
            Assert.AreEqual(frame.Height, evidence.Height);
            // Assert... <-- This assert is too complex to be done here. The real test should be done in CreatedTranslatedPicture()

            #endregion
        }
    }
}
