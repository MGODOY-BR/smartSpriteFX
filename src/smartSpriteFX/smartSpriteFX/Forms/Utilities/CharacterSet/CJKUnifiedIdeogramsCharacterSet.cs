using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.CharacterSet
{
    /// <summary>
    /// Represents a Chinese / Japanese / Korean character set.
    /// </summary>
    public class CJKUnifiedIdeogramsCharacterSet : ICharacterSet
    {
        public string GetMaxRange()
        {
            return "9FFF";
        }

        public string GetMinRange()
        {
            return "4E00";
        }

        public string GetShortName()
        {
            return "CJK.unified";
        }

        public override string ToString()
        {
            return "CJK - Chinese / Japanese / Korean Unified Ideograms";
        }
    }
}
