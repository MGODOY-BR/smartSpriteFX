using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Effects.Infra;

namespace smartSprite.SpriteEffectModule.Test.Infra
{
    [TestClass]
    public class ColorBufferTest
    {
        [TestMethod]
        public void RegisterTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4);
            test.Register(System.Drawing.Color.Blue);
            test.Register(System.Drawing.Color.Green);
            test.Register(System.Drawing.Color.Yellow);
            test.Register(System.Drawing.Color.White);

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterUpToBoundTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4);
            test.Register(System.Drawing.Color.Blue);
            test.Register(System.Drawing.Color.BlueViolet);
            test.Register(System.Drawing.Color.Green);
            test.Register(System.Drawing.Color.Yellow);
            test.Register(System.Drawing.Color.White);

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterTooCloseColorTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4);
            for (int i = 0; i < 4; i++)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterNotSoCloseColorTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(4);
            for (int i = 0; i < 255; i += 255/4)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }

        [TestMethod]
        public void RegisterDistantColorTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            ColorBuffer test = new ColorBuffer(0);
            for (int i = 0; i < 4; i++)
            {
                test.Register(
                    System.Drawing.Color.FromArgb(255, 255, i));
            }

            #endregion

            #region Getting the evidences

            var evidence = test.Count();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence);

            #endregion
        }
    }
}
