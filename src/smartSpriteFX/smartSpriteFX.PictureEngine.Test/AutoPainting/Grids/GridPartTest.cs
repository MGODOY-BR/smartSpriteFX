using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.AutoPainting.Grids;
using smartSuite.smartSpriteFX.Pictures;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.AutoPainting.Grids
{
    [TestClass]
    public class GridPartTest
    {
        [TestMethod]
        public void CheckAdjacentRightTest()
        {
            #region Scenario setup

            Grid grid = new Grid();
            var stub = new GridPart(grid)
            {
                Column = 6,
                Row = 5
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(grid)
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.RIGHT, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentFarFromRightTest()
        {
            #region Scenario setup

            var grid = new Grid();
            var stub = new GridPart(grid)
            {
                Column = 7,
                Row = 5
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(grid)
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.NOT_RELATIVE, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentLeftTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 4,
                Row = 5
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.LEFT, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentFarFromLeftTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 2,
                Row = 5
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.NOT_RELATIVE, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentUpTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 5,
                Row = 4
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.UP, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentFarFromUpTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 5,
                Row = 2
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.NOT_RELATIVE, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentNowhereUpTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 6,
                Row = 4
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.NOT_RELATIVE, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentDownTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 5,
                Row = 6
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.DOWN, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentFarFromDownTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 5,
                Row = 7
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.NOT_RELATIVE, evidence);

            #endregion
        }

        [TestMethod]
        public void CheckAdjacentNowhereDownTest()
        {
            #region Scenario setup

            var stub = new GridPart(new Grid())
            {
                Column = 6,
                Row = 2
            };

            #endregion

            #region Running the tested operation

            var test = new GridPart(new Grid())
            {
                Column = 5,
                Row = 5
            };
            var evidence =
                test.CheckAdjacent(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(DegreeOfRelashionshipEnum.NOT_RELATIVE, evidence);

            #endregion
        }

        [TestMethod]
        public void IsContainedTest()
        {
            #region Scenario setup

            var stub = new Point
            {
                X = 6,
                Y = 6
            };

            var test = new GridPart(new Grid())
            {
                PointA = new Point
                {
                    X = 5,
                    Y = 5
                },
                PointB = new Point
                {
                    X = 10,
                    Y = 10
                }
            };

            #endregion

            #region Running the tested operation

            var evidence = 
                test.IsContained(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void IsNotContainedTest()
        {
            #region Scenario setup

            var stub = new Point
            {
                X = 4,
                Y = 4
            };

            var test = new GridPart(new Grid())
            {
                PointA = new Point
                {
                    X = 5,
                    Y = 5
                },
                PointB = new Point
                {
                    X = 10,
                    Y = 10
                }
            };

            #endregion

            #region Running the tested operation

            var evidence =
                test.IsContained(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void IsNotContained2Test()
        {
            #region Scenario setup

            var stub = new Point
            {
                X = 11,
                Y = 11
            };

            var grid = new Grid();
            var test = new GridPart(grid)
            {
                PointA = new Point
                {
                    X = 5,
                    Y = 5
                },
                PointB = new Point
                {
                    X = 10,
                    Y = 10
                }
            };

            #endregion

            #region Running the tested operation

            var evidence =
                test.IsContained(stub);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void IsThereAnyPointInEdgeTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            GridPart test = new GridPart(new Grid())
            {
                PointA = new Point { X = 0, Y = 0 },
                PointB = new Point { X = 10, Y = 10 },
            };
            test.PointList.AddRange(new List<Point>()
            {
                new Point { X = 0, Y = 0 },
                new Point { X = 10, Y = 10 },
            });

            #endregion

            #region Running the tested operation

            bool evidence =
                test.IsThereAnyPointInEdge();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void IsThereNonePointInEdgeTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            GridPart test = new GridPart(new Grid())
            {
                PointA = new Point { X = 0, Y = 0 },
                PointB = new Point { X = 100, Y = 100 },
            };
            test.PointList.AddRange(new List<Point>()
            {
                new Point { X = 26, Y = 36 },
                new Point { X = 27, Y = 38 },
            });

            #endregion

            #region Running the tested operation

            bool evidence =
                test.IsThereAnyPointInEdge();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }

        [TestMethod]
        public void HasContainedTest()
        {
            #region Scenario setup

            GridPart test = new GridPart(new Grid())
            {
                PointA = new Point { X = 0, Y = 0 },
                PointB = new Point { X = 10, Y = 10 },
            };
            test.PointList.AddRange(new List<Point>()
            {
                new Point { X = 0, Y = 0 },
                new Point { X = 2, Y = 2 },
                new Point { X = 3, Y = 4 },
                new Point { X = 10, Y = 10 },
            });

            #endregion

            #region Running the tested operation

            var evidence =
                test.HasContained(
                    new PictureEngine.AutoPainting.Traces.PointHand(
                        new Point(2, 2), 
                        new Point(5, 5)));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence);

            #endregion
        }

        [TestMethod]
        public void HasContainedFalseTest()
        {
            #region Scenario setup

            GridPart test = new GridPart(new Grid())
            {
                PointA = new Point { X = 0, Y = 0 },
                PointB = new Point { X = 10, Y = 10 },
            };
            test.PointList.AddRange(new List<Point>()
            {
                new Point { X = 0, Y = 0 },
                new Point { X = 10, Y = 10 },
            });

            #endregion

            #region Running the tested operation

            var evidence =
                test.HasContained(
                    new PictureEngine.AutoPainting.Traces.PointHand(
                        new Point(2, 2),
                        new Point(5, 5)));

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.IsFalse(evidence);

            #endregion
        }
    }
}
