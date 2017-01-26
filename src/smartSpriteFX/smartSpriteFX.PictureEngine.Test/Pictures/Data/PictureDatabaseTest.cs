using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.Pictures.Data
{
    [TestClass]
    public class PictureDatabaseTest
    {
        [TestMethod]
        public void TestOpenAndClose()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            Exception evidence = null;

            try
            {
                PictureDatabase test = PictureDatabase.Open();
                test.Close();
            }
            catch (Exception ex)
            {
                evidence = ex;
            }

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsNull(evidence);

            #endregion
        }

        [TestMethod]
        public void TestCreateDataBase()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();

            #endregion

            #region Running the tested operation

            Exception evidence = null;
            try
            {
                test.CreateDatabase();
            }
            catch (Exception ex)
            {
                evidence = ex;
            }

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsNull(evidence, evidence != null ? evidence.ToString() : null);

            #endregion
        }

        [TestMethod]
        public void TestSELECT()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, new smartSpriteFX.Pictures.ColorPattern.ColorInfo(System.Drawing.Color.FromArgb(0, 1, 2, 3)));
            test.INSERT(2, 2, new smartSpriteFX.Pictures.ColorPattern.ColorInfo(System.Drawing.Color.FromArgb(10, 11, 12, 13)));

            #endregion

            #region Running the tested operation

            var evidence = test.SELECT(1, 1);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidence);
            Assert.AreEqual(0, evidence.GetInnerColor().A);
            Assert.AreEqual(1, evidence.GetInnerColor().R);
            Assert.AreEqual(2, evidence.GetInnerColor().G);
            Assert.AreEqual(3, evidence.GetInnerColor().B);

            #endregion
        }

        [TestMethod]
        public void TestSELECTALL()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, new smartSpriteFX.Pictures.ColorPattern.ColorInfo(System.Drawing.Color.FromArgb(0, 1, 2, 3)));
            test.INSERT(2, 2, new smartSpriteFX.Pictures.ColorPattern.ColorInfo(System.Drawing.Color.FromArgb(10, 11, 12, 13)));

            #endregion

            #region Running the tested operation

            var evidence = test.SELECTALL();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidence);
            Assert.AreEqual(2, evidence.Count);

            #endregion
        }

        [TestMethod]
        public void TestCLEAR()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, new smartSpriteFX.Pictures.ColorPattern.ColorInfo(System.Drawing.Color.FromArgb(0, 1, 2, 3)));
            test.INSERT(2, 2, new smartSpriteFX.Pictures.ColorPattern.ColorInfo(System.Drawing.Color.FromArgb(10, 11, 12, 13)));

            #endregion

            #region Running the tested operation

            test.CLEAR();

            #endregion

            #region Getting the evidences

            var evidence = test.SELECTALL();

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidence);
            Assert.AreEqual(0, evidence.Count);

            #endregion
        }
    }
}
