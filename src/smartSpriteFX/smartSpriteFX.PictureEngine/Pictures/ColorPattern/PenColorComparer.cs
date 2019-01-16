using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern
{
    /// <summary>
    /// It's a comparer prepared to identify just pen colors
    /// </summary>
    public class PenColorComparer : IColorFilter
    {
        /// <summary>
        /// It's the cache of point
        /// </summary>
        private smartSuite.smartSpriteFX.Pictures.Point _pointCache;

        /// <summary>
        /// It's the photo of original point
        /// </summary>
        private smartSuite.smartSpriteFX.Pictures.Point _pointPhoto;

        /// <summary>
        /// It's a list of point sizes
        /// </summary>
        private List<smartSuite.smartSpriteFX.Pictures.Point> _pointSizeList = new List<smartSuite.smartSpriteFX.Pictures.Point>();

        /// <summary>
        /// It's an addictional darkness used to ignore more colors
        /// </summary>
        private int _extraDarkness;

        /// <summary>
        /// It's the last validation
        /// </summary>
        private bool _lastValidation = false;

        /// <summary>
        /// It's an addictional darkness used to ignore more colors
        /// </summary>
        public int ExtraDarkness { get => _extraDarkness; set => _extraDarkness = value; }

        public PenColorComparer()
        {

        }
        public PenColorComparer(int extraDarkness)
        {
            this._extraDarkness = extraDarkness;
        }

        public bool IsPencilColor(Color color)
        {
            int darknessCommonComponents = 125 - this._extraDarkness;
            if (darknessCommonComponents < 0) darknessCommonComponents = 0;

            int redComponent = 200; //170;
            if (redComponent < 0) redComponent = 0;

            return
                color.A == 255 &&
                color.R <= redComponent &&
                color.G <= darknessCommonComponents &&
                color.B <= darknessCommonComponents;
        }

        public bool IsValid(int x, int y, Color color)
        {
            bool returnValue = 
                this.IsPencilColor(color);

            if (returnValue)
            {
                this.UpdateTraceAttributes(x, y);
            }

            if (returnValue != this._lastValidation)
            {
                this._pointSizeList.Add(this._pointCache);
                this._pointCache = null;
            }
            this._lastValidation = returnValue;

            return returnValue;
        }

        /// <summary>
        /// Updates trace attributes
        /// </summary>
        /// <remarks>So far, Y is not been considered properly. It is needed control all the parallel Y</remarks>
        private void UpdateTraceAttributes(int x, int y)
        {
            if (this._pointCache == null)
            {
                this._pointPhoto = new smartSpriteFX.Pictures.Point(x, y);
                this._pointCache = new smartSpriteFX.Pictures.Point();
            }

            this._pointCache.X = x - this._pointPhoto.X;
            this._pointCache.Y = y - this._pointPhoto.Y;
        }

        /// <summary>
        /// Calculate the point radio
        /// </summary>
        /// <returns></returns>
        public float CalculatePointRadio()
        {
            #region Entries validation

            if(this._pointSizeList.Count == 0)
            {
                return 0;
            }

            #endregion

            // Copyng and removing the lower points
            var pointSizeList = new List<smartSuite.smartSpriteFX.Pictures.Point>();
            pointSizeList.AddRange(this._pointSizeList);

            // Removing the lowest point
            pointSizeList.RemoveAt(0);
            pointSizeList.RemoveAll(p => p == null);

            // Calculating the average
            return (int)pointSizeList.Average(p => Math.Sqrt((p.X * p.X) + (p.Y * p.Y)) * 2);
        }

    }
}
