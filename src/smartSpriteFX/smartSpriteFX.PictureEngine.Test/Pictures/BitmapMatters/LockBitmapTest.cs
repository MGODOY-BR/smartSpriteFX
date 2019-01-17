using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.BitmapMatters;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.Pictures.BitmapMatters
{
    [TestClass]
    public class LockBitmapTest
    {
        [TestMethod]
        public void TestGetPixel()
        {
            #region Scenario setup

            LockBitmap lockBitmap = new LockBitmap(PictureStubs.square);
            lockBitmap.LockBits();

            #endregion

            #region Running the tested operation

            var evidence =
                // lockBitmap.GetPixel(10, 15);
                lockBitmap.GetPixel(4, 2);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            #endregion
        }
    }
}
