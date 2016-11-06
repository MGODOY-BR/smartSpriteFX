using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Effects.Tools;
using smartSuite.smartSpriteFX.Pictures;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Tools
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

            ResolutionTranslator test = new ResolutionTranslator(frame, 1366, 768, 320, 224, 512);
            for (int y = 0; y < frame.Height; y++)
            {
                for (int x = 0; x < frame.Width; x++)
                {
                    var pixel = frame.GetPixel(x, y);

                    #region Entries validation

                    if (pixel == null)
                    {
                        throw new ArgumentNullException("pixel");
                    }

                    #endregion

                    test.Translate(x, y, pixel.Value);
                }
            }

            #endregion

            #region Getting the evidences

            var evidence = test.CreatedTranslatedPicture();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(512, evidence.ColorCount);
            // Assert... <-- This assert is too complex to be done here. The real test should be done in CreatedTranslatedPicture()

            #endregion
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
