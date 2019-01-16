using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using smartSuite.smartSpriteFX.Forms.Utilities.SpriteSheet;
using System.IO;

namespace smartSuite.smartSpriteFX.Test.Forms.Utilities.SpriteSheet
{
    [TestClass]
    public class SpriteSheetMakerTest
    {
        [TestMethod]
        public void GenerateSingleColumnImagesTest()
        {
            #region Scenario setup

            SpriteFileListOrganizer organizerMock = Substitute.For<SpriteFileListOrganizer>(@"C:\pictures");
            organizerMock.CalculateSpriteSheet().Returns(new SpriteMatrixInfo {RowCount=4, ColumnCount=1});
            organizerMock.LargerSprite.Returns(new Dimension { Width = 10, Height = 40 });
            organizerMock.ListImages().Returns(new List<string> { @"C:\pictures\nonono1.png", @"C:\pictures\nononon2.png", @"C:\pictures\nononon3.png", @"C:\pictures\nononon4.png" });
            organizerMock.FileCount.Returns(4);

            List<RectangleF> evidenceAreaList = new List<RectangleF>();

            DrawerDelegate mockDelegate = Substitute.For<DrawerDelegate>(new Bitmap(10, 160));
            mockDelegate.When(x => x.DrawImage(Arg.Any<Image>(), Arg.Any<RectangleF>())).Do( x => evidenceAreaList.Add(x.Arg<RectangleF>()) );
            mockDelegate.GetImage(Arg.Any<string>()).Returns(new Bitmap(10, 40));

            SpriteSheetMaker test = Substitute.ForPartsOf<SpriteSheetMaker>(organizerMock);
            test.GetDrawerDelegate(Arg.Any<Image>()).Returns(mockDelegate);
            test.GetStreamUVInfo(Arg.Any<String>()).Returns(new StreamWriter(new MemoryStream()));
            test.GetStreamText(Arg.Any<String>()).Returns(new StreamWriter(new MemoryStream()));

            #endregion

            #region Running the tested operation

            test.Generate(@"C:\pictures\nnononFolder");

            #endregion

            #region Getting the evidences

            var evidenceTotalHeight = evidenceAreaList.Sum(x => x.Height);
            var evidenceMaxLeft = evidenceAreaList.Max(x => x.Left);

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidenceAreaList.Count);
            Assert.AreEqual(160, evidenceTotalHeight);
            Assert.AreEqual(0, evidenceMaxLeft);
            for (int i = 0; i < 4; i++)
            {
                var evidenciaAreaItem = evidenceAreaList[i];

                Assert.AreEqual(0, evidenciaAreaItem.Left);
                Assert.AreEqual(i * 40, evidenciaAreaItem.Top);
            }

            #endregion
        }

        [TestMethod]
        public void GenerateMultipleColumnsImagesTest()
        {
            #region Scenario setup

            List<string> imageList = new List<string>();
            for (int i = 1; i < 13; i++)
            {
                imageList.Add(@"C:\pictures\nonono" + i + ".png");
            }

            SpriteFileListOrganizer organizerMock = Substitute.For<SpriteFileListOrganizer>(@"C:\pictures");
            organizerMock.CalculateSpriteSheet().Returns(new SpriteMatrixInfo { RowCount = 4, ColumnCount = 3 });
            organizerMock.LargerSprite.Returns(new Dimension { Width = 10, Height = 40 });
            organizerMock.ListImages().Returns(imageList);
            organizerMock.FileCount.Returns(12);

            List<RectangleF> evidenceAreaList = new List<RectangleF>();

            DrawerDelegate mockDelegate = Substitute.For<DrawerDelegate>(new Bitmap(10, 160));
            mockDelegate.When(x => x.DrawImage(Arg.Any<Image>(), Arg.Any<RectangleF>())).Do(x => evidenceAreaList.Add(x.Arg<RectangleF>()));
            mockDelegate.GetImage(Arg.Any<string>()).Returns(new Bitmap(10, 40));

            SpriteSheetMaker test = Substitute.ForPartsOf<SpriteSheetMaker>(organizerMock);
            test.GetDrawerDelegate(Arg.Any<Image>()).Returns(mockDelegate);
            test.GetStreamUVInfo(Arg.Any<String>()).Returns(new StreamWriter(new MemoryStream()));
            test.GetStreamText(Arg.Any<String>()).Returns(new StreamWriter(new MemoryStream()));

            #endregion

            #region Running the tested operation

            test.Generate(@"C:\pictures\nnononFolder");

            #endregion

            #region Getting the evidences

            var evidenceMaxTop = evidenceAreaList.Max(x => x.Top);
            var evidenceMaxLeft = evidenceAreaList.Max(x => x.Left);

            #endregion

            #region Validating the evidences

            Assert.AreEqual(12, evidenceAreaList.Count);
            Assert.AreEqual(120, evidenceMaxTop);
            Assert.AreEqual(20, evidenceMaxLeft);

            List<RectangleF>.Enumerator enumerator = evidenceAreaList.GetEnumerator();

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    var evidenciaAreaItem = enumerator.Current;

                    Assert.AreEqual(x * 10, evidenciaAreaItem.Left);
                    Assert.AreEqual(y * 40, evidenciaAreaItem.Top);
                }
            }

            #endregion
        }

    }
}
