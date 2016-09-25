using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using smartSuite.smartSprite.Animations;
using smartSuite.smartSprite.Pictures;
using System.Collections.Generic;

namespace smartSprite.SpriteEffectModule.Test.Animations
{
    [TestClass]
    public class FrameIteratorTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void OpenTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            FrameIterator test = FrameIterator.Open(@"StubAnimation\Enumerated");

            #endregion

            #region Getting the evidences

            var evidenceFileList = test.GetFileList();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(10, evidenceFileList.Count);
            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(
                    evidenceFileList[i].EndsWith(
                        (i + 1).ToString() + "_Circle.stub.bmp"));
            }

            #endregion
        }
    }
}
