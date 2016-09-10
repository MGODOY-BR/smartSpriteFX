using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Pictures;
using System.IO;

namespace smartSprite.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class _16BitFilterTest
    {
        [TestMethod]
        public void ApplyFilterTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Character.stub.png");
            int originalWidth = frame.Width;
            int originalHeight = frame.Height;

            #endregion

            #region Running the tested operation

            _16BitFilter test = new _16BitFilter();
            test.Reset();
            var appliedEvidence = test.ApplyFilter(frame, 0);

            #endregion

            #region Getting the evidences

            frame.SaveCopy("16bit.png");

            #endregion

            #region Validating the evidences

            Assert.IsTrue(frame.ColorCount <= 512);
            Assert.IsTrue(appliedEvidence);
            Assert.AreEqual(originalWidth, frame.Width);
            Assert.AreEqual(originalHeight, frame.Height);
            Assert.IsTrue(File.Exists("16bit.png"));

            #endregion
        }
    }
}
