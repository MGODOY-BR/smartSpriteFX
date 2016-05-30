
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures.PixelPatterns{
	/// <summary>
	/// Represents informations about to a pixel
	/// </summary>
	internal class PixelInfo : IComparable<PixelInfo>
    {

		public PixelInfo() {
		}

		/// <summary>
		/// 
		/// </summary>
		public int X
        {
            get;
            set;
        }

		/// <summary>
		/// 
		/// </summary>
		public int Y
        {
            get;
            set;
        }

		/// <summary>
		/// 
		/// </summary>
		public Color Color
        {
            get;
            set;
        }

        public int CompareTo(PixelInfo other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            #endregion

            if (this.Y == other.Y)
            {
                return this.X.CompareTo(other.X);
            }
            else
            {
                return this.Y.CompareTo(other.Y);
            }
        }

        public override int GetHashCode()
        {
            return
                String.Format("{0}_{1}", this.X, this.Y).GetHashCode();
        }

        public override string ToString()
        {
            return
                String.Format("{0}, {1}", this.X, this.Y);
        }
    }
}