using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures
{
    /// <summary>
    /// Defines how an object that can be filled from  a string
    /// </summary>
    public interface IFromString
    {
        /// <summary>
        /// Fills the data
        /// </summary>
        /// <param name="valueString"></param>
        void FillMe(String valueString);
    }
}
