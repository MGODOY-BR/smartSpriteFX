using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.AutoPainting.Traces;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces;
using smartSuite.smartSpriteFX.Pictures;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.AutoPainting.Traces
{
    [TestClass]
    public class LineTest
    {
        #region Assimilate tests

        [TestMethod]
        public void AssimilateFromEmptyLineTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            Line test = new Line();
            var evidence =
                test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByXTest()
        {
            #region Scenario setup

            Line test = 
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
                test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 10,
                            Y = 0
                        },
                        new Point
                        {
                            X = 20,
                            Y = 10
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByYTest()
        {
            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });
            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
                test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 10
                        },
                        new Point
                        {
                            X = 10,
                            Y = 20
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByXToleranceTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            Line test = 
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 10,
                            Y = 0
                        },
                        new Point
                        {
                            X = 20,
                            Y = 10
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);
            Assert.AreEqual(0, test.PointCache[0].StartPoint.X);
            Assert.AreEqual(20, test.PointCache[0].EndPoint.X);
            Assert.AreEqual(0, test.PointCache[0].StartPoint.Y);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.Y);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByYToleranceTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
                test.Assimilate(
                        new PointHand(
                            new Point
                            {
                                X = 10,
                                Y = 15
                            },
                            new Point
                            {
                                X = 10,
                                Y = 20
                            }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);
            Assert.AreEqual(0, test.PointCache[0].StartPoint.X);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.X);
            Assert.AreEqual(20, test.PointCache[0].EndPoint.Y);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByBothToleranceTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
                test.Assimilate(
                        new PointHand(
                            new Point
                            {
                                X = 5,
                                Y = 5
                            },
                            new Point
                            {
                                X = 15,
                                Y = 15
                            }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);
            Assert.AreEqual(0, test.PointCache[0].StartPoint.X);
            Assert.AreEqual(15, test.PointCache[0].EndPoint.X);
            Assert.AreEqual(0, test.PointCache[0].StartPoint.Y);
            Assert.AreEqual(15, test.PointCache[0].EndPoint.Y);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByXNegativeTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = -5,
                            Y = 0
                        },
                        new Point
                        {
                            X = 5,
                            Y = 0
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);
            Assert.AreEqual(-5, test.PointCache[0].StartPoint.X);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.X);
            Assert.AreEqual(0, test.PointCache[0].StartPoint.Y);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.Y);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByYNegativeTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = -5
                        },
                        new Point
                        {
                            X = 10,
                            Y = 0
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);
            Assert.AreEqual(0, test.PointCache[0].StartPoint.X);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.X);
            Assert.AreEqual(-5, test.PointCache[0].StartPoint.Y);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.Y);

            #endregion
        }

        [TestMethod]
        public void AssimilateFromSinglePopulatedLineByBothNegativeTest()
        {
            Assert.Inconclusive();
            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = -5,
                            Y = -5
                        },
                        new Point
                        {
                            X = 0,
                            Y = 0
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);
            Assert.AreEqual(-5, test.PointCache[0].StartPoint.X);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.X);
            Assert.AreEqual(-5, test.PointCache[0].StartPoint.Y);
            Assert.AreEqual(10, test.PointCache[0].EndPoint.Y);

            #endregion

        }

        [TestMethod]
        public void AssimilateInclinedDownLineTest()
        {
            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });

            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
                test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 10,
                            Y = 10
                        },
                        new Point
                        {
                            X = 20,
                            Y = 20
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);

            #endregion
        }

        [TestMethod]
        public void AssimilatePerpendicularPointsTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 2,
                        Y = 2
                    });
            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
                test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 10,
                            Y = 10
                        },
                        new Point
                        {
                            X = 20,
                            Y = 20
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);
            Assert.AreEqual(1, test.PointCache.Count);

            #endregion
        }

        [TestMethod]
        public void AssimilateInclinedUpLineTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            Line test =
                new Line(
                    new Point
                    {
                        X = 10,
                        Y = 10
                    });
            test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 10,
                            Y = 10
                        },
                        new Point
                        {
                            X = 20,
                            Y = 20
                        }));

            #endregion

            #region Running the tested operation

            var evidence =
                test.Assimilate(
                    new PointHand(
                        new Point
                        {
                            X = 0,
                            Y = 0
                        },
                        new Point
                        {
                            X = 10,
                            Y = 10
                        }));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);
            Assert.AreEqual(1, test.PointCache.Count);

            #endregion
        }

        #endregion
    }
}
