using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Test.Effects.Filters
{
    [TestClass]
    public class AlignToBottomTest
    {
        [TestMethod]
        public void ApplyTest()
        {
            #region Scenario setup

            Picture frame = Picture.GetInstance(@"Stubs\Something.stub.png");

            #endregion

            #region Running the tested operation

            AlignToBottom test = new AlignToBottom();
            var evidence = test.ApplyFilter(frame, 0);

            #endregion

            #region Getting the evidences

            var evidenceMaxY = frame.GetAllPixels().Max(p => p.Y);
            var evidenceMinY = frame.GetAllPixels().Min(p => p.Y);
            
            #endregion

            #region Validating the evidences

            Assert.IsTrue(evidence, "Filter ignored");
            Assert.AreEqual(frame.OriginalHeight, frame.Height, "Height hasn't been restored");
            Assert.AreEqual(frame.Height, evidenceMaxY, "Bottom has not been aligned");
            Assert.AreNotEqual(0, evidenceMinY, "MinY hasn't been translated");

            #endregion
        }
    }
}
