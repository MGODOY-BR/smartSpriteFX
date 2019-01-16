using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.AutoPainting.Traces;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces;
using smartSuite.smartSpriteFX.Pictures;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.AutoPainting.Traces
{
    [TestClass]
    public class TraceSorterTest
    {
        [TestMethod]
        public void SortRegularHorizontalLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(12, 10),
                new PointHand(13, 10),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence.Count);
            Assert.AreEqual("X = 10, Y = 10", evidence[0].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[1].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[2].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[3].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularVerticalLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 11),
                new PointHand(10, 12),
                new PointHand(10, 13),
                new PointHand(10, 14),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence.Count);
            Assert.AreEqual("X = 10, Y = 11", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 13", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 14", evidence[3].ToString());

            #endregion
        }

        [TestMethod]
        public void SortIrregularHorizontalLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(13, 10),
                new PointHand(12, 10),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence.Count);
            Assert.AreEqual("X = 10, Y = 10", evidence[0].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[1].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[2].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[3].ToString());

            #endregion
        }

        [TestMethod]
        public void SortIrregularVerticalLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 11),
                new PointHand(10, 12),
                new PointHand(10, 14),
                new PointHand(10, 13),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence.Count);
            Assert.AreEqual("X = 10, Y = 11", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 13", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 14", evidence[3].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularLeanLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 11),
                new PointHand(12, 12),
                new PointHand(13, 13),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence.Count);
            Assert.AreEqual("X = 10, Y = 10", evidence[0].ToString());
            Assert.AreEqual("X = 11, Y = 11", evidence[1].ToString());
            Assert.AreEqual("X = 12, Y = 12", evidence[2].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[3].ToString());

            #endregion
        }

        [TestMethod]
        public void SortIrregularLeanLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 11),
                new PointHand(13, 13),
                new PointHand(12, 12),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence.Count);
            Assert.AreEqual("X = 10, Y = 10", evidence[0].ToString());
            Assert.AreEqual("X = 11, Y = 11", evidence[1].ToString());
            Assert.AreEqual("X = 12, Y = 12", evidence[2].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[3].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularCornerLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(12, 10),
                new PointHand(13, 10),
                new PointHand(13, 11),
                new PointHand(13, 12),
                new PointHand(13, 13),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(7, evidence.Count);
            Assert.AreEqual("X = 10, Y = 10", evidence[0].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[1].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[2].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[3].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[4].ToString());
            Assert.AreEqual("X = 13, Y = 12", evidence[5].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[6].ToString());

            #endregion
        }

        [TestMethod]
        public void SortIrregularCornerLineTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(13, 10),
                new PointHand(12, 10),
                new PointHand(13, 11),
                new PointHand(13, 13),
                new PointHand(13, 12),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(7, evidence.Count);
            Assert.AreEqual("X = 10, Y = 10", evidence[0].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[1].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[2].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[3].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[4].ToString());
            Assert.AreEqual("X = 13, Y = 12", evidence[5].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[6].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularCornerLine2Test()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(10, 13),
                new PointHand(12, 10),
                new PointHand(13, 10),
                new PointHand(10, 11),
                new PointHand(10, 12),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(7, evidence.Count);
            Assert.AreEqual("X = 10, Y = 13", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 11", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 10", evidence[3].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[4].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[5].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[6].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularCornerLine2_2Test()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(13, 13),
                new PointHand(11, 10),
                new PointHand(10, 13),
                new PointHand(13, 12),
                new PointHand(12, 10),
                new PointHand(13, 10),
                new PointHand(10, 11),
                new PointHand(13, 11),
                new PointHand(10, 12),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(10, evidence.Count);
            Assert.AreEqual("X = 10, Y = 13", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 11", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 10", evidence[3].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[4].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[5].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[6].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[7].ToString());
            Assert.AreEqual("X = 13, Y = 12", evidence[8].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[9].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularCornerLine3Test()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(12, 10),
                new PointHand(13, 10),
                new PointHand(10, 11),
                new PointHand(13, 11),
                new PointHand(10, 12),
                new PointHand(13, 12),
                new PointHand(10, 13),
                new PointHand(13, 13),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(10, evidence.Count);

            Assert.AreEqual("X = 10, Y = 13", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 11", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 10", evidence[3].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[4].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[5].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[6].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[7].ToString());
            Assert.AreEqual("X = 13, Y = 12", evidence[8].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[9].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularCornerCompleteSquareTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(12, 10),
                new PointHand(13, 10),
                new PointHand(13, 11),
                new PointHand(13, 12),
                new PointHand(13, 13),
                new PointHand(12, 13),
                new PointHand(11, 13),
                new PointHand(10, 13),
                new PointHand(10, 12),
                new PointHand(10, 11),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(12, evidence.Count);

            Assert.AreEqual("X = 13, Y = 12", evidence[0].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[1].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[2].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[3].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[4].ToString());
            Assert.AreEqual("X = 10, Y = 10", evidence[5].ToString());
            Assert.AreEqual("X = 10, Y = 11", evidence[6].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[7].ToString());
            Assert.AreEqual("X = 10, Y = 13", evidence[8].ToString());
            Assert.AreEqual("X = 11, Y = 13", evidence[9].ToString());
            Assert.AreEqual("X = 12, Y = 13", evidence[10].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[11].ToString());

            #endregion
        }
        [TestMethod]
        public void SortRegularCornerLine3_InvertedTest()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(10, 11),
                new PointHand(10, 12),
                new PointHand(13, 13),
                new PointHand(13, 12),
                new PointHand(13, 10),
                new PointHand(13, 11),
                new PointHand(10, 13),
                new PointHand(11, 13),
                new PointHand(12, 13),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(10, evidence.Count);

            Assert.AreEqual("X = 10, Y = 10", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 11", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 13", evidence[3].ToString());
            Assert.AreEqual("X = 11, Y = 13", evidence[4].ToString());
            Assert.AreEqual("X = 12, Y = 13", evidence[5].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[6].ToString());
            Assert.AreEqual("X = 13, Y = 12", evidence[7].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[8].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[9].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularCornerLine4Test()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(12, 10),
                new PointHand(13, 15),
                new PointHand(13, 10),
                new PointHand(10, 11),
                new PointHand(13, 14),
                new PointHand(13, 11),
                new PointHand(10, 12),
                new PointHand(13, 12),
                new PointHand(10, 13),
                new PointHand(13, 13),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(12, evidence.Count);

            Assert.AreEqual("X = 10, Y = 13", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 11", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 10", evidence[3].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[4].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[5].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[6].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[7].ToString());
            Assert.AreEqual("X = 13, Y = 12", evidence[8].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[9].ToString());
            Assert.AreEqual("X = 13, Y = 14", evidence[10].ToString());
            Assert.AreEqual("X = 13, Y = 15", evidence[11].ToString());

            #endregion
        }

        [TestMethod]
        public void SortRegularCornerLine5Test()
        {
            #region Scenario setup

            List<PointHand> stubList = new List<PointHand>
            {
                new PointHand(10, 14),
                new PointHand(10, 10),
                new PointHand(11, 10),
                new PointHand(10, 15),
                new PointHand(12, 10),
                new PointHand(13, 10),
                new PointHand(10, 11),
                new PointHand(13, 11),
                new PointHand(10, 12),
                new PointHand(13, 12),
                new PointHand(10, 13),
                new PointHand(13, 13),
            };
            Line lineStub = new Line();
            foreach (var stubItem in stubList)
            {
                stubItem.OwnerLine = lineStub;
            }

            #endregion

            #region Running the tested operation

            var test = new TraceSorter();
            test.Sort(stubList);

            #endregion

            #region Getting the evidences

            var evidence = stubList;

            #endregion

            #region Validating the evidences

            Assert.AreEqual(12, evidence.Count);

            Assert.AreEqual("X = 10, Y = 15", evidence[0].ToString());
            Assert.AreEqual("X = 10, Y = 14", evidence[1].ToString());
            Assert.AreEqual("X = 10, Y = 13", evidence[2].ToString());
            Assert.AreEqual("X = 10, Y = 12", evidence[3].ToString());
            Assert.AreEqual("X = 10, Y = 11", evidence[4].ToString());
            Assert.AreEqual("X = 10, Y = 10", evidence[5].ToString());
            Assert.AreEqual("X = 11, Y = 10", evidence[6].ToString());
            Assert.AreEqual("X = 12, Y = 10", evidence[7].ToString());
            Assert.AreEqual("X = 13, Y = 10", evidence[8].ToString());
            Assert.AreEqual("X = 13, Y = 11", evidence[9].ToString());
            Assert.AreEqual("X = 13, Y = 12", evidence[10].ToString());
            Assert.AreEqual("X = 13, Y = 13", evidence[11].ToString());

            #endregion
        }
    }
}
