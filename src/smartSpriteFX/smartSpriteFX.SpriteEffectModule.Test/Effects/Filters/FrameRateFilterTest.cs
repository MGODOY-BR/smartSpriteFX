using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Pictures;
using Rhino.Mocks;
using System.Collections.Generic;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class FrameRateFilterTest
    {
        [TestCleanup]
        public void TearDown()
        {
            PictureDatabase.Clear();
        }

        [TestMethod]
        public void TestApplyFilter()
        {
            #region Scenario setup

            Picture[] frameList = new Picture[1000];
            for(int i = 0; i < frameList.Length; i++)
            {
                frameList[i] = new Picture();
            }

            List<Picture> evidenceList = new List<Picture>();
            FrameRateFilter test = new FrameRateFilter();
            test.Reset();

            #endregion

            #region Running the tested operation

            for (int i = 0; i < frameList.Length; i++)
            {
                var frame = frameList[i];
                if (test.ApplyFilter(frame, i))
                {
                    evidenceList.Add(frame);
                }
            }

            #endregion

            #region Getting the evidences

            List<int> evidenceIndexList = new List<int>();
            for (int i = 0; i < evidenceList.Count; i++)
            {
                for (int j = 0; j < frameList.Length; j++)
                {
                    if (evidenceList[i] == frameList[j])
                    {
                        evidenceIndexList.Add(j);
                        Console.WriteLine(j);
                    }
                }
            }

            #endregion

            #region Validating the evidences

//            Assert.AreNotEqual(0, evidenceIndexList.Count);
            Assert.AreNotEqual(frameList.Length, evidenceList.Count);
            for (int i = 0; i < evidenceIndexList.Count - 1; i++)
            {
                Assert.AreNotEqual(
                    evidenceIndexList[i], evidenceIndexList[i + 1]);
            }

            #endregion
        }

        [TestMethod]
        public void TestApplyFilterWithKeyframe()
        {
            #region Scenario setup

            Picture[] frameList = new Picture[1000];
            for (int i = 0; i < frameList.Length; i++)
            {
                frameList[i] = new Picture();
            }

            List<Picture> evidenceList = new List<Picture>();
            FrameRateFilter test = new FrameRateFilter();
            test.Reset();
            // Adding favorite keyframes
            test.KeyFrames.Add(1);
            test.KeyFrames.Add(2);
            test.KeyFrames.Add(3);
            test.KeyFrames.Add(400);

            #endregion

            #region Running the tested operation

            for (int i = 0; i < frameList.Length; i++)
            {
                var frame = frameList[i];
                if (test.ApplyFilter(frame, i))
                {
                    evidenceList.Add(frame);
                }
            }

            #endregion

            #region Getting the evidences

            List<int> evidenceIndexList = new List<int>();
            for (int i = 0; i < evidenceList.Count; i++)
            {
                for (int j = 0; j < frameList.Length; j++)
                {
                    if (evidenceList[i] == frameList[j])
                    {
                        evidenceIndexList.Add(j);
                        Console.WriteLine(j);
                    }
                }
            }

            #endregion

            #region Validating the evidences

            Assert.AreNotEqual(0, evidenceIndexList.Count);
            Assert.AreNotEqual(frameList.Length, evidenceList.Count);
            foreach (var keyFrame in test.KeyFrames)
            {
                Assert.IsTrue(evidenceIndexList.Contains(keyFrame));
            }

            #endregion
        }
    }
}
