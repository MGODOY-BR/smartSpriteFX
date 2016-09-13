using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Pictures;
using System.IO;

namespace smartSprite.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class _8BitFilterTest
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

            _8BitFilter test = new _8BitFilter();
            test.Reset();
            var appliedEvidence = test.ApplyFilter(frame, 0);

            #endregion

            #region Getting the evidences

            frame.SaveCopy("8bit.png");

            Console.WriteLine(frame.ColorCount);

            #endregion

            #region Validating the evidences

            Assert.AreEqual(32, frame.ColorCount);
            Assert.IsTrue(appliedEvidence);
            Assert.AreEqual(originalWidth, frame.Width);
            Assert.AreEqual(originalHeight, frame.Height);
            Assert.IsTrue(File.Exists("8bit.png"));

            #endregion
        }
    }
}
