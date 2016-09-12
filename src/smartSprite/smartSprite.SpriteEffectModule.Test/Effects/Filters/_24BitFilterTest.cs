using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Pictures;
using System.IO;

namespace smartSprite.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class _24BitFilterTest
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

            _24BitFilter test = new _24BitFilter();
            test.Reset();
            var appliedEvidence = test.ApplyFilter(frame, 0);

            #endregion

            #region Getting the evidences

            frame.SaveCopy("24bit.png");

            Console.WriteLine(frame.ColorCount);

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4096, frame.ColorCount);
            Assert.IsTrue(appliedEvidence);
            Assert.AreEqual(originalWidth, frame.Width);
            Assert.AreEqual(originalHeight, frame.Height);
            Assert.IsTrue(File.Exists("24bit.png"));

            #endregion
        }
    }
}
