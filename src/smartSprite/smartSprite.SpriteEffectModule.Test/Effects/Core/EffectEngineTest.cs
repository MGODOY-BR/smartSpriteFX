using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSprite.Effects.Core;
using System.Windows.Forms;
using smartSuite.smartSprite.Effects.FilterEngine;

namespace smartSprite.SpriteEffectModule.Test.Effects.Core
{
    [TestClass]
    public class EffectEngineTest
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
        public void UpdatePreviewBoardTest()
        {
            #region Scenario setup

            EffectEngine.Initializate(@"StubAnimation\Enumerated");

            PictureBox pictureBoxStub = new PictureBox();
            EffectEngine.SetPreviewBoard(pictureBoxStub);

            var filterPallete = FilterCollection.GetFilterPallete();

            FilterCollection filterCollection = new FilterCollection();
            filterCollection.Register(filterPallete[0], 1);
            filterCollection.Register(filterPallete[1], 2);
            filterCollection.Register(filterPallete[2], 3);

            int filterIndex = 0;
            foreach (var filterItem in filterCollection.GetFilterBufferList())
            {
                EffectEngine.RegisterFilter(filterItem, filterIndex);
                filterIndex++;
            }

            #endregion

            #region Running the tested operation

            EffectEngine.UpdatePreviewBoard();

            #endregion

            #region Getting the evidences

            var evidenceImage = pictureBoxStub.Image;

            #endregion

            #region Validating the evidences

            Assert.IsNotNull(evidenceImage);

            #endregion
        }
    }
}
