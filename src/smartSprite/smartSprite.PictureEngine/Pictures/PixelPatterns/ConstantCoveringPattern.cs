
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSpriteFX.Pictures.PixelPatterns{
	/// <summary>
	/// It´s a covering algorithm for constant covering based in the last line color
	/// </summary>
	internal class ConstantCoveringPattern : ICoveringPattern {

		public ConstantCoveringPattern() {
		}

        /// <summary>
        /// It´s a referential pattern range
        /// </summary>
        private List<PixelInfo> _referentialRangeList = new List<PixelInfo>();

        /// <summary>
        /// It´s referential coordinate Y
        /// </summary>
        private int _referentialY;

        /// <summary>
        /// Gets a pixel correspondent to coordinates
        /// </summary>
        /// <param name="pixelInfoList"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(List<PixelInfo> pixelInfoList, int x, int y)
        {
            #region Entries validation

            if (pixelInfoList == null)
            {
                throw new ArgumentNullException("pixelInfoList");
            }

            #endregion

            this.provideReferentialPatternLine(pixelInfoList, x, y);

            var results = from pixelInfoItem in _referentialRangeList
                          where
                                pixelInfoItem.X == x
                                && pixelInfoItem.Y == this._referentialY
                          select pixelInfoItem;

            var returnedPixel = results.LastOrDefault();

            #region Entries validation

            if (returnedPixel == null)
            {
                return Color.Black;
            }

            #endregion

            return returnedPixel.Color;
        }

        /// <summary>
        /// Fills the referentialPatternLine attribute
        /// </summary>
        /// <param name="pixelInfoList"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void provideReferentialPatternLine(List<PixelInfo> pixelInfoList, int x, int y)
        {
            #region Entries validation

            if (pixelInfoList == null)
            {
                throw new ArgumentNullException("pixelInfoList");
            }
            if (_referentialRangeList == null)
            {
                throw new ArgumentNullException("_referentialRangeList");
            }
            if (_referentialRangeList.Count > 0)
            {
                return;
            }

            #endregion

            this._referentialY = y - 1;

            var pixelInfoLineList = from pixelItem in pixelInfoList
                                    where
                                        pixelItem.Y == this._referentialY
                                    select pixelItem;

            this._referentialRangeList.AddRange(pixelInfoLineList);
        }
    }
}