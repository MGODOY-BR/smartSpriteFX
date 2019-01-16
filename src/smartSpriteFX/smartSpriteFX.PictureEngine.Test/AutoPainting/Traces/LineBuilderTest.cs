using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.AutoPainting.Grids;
using smartSuite.smartSprite.AutoPainting.Traces;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.AutoPainting.Traces
{
    [TestClass]
    public class LineBuilderTest
    {
        [TestMethod]
        public void ExtractLinesOneHorizontalTest()
        {
            Assert.Inconclusive();

            #region Scenario setup

            var grid = new Grid();
            var gridPartStub = new GridPart(grid);
            for (int x = 0; x < 10; x++)
            {
                gridPartStub.PointList.Add(
                    new smartSpriteFX.Pictures.Point
                    {
                        X = x,
                        Y = 0
                    });
            }

            #endregion

            #region Running the tested operation

            var test = new LineBuilder(gridPartStub, 4f);
            var evidence =
                test.ExtractLines();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence.Count);
            Assert.AreEqual(1, evidence[0].PointCache.Count);

            #endregion
        }

        [TestMethod]
        public void ExtractLinesTwoHorizontalParalelTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //var grid = new Grid();
            //var gridPartStub = new GridPart(grid);
            //for (int y = 0; y < 100; y += 99)
            //{
            //    for (int x = 0; x < 10; x++)
            //    {
            //        gridPartStub.PointList.Add(
            //            new smartSpriteFX.Pictures.Point
            //            {
            //                X = x,
            //                Y = y
            //            });
            //    }
            //}

            //#endregion

            //#region Running the tested operation

            //var test = new LineBuilder(gridPartStub);
            //var evidence =
            //    test.ExtractLines();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(2, evidence.Count);
            //Assert.AreEqual(10, evidence[0].PointList.Count);
            //Assert.AreEqual(10, evidence[1].PointList.Count);

            //#endregion
        }

        [TestMethod]
        public void ExtractLinesOneVerticalTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //var grid = new Grid();
            //var gridPartStub = new GridPart(grid);
            //for (int y = 0; y < 10; y++)
            //{
            //    gridPartStub.PointList.Add(
            //        new smartSpriteFX.Pictures.Point
            //        {
            //            X = 0,
            //            Y = y
            //        });
            //}

            //#endregion

            //#region Running the tested operation

            //var test = new LineBuilder(gridPartStub);
            //var evidence =
            //    test.ExtractLines();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(1, evidence.Count);
            //Assert.AreEqual(10, evidence[0].PointList.Count);

            //#endregion
        }

        [TestMethod]
        public void ExtractLinesTwoVerticalParalelTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //var grid = new Grid();
            //var gridPartStub = new GridPart(grid);
            //for (int y = 0; y < 10; y++)
            //{
            //    for (int x = 0; x < 100; x += 99)
            //    {
            //        gridPartStub.PointList.Add(
            //            new smartSpriteFX.Pictures.Point
            //            {
            //                X = x,
            //                Y = y
            //            });
            //    }
            //}

            //#endregion

            //#region Running the tested operation

            //var test = new LineBuilder(gridPartStub);
            //var evidence =
            //    test.ExtractLines();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(2, evidence.Count);
            //Assert.AreEqual(10, evidence[0].PointList.Count);
            //Assert.AreEqual(10, evidence[1].PointList.Count);

            //#endregion
        }

        [TestMethod]
        public void ExtractLinesOneInclinedTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //var grid = new Grid();
            //var gridPartStub = new GridPart(grid);
            //for (int x = 0; x < 10; x++)
            //{
            //    gridPartStub.PointList.Add(
            //        new smartSpriteFX.Pictures.Point
            //        {
            //            X = x,
            //            Y = x
            //        });
            //}

            //#endregion

            //#region Running the tested operation

            //var test = new LineBuilder(gridPartStub);
            //var evidence =
            //    test.ExtractLines();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(1, evidence.Count);
            //Assert.AreEqual(10, evidence[0].PointList.Count);

            //#endregion

        }

        [TestMethod]
        public void ExtractLinesTwoInclinedParalelTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //var grid = new Grid();
            //var gridPartStub = new GridPart(grid);
            //for (int x = 0; x < 10; x++)
            //{
            //    gridPartStub.PointList.Add(
            //        new smartSpriteFX.Pictures.Point
            //        {
            //            X = x,
            //            Y = x
            //        });
            //}
            //for (int x = 0; x < 10; x++)
            //{
            //    gridPartStub.PointList.Add(
            //        new smartSpriteFX.Pictures.Point
            //        {
            //            X = x + 99,
            //            Y = x + 99
            //        });
            //}

            //#endregion

            //#region Running the tested operation

            //var test = new LineBuilder(gridPartStub);
            //var evidence =
            //    test.ExtractLines();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(2, evidence.Count);
            //Assert.AreEqual(10, evidence[0].PointList.Count);
            //Assert.AreEqual(10, evidence[1].PointList.Count);

            //#endregion

        }

        [TestMethod]
        public void ExtractLinesTwoInclinedCruisedTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //var gridPartStub = new GridPart(new Grid());
            //for (int x = -50; x < 50; x++)
            //{
            //    int y = (7 - x) / 4;
            //    gridPartStub.PointList.Add(
            //        new smartSpriteFX.Pictures.Point
            //        {
            //            X = x,
            //            Y = y
            //        });
            //}
            //for (int x = -50; x < 50; x++)
            //{
            //    int y = -1 / 3 * x;
            //    gridPartStub.PointList.Add(
            //        new smartSpriteFX.Pictures.Point
            //        {
            //            X = x,
            //            Y = y
            //        });
            //}

            //#endregion

            //#region Running the tested operation

            //var test = new LineBuilder(gridPartStub);
            //var evidence =
            //    test.ExtractLines();

            //#endregion

            //#region Getting the evidences

            //var xA = from evidenceItem in evidence[0].PointList
            //         group evidenceItem.Value by evidenceItem.Value.X;

            //var xB = from evidenceItem in evidence[1].PointList
            //         group evidenceItem.Value by evidenceItem.Value.X;

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(2, evidence.Count);
            //Assert.AreEqual(164, evidence[0].PointList.Count);
            //Assert.AreEqual(100, evidence[1].PointList.Count);

            //Assert.AreEqual(100, xA.Count());
            //Assert.AreEqual(100, xB.Count());

            //#endregion

        }
    }
}
