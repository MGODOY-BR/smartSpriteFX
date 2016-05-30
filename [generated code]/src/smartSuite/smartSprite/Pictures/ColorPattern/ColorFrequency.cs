using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSprite.Pictures.ColorPattern
{
    /// <summary>
    /// Represents a frequency of a color
    /// </summary>
    class ColorFrequency
    {
        public Color Color { get; set; }
        public int Frequency { get; set; }
        public int Length { get; set; }
        /// <summary>
        /// Gets the percentage of frequency after length
        /// </summary>
        /// <returns></returns>
        public float GetPercentage()
        {
            return ((float)this.Frequency / (float)this.Length) * 100;
        }
    }
}
