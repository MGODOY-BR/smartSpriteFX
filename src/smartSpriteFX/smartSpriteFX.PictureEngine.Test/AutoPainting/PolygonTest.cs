using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using smartSuite.smartSprite.AutoPainting;
using smartSuite.smartSprite.AutoPainting.Grids;
using smartSuite.smartSprite.AutoPainting.Traces;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.AutoPainting
{
    [TestClass]
    public class PolygonTest
    {
        [TestMethod]
        public void DistributeTest()
        {
            Assert.Inconclusive();
            #region Scenario setup

            var penColorComparer = new PenColorComparer();

            var pictureStub =
                Picture.GetInstance(PictureStubs.square, penColorComparer);

            PointInfo[] pointStubList = pictureStub.GetAllPixels().ToArray();

            Grid grid = new Grid
            {
                RowCount = 4,
                ColumnCount = 4,
                Size = new Point
                {
                    X = pictureStub.Width,
                    Y = pictureStub.Height
                },
                Start = new Point
                {
                    X = 0,
                    Y = 0
                },
            };

            #endregion

            #region Running the tested operation

            var evidencePolygonList =
                Polygon.Distribute(grid, penColorComparer.CalculatePointRadio(), pointStubList);

            #endregion

            #region Getting the evidences

            var evidenceLineList = 
                evidencePolygonList.First().LineList;

            int i = 0;
            evidencePolygonList.ForEach(new Action<Polygon>(delegate (Polygon polygon)
            {
                polygon.DrawDebug(i + ".png");
                i++;
            }));

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidencePolygonList.Count);
            Assert.AreEqual(4, evidencePolygonList.First().CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void CalculateStatusCompleteTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //Polygon polygon = new Polygon();
            //polygon.LineList.AddRange(new List<Line>()
            //{
            //    new Line(new Point{ X = 0, Y = 0 }, new Point{ X = 50, Y = 50 }),
            //    new Line(new Point{ X = 50, Y = 50 }, new Point{ X = 100, Y = 100 }),
            //    new Line(new Point{ X = 100, Y = 100 }, new Point{ X = 0, Y = 0 }),
            //});

            //#endregion

            //#region Running the tested operation

            //var evidence = 
            //    polygon.CalculateStatus();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(PolygonStatusEnum.COMPLETE, evidence);

            //#endregion
        }

        [TestMethod]
        public void CalculateStatusInCompleteTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //Polygon polygon = new Polygon();
            //polygon.LineList.AddRange(new List<Line>()
            //{
            //    new Line(new Point{ X = 0, Y = 0 }, new Point{ X = 50, Y = 50 }),
            //    new Line(new Point{ X = 50, Y = 50 }, new Point{ X = 100, Y = 100 }),
            //    new Line(new Point{ X = 100, Y = 100 }, new Point{ X = 20, Y = 20 }),
            //});

            //#endregion

            //#region Running the tested operation

            //var evidence =
            //    polygon.CalculateStatus();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(PolygonStatusEnum.INCOMPLETE, evidence);

            //#endregion
        }

        [TestMethod]
        public void CalculateStatusNonRecognizedTest()
        {
            Assert.Inconclusive();

            //#region Scenario setup

            //Polygon polygon = new Polygon();
            //polygon.LineList.AddRange(new List<Line>()
            //{
            //    new Line(new Point{ X = 0, Y = 0 }, new Point{ X = 10, Y = 10 }),
            //});

            //#endregion

            //#region Running the tested operation

            //var evidence =
            //    polygon.CalculateStatus();

            //#endregion

            //#region Getting the evidences

            //#endregion

            //#region Validating the evidences

            //Assert.AreEqual(PolygonStatusEnum.NOT_RECOGNIZED, evidence);

            //#endregion
        }
    }
}
