﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Effects.Core;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.Effects.FilterEngine;
using System.IO;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Effects.Core
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
            PictureDatabase.Clear();
        }

        [TestMethod]
        public void UpdatePreviewBoardTest()
        {
            #region Scenario setup

            EffectEngine.Initializate(@"StubAnimation\Enumerated");

            PictureBox pictureBoxStub = new PictureBox();

            EffectEngine.SetPreviewBoard(pictureBoxStub);

            EffectEngine.GetIterator().MoveFirst();

            EffectEngine.SetSourcePreviewImage(
                EffectEngine.GetIterator().GetCurrent());

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

        [TestMethod]
        public void ApplyTest()
        {
            #region Scenario setup

            EffectEngine.Initializate(@"StubAnimation\Enumerated");

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

            EffectEngine.Apply();

            #endregion

            #region Getting the evidences

            var evidenceList = Directory.GetFiles(@"StubAnimation\Enumerated\filtered", "*.filtered.*");

            #endregion

            #region Validating the evidences

            Assert.AreEqual(10, evidenceList.Length);

            #endregion
        }

        [TestMethod]
        public void LoadFilterSetTest()
        {
            #region Scenario setup

            EffectEngine.Initializate(@"StubAnimation\Enumerated");
            EffectEngine.GetSelectedFilterList().Clear();

            #endregion

            #region Running the tested operation

            EffectEngine.LoadFilterSet(@"Stubs\stub.filterSet");

            #endregion

            #region Getting the evidences

            var evidenceLine = EffectEngine.GetSelectedFilterList();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidenceLine.Count);
            Assert.AreEqual(2.2f, ((ScaleFilter)evidenceLine[3]).Scale); 

            #endregion
        }

        [TestMethod]
        public void SaveFilterSetTest()
        {
            #region Scenario setup

            EffectEngine.Initializate(@"StubAnimation\Enumerated");

            var filterPallete = FilterCollection.GetFilterPallete();

            FilterCollection filterCollection = new FilterCollection();
            EffectEngine.GetSelectedFilterList().Clear();
            filterCollection.Register(filterPallete[0], 1);
            filterCollection.Register(filterPallete[1], 2);
            filterCollection.Register(filterPallete[2], 3);

            ScaleFilter scaleFilter = new ScaleFilter();
            scaleFilter.Scale = 2.2f;
            filterCollection.Register(scaleFilter, 4);

            int filterIndex = 0;
            foreach (var filterItem in filterCollection.GetFilterBufferList())
            {
                EffectEngine.RegisterFilter(filterItem, filterIndex);
                filterIndex++;
            }

            #endregion

            #region Running the tested operation

            EffectEngine.SaveFilterSet("test.filterSet");

            #endregion

            #region Getting the evidences

            var evidenceLine = File.OpenText("test.filterSet").ReadToEnd().Split('\n');

            #endregion

            #region Validating the evidences

            Assert.AreEqual(4, evidenceLine.Length);

            #endregion
        }
    }
}
