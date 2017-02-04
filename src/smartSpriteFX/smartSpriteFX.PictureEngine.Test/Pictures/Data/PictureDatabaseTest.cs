using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;
using smartSuite.smartSpriteFX.Pictures;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.Pictures.Data
{
    [TestClass]
    public class PictureDatabaseTest
    {
        [TestCleanup]
        public void TearDown()
        {
            PictureDatabase.Clear();
        }

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
            test.INSERT(1, 1, System.Drawing.Color.FromArgb(0, 1, 2, 3));
            test.INSERT(2, 2, System.Drawing.Color.FromArgb(10, 11, 12, 13));

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
        public void TestSELECTByPoint()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, System.Drawing.Color.FromArgb(0, 1, 2, 3));
            test.INSERT(2, 2, System.Drawing.Color.FromArgb(10, 11, 12, 13));

            #endregion

            #region Running the tested operation

            var evidence = test.SELECT(new Point(1, 1));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidence);
            Assert.AreEqual(0, evidence.Color.A);
            Assert.AreEqual(1, evidence.Color.R);
            Assert.AreEqual(2, evidence.Color.G);
            Assert.AreEqual(3, evidence.Color.B);

            #endregion
        }

        [TestMethod]
        public void TestSELECTByColor()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            var color = System.Drawing.Color.FromArgb(0, 1, 2, 3);
            var color2 = System.Drawing.Color.FromArgb(10, 11, 12, 13);

            test.INSERT(1, 1, color);
            test.INSERT(2, 2, color2);
            test.INSERT(3, 4, color);

            #endregion

            #region Running the tested operation

            var evidence = test.SELECT(color);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidence);
            Assert.AreEqual(2, evidence.Count);

            #endregion
        }

        [TestMethod]
        public void TestSELECTALL()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, System.Drawing.Color.FromArgb(0, 1, 2, 3));
            test.INSERT(2, 2, System.Drawing.Color.FromArgb(10, 11, 12, 13));

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
        public void TestCOUNT()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, System.Drawing.Color.FromArgb(0, 1, 2, 3));
            test.INSERT(2, 2, System.Drawing.Color.FromArgb(10, 11, 12, 13));

            #endregion

            #region Running the tested operation

            var evidence = test.COUNT();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(2, evidence);

            #endregion
        }

        [TestMethod]
        public void TestCLEAR()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, System.Drawing.Color.FromArgb(0, 1, 2, 3));
            test.INSERT(2, 2, System.Drawing.Color.FromArgb(10, 11, 12, 13));

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

        [TestMethod]
        public void TestUPDATE()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, System.Drawing.Color.FromArgb(0, 1, 2, 3));
            test.INSERT(2, 2, System.Drawing.Color.FromArgb(10, 11, 12, 13));

            #endregion

            #region Running the tested operation

            var evidenceAmount =
                test.UPDATE(2, 2, System.Drawing.Color.FromArgb(100, 120, 130, 140));

            #endregion

            #region Getting the evidences

            var evidence = test.SELECT(2, 2);

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidence);
            Assert.AreEqual(100, evidence.GetInnerColor().A);
            Assert.AreEqual(120, evidence.GetInnerColor().R);
            Assert.AreEqual(130, evidence.GetInnerColor().G);
            Assert.AreEqual(140, evidence.GetInnerColor().B);
            Assert.AreEqual(1, evidenceAmount);

            #endregion
        }

        [TestMethod]
        public void TestClone()
        {
            #region Scenario setup

            PictureDatabase test = PictureDatabase.Open();
            test.CreateDatabase();
            test.INSERT(1, 1, System.Drawing.Color.FromArgb(0, 1, 2, 3));
            test.INSERT(2, 2, System.Drawing.Color.FromArgb(10, 11, 12, 13));

            #endregion

            #region Running the tested operation

            var evidence = test.Clone();

            #endregion

            #region Getting the evidences

            var evidenceCount = evidence.COUNT();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(evidenceCount, test.COUNT());

            #endregion
        }
    }
}
