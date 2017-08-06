using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.SpriteEffectModule.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Animations
{
    [TestClass]
    public class AnimationComparerTest
    {
        [TestMethod]
        public void CompareTest()
        {
            #region Scenario setup

            List<String> stringList = new List<string>
            {
                "0001.1.filtered.png",
                "0005.5.filtered.png",
                "0003.3.filtered.png",
            };

            #endregion

            #region Running the tested operation

            AnimationComparer test = new AnimationComparer();
            stringList.Sort(test);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual("0001.1.filtered.png", stringList[0]);
            Assert.AreEqual("0003.3.filtered.png", stringList[1]);
            Assert.AreEqual("0005.5.filtered.png", stringList[2]);

            #endregion
        }

        [TestMethod]
        public void CompareTest2()
        {
            #region Scenario setup

            List<String> stringList = new List<string>
            {
                "00001.png",
                "00003.png",
                "00002.png",
            };

            #endregion

            #region Running the tested operation

            AnimationComparer test = new AnimationComparer();
            stringList.Sort(test);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual("00001.png", stringList[0]);
            Assert.AreEqual("00002.png", stringList[1]);
            Assert.AreEqual("00003.png", stringList[2]);

            #endregion
        }

        [TestMethod]
        public void CompareTest3()
        {
            #region Scenario setup

            List<String> stringList = new List<string>
            {
                "1_Circle.stub.bmp",
                "10_Circle.stub.bmp",
                "2_Circle.stub.bmp",
            };

            #endregion

            #region Running the tested operation

            AnimationComparer test = new AnimationComparer();
            stringList.Sort(test);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual("1_Circle.stub.bmp", stringList[0]);
            Assert.AreEqual("2_Circle.stub.bmp", stringList[1]);
            Assert.AreEqual("10_Circle.stub.bmp", stringList[2]);

            #endregion
        }

        [TestMethod]
        public void CompareTest4()
        {
            #region Scenario setup

            List<String> stringList = new List<string>
            {
                "0011.png0000.png",
                "0011.png0010.png",
                "0011.png0002.png",
            };

            #endregion

            #region Running the tested operation

            AnimationComparer test = new AnimationComparer();
            stringList.Sort(test);

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual("0011.png0000.png", stringList[0]);
            Assert.AreEqual("0011.png0002.png", stringList[1]);
            Assert.AreEqual("0011.png0010.png", stringList[2]);

            #endregion
        }
    }
}
