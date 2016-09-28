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

        [TestMethod]
        public void NextTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            FrameIterator test = FrameIterator.Open(@"StubAnimation\Enumerated");

            #endregion

            #region Getting the evidences

            var evidence = test.Next();
            var evidencePicture = test.GetCurrent();

            var evidence2 = test.Next();
            var evidencePicture2 = test.GetCurrent();

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.IsNotNull(evidencePicture);
            Assert.AreEqual(
                @"StubAnimation\Enumerated\1_Circle.stub.bmp", evidencePicture.FullPath);

            Assert.IsTrue(evidence2);
            Assert.IsNotNull(evidencePicture2);
            Assert.AreEqual(
                @"StubAnimation\Enumerated\2_Circle.stub.bmp", evidencePicture2.FullPath);

            #endregion
        }

        [TestMethod]
        public void MoveFirstTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            FrameIterator test = FrameIterator.Open(@"StubAnimation\Enumerated");
            test.Next();
            test.Next();

            #endregion

            #region Getting the evidences

            test.MoveFirst();
            var evidencePicture = test.GetCurrent();

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidencePicture);
            Assert.AreEqual(
                @"StubAnimation\Enumerated\1_Circle.stub.bmp", evidencePicture.FullPath);

            #endregion
        }

        [TestMethod]
        public void MoveLastTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            FrameIterator test = FrameIterator.Open(@"StubAnimation\Enumerated");

            #endregion

            #region Getting the evidences

            test.MoveLast();
            var evidencePicture = test.GetCurrent();

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidencePicture);
            Assert.AreEqual(
                @"StubAnimation\Enumerated\10_Circle.stub.bmp", evidencePicture.FullPath);

            #endregion
        }

        [TestMethod]
        public void MoveToTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            FrameIterator test = FrameIterator.Open(@"StubAnimation\Enumerated");

            #endregion

            #region Getting the evidences

            test.MoveTo(3);
            var evidencePicture = test.GetCurrent();

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidencePicture);
            Assert.AreEqual(
                @"StubAnimation\Enumerated\4_Circle.stub.bmp", evidencePicture.FullPath);

            #endregion
        }
    }
}
