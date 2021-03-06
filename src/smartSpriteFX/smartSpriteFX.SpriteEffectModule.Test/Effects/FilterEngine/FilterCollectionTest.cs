﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.FilterEngine;
using smartSuite.smartSpriteFX.Effects.Filters;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;
using NSubstitute;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test
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
            FilterCollection.DoNotPutFrameIndex = false;
            this._appliedFilteresCount = 0;
            var thirdPartyPluginList = Directory.GetFiles(@"ThirdPartyEffectModulePlugin");
            foreach (var plugin in thirdPartyPluginList)
            {
                if (plugin.EndsWith(".dll"))
                {
                    try
                    {
                        File.Delete(plugin);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Errors in this process can't interropts the process
                    }
                }
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            FilterCollection.DoNotPutFrameIndex = false;
            PictureDatabase.Clear();

            this._appliedFilteresCount = 0;
            var thirdPartyPluginList = Directory.GetFiles(@"ThirdPartyEffectModulePlugin");
            foreach (var plugin in thirdPartyPluginList)
            {
                if (plugin.EndsWith(".dll"))
                {
                    try
                    {
                        File.Delete(plugin);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Errors in this process can't interropts the process
                    }
                }
            }
        }

        [TestMethod]
        public void ApplyTest()
        {
            #region Scenario setup

            string evidenceGeneratedFile = null;
            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection test = Substitute.ForPartsOf<FilterCollection>();
            test.When(x => x.GenerateFilteredImage(Arg.Any<Picture>(), Arg.Any<string>())).Do(x => evidenceGeneratedFile = x.Arg<string>());

            for (int i = 0; i < filterList.Length; i++)
            {
                var testItem = Substitute.For<IEffectFilter>();

                // Stub ApplyFilter
                testItem.When(x => x.ApplyFilter(Arg.Any<Picture>(), Arg.Any<int>()))
                    .Do(x => ApplyStub(x.Arg<Picture>(), x.Arg<int>())
                    .Returns(true));

                filterList[i] = testItem;
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
            Assert.AreEqual(48, frame.OriginalHeight);
            Assert.AreEqual(48, frame.OriginalWidth);
            Assert.AreEqual(@"Stubs\filtered\Circle.stub.0.filtered.bmp", evidenceGeneratedFile);

            #endregion
        }

        [TestMethod]
        public void ApplyDoNotPutFrameIndexTest()
        {
            #region Scenario setup

            string evidenceGeneratedFile = null;
            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection.DoNotPutFrameIndex = true;
            FilterCollection test = Substitute.ForPartsOf<FilterCollection>();
            test.When(x => x.GenerateFilteredImage(Arg.Any<Picture>(), Arg.Any<string>())).Do(x => evidenceGeneratedFile = x.Arg<string>());

            for (int i = 0; i < filterList.Length; i++)
            {
                var testItem = Substitute.For<IEffectFilter>();

                // Stub ApplyFilter
                testItem.When(x => x.ApplyFilter(Arg.Any<Picture>(), Arg.Any<int>()))
                    .Do(x => ApplyStub(x.Arg<Picture>(), x.Arg<int>())
                    .Returns(true));

                filterList[i] = testItem;
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
            Assert.AreEqual(48, frame.OriginalHeight);
            Assert.AreEqual(48, frame.OriginalWidth);
            Assert.AreEqual(@"Stubs\filtered\Circle.stub.filtered.bmp", evidenceGeneratedFile);

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
                filterList[i] = Substitute.For<IEffectFilter>();

                test.Register(filterList[i], i);
            }

            #endregion

            #region Getting the evidences

            var evidenciaList = test.GetFilterBufferList();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(filterList.Length, evidenciaList.Count);
            Assert.AreEqual(48, frame.OriginalHeight);
            Assert.AreEqual(48, frame.OriginalWidth);

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

            filterList[0] = Substitute.For<IEffectFilter>();
            test.Register(filterList[0], 1);

            filterList[1] = Substitute.For<IEffectFilter>();
            test.Register(filterList[1], 0);

            filterList[2] = Substitute.For<IEffectFilter>();
            test.Register(filterList[2], 2);

            #endregion

            #region Getting the evidences

            var evidenciaList = test.GetFilterBufferList();

            #endregion

            #region Validating the evidences

            Assert.AreEqual(filterList[1], evidenciaList[0]);
            Assert.AreEqual(filterList[0], evidenciaList[1]);
            Assert.AreEqual(filterList[2], evidenciaList[2]);
            Assert.AreEqual(48, frame.OriginalHeight);
            Assert.AreEqual(48, frame.OriginalWidth);

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
                filterList[i] = Substitute.For<IEffectFilter>();

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
            Assert.AreEqual(48, frame.OriginalHeight);
            Assert.AreEqual(48, frame.OriginalWidth);

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

                using smartSuite.smartSpriteFX.Effects.Filters;
                using smartSuite.smartSpriteFX.Pictures;
                using smartSuite.smartSpriteFX.Effects.Infra;
                using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
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

        [TestMethod]
        public void GetLoadErrorListTest()
        {
            #region Scenario setup

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
#pragma warning disable CS0618 // Type or member is obsolete
            ICodeCompiler compiler = codeProvider.CreateCompiler();
#pragma warning restore CS0618 // Type or member is obsolete
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = @"ThirdPartyEffectModulePlugin\ThirdPartyPluginMock2.dll";

            parameters.ReferencedAssemblies.Add(
                    typeof(IEffectFilter).Assembly.Location);
            parameters.ReferencedAssemblies.Add(
                    typeof(Picture).Assembly.Location);
            parameters.ReferencedAssemblies.Add(
                    typeof(System.Windows.Forms.Panel).Assembly.Location);

            CompilerResults results = compiler.CompileAssemblyFromSource(parameters, @"

                using smartSuite.smartSpriteFX.Effects.Filters;
                using smartSuite.smartSpriteFX.Pictures;
                using smartSuite.smartSpriteFX.Effects.Infra;
                using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
                using System;
                using System.Collections.Generic;
                using System.Text;

                namespace ThirdPartyEffectMock
                {
                    public class ThirdPartyEffectMock2 : IEffectFilter
                    {
                        public ThirdPartyEffectMock2()
                        {
                            throw new ApplicationException(""Teste de erro ao carregar o plugin."");
                        }

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
            Assert.AreEqual(1, evidenciaLoadErrorList.Count);
            Assert.AreNotEqual(0, evidenciaFilterPalleteList.Count);
            Assert.IsNull(evidenciaFilterPalleteList.Find(x => x.GetType().Name == "ThirdPartyEffectMock2"));

            #endregion
        }

        [TestMethod]
        public void UpFilterOrderTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection test = new FilterCollection();

            filterList[0] = new _16BitFilter();
            test.Register(filterList[0], 1);

            filterList[1] = new _24BitFilter();
            test.Register(filterList[1], 0);

            filterList[2] = new _8BitFilter();
            test.Register(filterList[2], 2);

            #endregion

            #region Running the tested operation

            test.UpOrder(filterList[2]);

            #endregion

            #region Getting the evidences

            var evidenceList = test.GetFilterBufferList();

            #endregion

            #region Validating the evidences

            Assert.AreSame(filterList[1], evidenceList[0]);
            Assert.AreSame(filterList[2], evidenceList[1]);
            Assert.AreSame(filterList[0], evidenceList[2]);
            Assert.AreEqual(48, frame.OriginalHeight);
            Assert.AreEqual(48, frame.OriginalWidth);

            #endregion
        }

        [TestMethod]
        public void DownFilterOrderTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Circle.stub.bmp");

            IEffectFilter[] filterList = new IEffectFilter[3];

            FilterCollection test = new FilterCollection();

            filterList[0] = new _16BitFilter();
            test.Register(filterList[0], 1);

            filterList[1] = new _24BitFilter();
            test.Register(filterList[1], 0);

            filterList[2] = new _8BitFilter();
            test.Register(filterList[2], 2);

            #endregion

            #region Running the tested operation

            test.DownOrder(filterList[1]);

            #endregion

            #region Getting the evidences

            var evidenceList = test.GetFilterBufferList();

            #endregion

            #region Validating the evidences

            Assert.AreSame(filterList[1], evidenceList[0]);
            Assert.AreSame(filterList[0], evidenceList[1]);
            Assert.AreSame(filterList[2], evidenceList[2]);

            #endregion
        }
    }
}