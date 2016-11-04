using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSpriteFX.Pictures.ColorPattern
{
    /// <summary>
    /// Represents an information from a color
    /// </summary>
    public class ColorInfo
    {
        /// <summary>
        /// It´s an inner color
        /// </summary>
        private Color _innerColor;

        /// <summary>
        /// Creates an instance of the object
        /// </summary>
        /// <param name="innerColor"></param>
        public ColorInfo(Color innerColor)
        {
            this._innerColor = innerColor;
        }

        /// <summary>
        /// Gets the inner color
        /// </summary>
        /// <returns></returns>
        public Color GetInnerColor()
        {
            return this._innerColor;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            ColorInfo other = obj as ColorInfo;
            if (other == null)
            {
                return false;
            }

            return this._innerColor.ToArgb().Equals(
                other._innerColor.ToArgb());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            #region Entries validation

            if (this._innerColor == null)
            {
                throw new ArgumentNullException("this._innerColor");
            }

            #endregion

            return this._innerColor.ToArgb().GetHashCode();
        }

        public ColorInfo Clone()
        {
            return new ColorInfo(this._innerColor);
        }

        /// <summary>
        /// Changes the inner color
        /// </summary>
        /// <param name="newColor"></param>
        internal void SetInnerColor(Color newColor)
        {
            this._innerColor = newColor;
        }
    }
}
