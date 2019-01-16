using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.AutoPainting.Grids;
using smartSuite.smartSpriteFX.Pictures;
using System.Linq;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.AutoPainting.Grids
{
    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void BuildTest()
        {
            #region Scenario setup

            var grid = new Grid
            {
                ColumnCount = 10,
                RowCount = 5,
                Size = new Point
                {
                    X = 100,
                    Y = 50
                },
                Start = new Point
                {
                    X = 0,
                    Y = 0
                },
            };

            #endregion

            #region Running the tested operation

            grid.Build(new PictureEngine.Pictures.PointInfo[0]);

            #endregion

            #region Getting the evidences

            var gridPartListEvidence = 
                grid.GridPartList;

            var minRowEvidence = gridPartListEvidence.Min(x => x.Row);
            var maxRowEvidence = gridPartListEvidence.Max(x => x.Row);
            var minColEvidence = gridPartListEvidence.Min(x => x.Column);
            var maxColEvidence = gridPartListEvidence.Max(x => x.Column);

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, minColEvidence);
            Assert.AreEqual(0, minRowEvidence);
            Assert.AreEqual(4, maxRowEvidence);
            Assert.AreEqual(9, maxColEvidence);
            Assert.AreEqual(50, gridPartListEvidence.Count);
            gridPartListEvidence.ForEach(x => Assert.AreEqual(x.Size.X, x.PointB.X - x.PointA.X));
            gridPartListEvidence.ForEach(x => Assert.AreEqual(x.Size.Y, x.PointB.Y - x.PointA.Y));

            #endregion
        }
    }
}
