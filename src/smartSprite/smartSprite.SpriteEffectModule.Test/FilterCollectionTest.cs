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
        delegate bool ApplyFilterDelegate(Picture frame, int index);

        private bool ApplyStub(Picture frame, int index)
        {
            Console.WriteLine("Teste");
            return true;
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
                filterList[i].Stub<IEffectFilter>(x=>x.ApplyFilter(null, 0))
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

            #endregion
        }
    }
}
