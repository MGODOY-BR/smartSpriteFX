using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using smartSuite.smartSpriteFX.Forms.Utilities.SpriteSheet;

namespace smartSuite.smartSpriteFX.Test.Forms.Utilities.SpriteSheet
{
    [TestClass]
    public class SpriteFileListOrganizerTest
    {
        [TestMethod]
        public void Load1000FilesTest()
        {
            #region Scenario setup

            List<String> imageStubList = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                imageStubList.Add("File" + i);
            }

            SpriteFileListOrganizer test = Substitute.ForPartsOf<SpriteFileListOrganizer>(@"c:\nononon");
            test.GetImageDimension(Arg.Any<string>()).Returns(new Dimension { Width = 0, Height = 0 });
            test.GetImageDimension("File4").Returns(new Dimension { Width = 10, Height = 10 });
            test.When(x => x.ListImages()).DoNotCallBase();
            test.ListImages().Returns(imageStubList);

            #endregion

            #region Running the tested operation

            test.Load();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(10, test.LargerSprite.Width);
            Assert.AreEqual(10, test.LargerSprite.Height);

            #endregion
        }

        [TestMethod]
        public void CalculateSpriteSheet2Files()
        {
            #region Scenario setup

            SpriteFileListOrganizer test = Substitute.ForPartsOf<SpriteFileListOrganizer>(@"c:\nononon");
            test.FileCount.Returns(2);

            #endregion

            #region Running the tested operation

            var evidence =
                test.CalculateSpriteSheet();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence.ColumnCount);
            Assert.AreEqual(2, evidence.RowCount);

            #endregion
        }

        [TestMethod]
        public void CalculateSpriteSheet16Files()
        {
            #region Scenario setup

            SpriteFileListOrganizer test = Substitute.ForPartsOf<SpriteFileListOrganizer>(@"c:\nononon");
            test.FileCount.Returns(16);

            #endregion

            #region Running the tested operation

            var evidence = 
                test.CalculateSpriteSheet();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidence.ColumnCount);
            Assert.AreEqual(4, evidence.RowCount);

            #endregion
        }

        [TestMethod]
        public void CalculateSpriteSheet4Files()
        {
            #region Scenario setup

            SpriteFileListOrganizer test = Substitute.ForPartsOf<SpriteFileListOrganizer>(@"c:\nononon");
            test.FileCount.Returns(4);

            #endregion

            #region Running the tested operation

            var evidence =
                test.CalculateSpriteSheet();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, evidence.ColumnCount);
            Assert.AreEqual(4, evidence.RowCount);

            #endregion
        }

        [TestMethod]
        public void CalculateSpriteSheet20000Files()
        {
            #region Scenario setup

            SpriteFileListOrganizer test = Substitute.ForPartsOf<SpriteFileListOrganizer>(@"c:\nononon");
            test.FileCount.Returns(20000);

            #endregion

            #region Running the tested operation

            var evidence =
                test.CalculateSpriteSheet();

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(200, evidence.ColumnCount);
            Assert.AreEqual(100, evidence.RowCount);

            #endregion
        }
    }
}
