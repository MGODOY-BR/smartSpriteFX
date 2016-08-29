using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using smartSuite.smartSprite.Pictures;
using smartSuite.smartSprite.Effects.FilterEngine;
using smartSuite.smartSprite.Effects.Filters;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using smartSuite.smartSprite.Effects.Infra;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;

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
            var thirdPartyPluginList = Directory.GetFiles(@"ThirdPartyEffectModulePlugin");
            foreach (var plugin in thirdPartyPluginList)
            {
                if (plugin.EndsWith(".dll"))
                {
                    File.Delete(plugin);
                }
            }
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

        [TestMethod]
        public void LoadTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            FilterCollection.Load();

            #endregion

            #region Getting the evidences

            var evidenciaLoadErrorList = FilterCollection.GetLoadErrorList();
            var evidenciaFilterPalleteList = FilterCollection.GetFilterPallete();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, evidenciaLoadErrorList.Count);
            Assert.AreNotEqual(0, evidenciaFilterPalleteList.Count);

            #endregion
        }

        [TestMethod]
        public void LoadThirdPartyPluginsTest()
        {
            #region Scenario setup

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
#pragma warning disable CS0618 // Type or member is obsolete
            ICodeCompiler compiler = codeProvider.CreateCompiler();
#pragma warning restore CS0618 // Type or member is obsolete
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = @"ThirdPartyEffectModulePlugin\ThirdPartyPluginMock.dll";

            parameters.ReferencedAssemblies.Add(
                    typeof(IEffectFilter).Assembly.Location);
            parameters.ReferencedAssemblies.Add(
                    typeof(Picture).Assembly.Location);
            parameters.ReferencedAssemblies.Add(
                    typeof(System.Windows.Forms.Panel).Assembly.Location);

            CompilerResults results = compiler.CompileAssemblyFromSource(parameters, @"

                using smartSuite.smartSprite.Effects.Filters;
                using smartSuite.smartSprite.Pictures;
                using smartSuite.smartSprite.Effects.Infra;
                using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
                using System;
                using System.Collections.Generic;
                using System.Text;

                namespace ThirdPartyEffectMock
                {
                    public class ThirdPartyEffectMock : IEffectFilter
                    {
                        public bool ApplyFilter(Picture frame, int index){return false;}

                        public Identification GetIdentification(){return null;}

                        public IConfigurationPanel ShowConfigurationPanel(){return null;}

                        public void Reset(){}
                    }
                }");

            #endregion

            #region Running the tested operation

            FilterCollection.Load();

            #endregion

            #region Getting the evidences

            var evidenciaLoadErrorList = FilterCollection.GetLoadErrorList();
            var evidenciaFilterPalleteList = FilterCollection.GetFilterPallete();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, results.Output.Count);
            Assert.AreEqual(0, evidenciaLoadErrorList.Count);
            Assert.AreNotEqual(0, evidenciaFilterPalleteList.Count);
            Assert.IsNotNull(evidenciaFilterPalleteList.Find(x => x.GetType().Name == "ThirdPartyEffectMock"));

            #endregion
        }
    }
}
