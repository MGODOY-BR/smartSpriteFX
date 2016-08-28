using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using smartSuite.smartSprite.Pictures;
using smartSuite.smartSprite.Effects.FilterEngine;
using smartSuite.smartSprite.Effects.Filters;

namespace smartSprite.SpriteEffectModule.Test
{
    [TestClass]
    public class FilterCollectionTest
    {
        /// <summary>
        /// Counts the amount of applied filters during the scenarios
        /// </summary>
        private int _appliedFilteresCount;

        /// <summary>
        /// It´s a delegate of method
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        delegate bool ApplyFilterDelegate(Picture frame, int index);

        /// <summary>
        /// It´s the stub for the method
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ApplyStub(Picture frame, int index)
        {
            _appliedFilteresCount++;
            return true;
        }

        [TestInitialize]
        public void Setup()
        {
            this._appliedFilteresCount = 0;
        }

        [TestMethod]
        public void ApplyTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection test = new FilterCollection();

            for (int i = 0; i < filterList.Length; i++)
            {
                filterList[i] = MockRepository.GenerateMock<IEffectFilter>();

                // Stub ApplyFilter
                filterList[i].Stub<IEffectFilter>(x => x.ApplyFilter(null, 0))
                    .IgnoreArguments()
                    .Do(new ApplyFilterDelegate(ApplyStub));

                test.Register(filterList[i], i);
            }

            #endregion

            #region Running the tested operation

            test.Apply(frame, 0);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(filterList.Length, _appliedFilteresCount);

            #endregion
        }

        [TestMethod]
        public void RegisterTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection test = new FilterCollection();

            #endregion

            #region Running the tested operation

            for (int i = 0; i < filterList.Length; i++)
            {
                filterList[i] = MockRepository.GenerateMock<IEffectFilter>();

                test.Register(filterList[i], i);
            }

            #endregion

            #region Getting the evidences

            var evidenciaList = test.GetFilterBufferList();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(filterList.Length, evidenciaList.Count);

            #endregion
        }

        [TestMethod]
        public void RegisterOrderedTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection test = new FilterCollection();

            #endregion

            #region Running the tested operation

            filterList[0] = MockRepository.GenerateMock<IEffectFilter>();
            test.Register(filterList[0], 1);

            filterList[1] = MockRepository.GenerateMock<IEffectFilter>();
            test.Register(filterList[1], 0);

            filterList[2] = MockRepository.GenerateMock<IEffectFilter>();
            test.Register(filterList[2], 2);

            #endregion

            #region Getting the evidences

            var evidenciaList = test.GetFilterBufferList();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(filterList[1], evidenciaList[0]);
            Assert.AreEqual(filterList[0], evidenciaList[1]);
            Assert.AreEqual(filterList[2], evidenciaList[2]);

            #endregion
        }

        [TestMethod]
        public void UnRegisterTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection test = new FilterCollection();

            for (int i = 0; i < filterList.Length; i++)
            {
                filterList[i] = MockRepository.GenerateMock<IEffectFilter>();

                test.Register(filterList[i], i);
            }

            #endregion

            #region Running the tested operation

            test.UnRegister(filterList[2]);

            #endregion

            #region Getting the evidences

            var evidenciaList = test.GetFilterBufferList();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(filterList.Length - 1, evidenciaList.Count);

            #endregion
        }
    }
}
