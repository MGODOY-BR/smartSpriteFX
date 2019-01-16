using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures;
using static smartSuite.smartSpriteFX.Pictures.Point;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.Pictures
{
    [TestClass]
    public class PointStringComparerTest
    {
        [TestMethod]
        public void CompareEqualTest()
        {
            #region Scenario setup

            var point1 = new Point
            {
                X = 0,
                Y = 0
            };
            var point2 = new Point
            {
                X = 0,
                Y = 0
            };

            #endregion

            #region Running the tested operation

            var test = new PointStringComparer();
            var evidence =
                test.Compare(point1.ToString(), point2.ToString());

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, evidence);

            #endregion
        }

        [TestMethod]
        public void CompareLowerThanByXTest()
        {
            #region Scenario setup

            var point1 = new Point
            {
                X = -1,
                Y = 0
            };
            var point2 = new Point
            {
                X = 0,
                Y = 0
            };

            #endregion

            #region Running the tested operation

            var test = new PointStringComparer();
            var evidence =
                test.Compare(point1.ToString(), point2.ToString());

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(-1, evidence);

            #endregion
        }

        [TestMethod]
        public void CompareLowerThanByYTest()
        {
            #region Scenario setup

            var point1 = new Point
            {
                X = 0,
                Y = -1
            };
            var point2 = new Point
            {
                X = 0,
                Y = 0
            };

            #endregion

            #region Running the tested operation

            var test = new PointStringComparer();
            var evidence =
                test.Compare(point1.ToString(), point2.ToString());

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(-1, evidence);

            #endregion
        }

        [TestMethod]
        public void CompareLowerThanByXYTest()
        {
            #region Scenario setup

            var point1 = new Point
            {
                X = -1,
                Y = -1
            };
            var point2 = new Point
            {
                X = 0,
                Y = 0
            };

            #endregion

            #region Running the tested operation

            var test = new PointStringComparer();
            var evidence =
                test.Compare(point1.ToString(), point2.ToString());

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(-1, evidence);

            #endregion
        }

        [TestMethod]
        public void CompareLowerGreaterThenByXTest()
        {
            #region Scenario setup

            var point1 = new Point
            {
                X = 1,
                Y = 0
            };
            var point2 = new Point
            {
                X = 0,
                Y = 0
            };

            #endregion

            #region Running the tested operation

            var test = new PointStringComparer();
            var evidence =
                test.Compare(point1.ToString(), point2.ToString());

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence);

            #endregion
        }

        [TestMethod]
        public void CompareLowerGreaterThenByYTest()
        {
            #region Scenario setup

            var point1 = new Point
            {
                X = 0,
                Y = 1
            };
            var point2 = new Point
            {
                X = 0,
                Y = 0
            };

            #endregion

            #region Running the tested operation

            var test = new PointStringComparer();
            var evidence =
                test.Compare(point1.ToString(), point2.ToString());

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence);

            #endregion
        }

        [TestMethod]
        public void CompareLowerGreaterThenByXYTest()
        {
            #region Scenario setup

            var point1 = new Point
            {
                X = 1,
                Y = 1
            };
            var point2 = new Point
            {
                X = 0,
                Y = 0
            };

            #endregion

            #region Running the tested operation

            var test = new PointStringComparer();
            var evidence =
                test.Compare(point1.ToString(), point2.ToString());

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence);

            #endregion
        }
    }
}
