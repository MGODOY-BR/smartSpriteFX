using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Effects.Tools;
using smartSuite.smartSprite.Pictures;

namespace smartSprite.SpriteEffectModule.Test.Tools
{
    [TestClass]
    public class ResolutionTranslatorTest
    {
        [TestMethod]
        public void TranslateTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Character.stub.png");

            #endregion

            #region Running the tested operation

            ResolutionTranslator test = new ResolutionTranslator(frame, frame.Width, frame.Height, 512);
            for (int y = 0; y < frame.Height; y++)
            {
                for (int x = 0; x < frame.Width; x++)
                {
                    test.Translate(x, y, frame.GetPixel(x, y));
                }
            }

            #endregion

            #region Getting the evidences

            var evidenceColorBufferList = test.GetColorBuffer();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(512, evidenceColorBufferList.Count());
            // Assert... <-- This assert is too complex to be done here. The real test should be done in CreatedTranslatedPicture()

            #endregion
        }
    }
}
