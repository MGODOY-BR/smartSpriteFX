using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Pictures.PixelPatterns;
using System.Collections.Generic;
using System.Drawing;

namespace smartSprite.Test.smartSuite.smartSprite.Pictures.PixelPatterns
{
    [TestClass]
    public class DefaultReferentialCoordinateCriteriaTest
    {
        [TestMethod]
        public void IsValidTest()
        {
            #region Scenario setup

            var learntCache = new Dictionary<string, Color>();
            learntCache.Add("1_1", Color.Blue);
            learntCache.Add("122_109", Color.Blue);
            learntCache.Add("200_400", Color.Blue);
            learntCache.Add("200_500", Color.Blue);

            #endregion

            #region Running the tested operation

            DefaultReferentialCoordinateCriteria criteria = new DefaultReferentialCoordinateCriteria();

            var evidence =
                criteria.IsValid(
                    learntCache,
                    200,
                    300);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void IsValidTestNegative()
        {
            #region Scenario setup

            var learntCache = new Dictionary<string, Color>();
            learntCache.Add("1_1", Color.Blue);
            learntCache.Add("122_109", Color.Blue);
            learntCache.Add("200_400", Color.Blue);
            learntCache.Add("200_500", Color.Blue);

            #endregion

            #region Running the tested operation

            DefaultReferentialCoordinateCriteria criteria = new DefaultReferentialCoordinateCriteria();

            var evidence =
                criteria.IsValid(
                    learntCache,
                    200,
                    560);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

    }
}
