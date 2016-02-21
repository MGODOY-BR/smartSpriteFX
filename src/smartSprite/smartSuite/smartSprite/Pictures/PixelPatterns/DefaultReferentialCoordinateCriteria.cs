
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace smartSuite.smartSprite.Pictures.PixelPatterns{
	/// <summary>
	/// Represents a referential corrdinate criteria
	/// </summary>
	public class DefaultReferentialCoordinateCriteria : IReferentialCoordinateCriteria {

        /// <summary>
        /// It´s the last X coordinate
        /// </summary>
        private int _lastX;

        /// <summary>
        /// It´s the last Y coordinate
        /// </summary>
        private int _lastY;

        /// <summary>
        /// It´s a regular expression to get coordinate from keys
        /// </summary>
        private Regex _regExCoordinate = new Regex(@"(\d+)_(\d+)", RegexOptions.Compiled);

        /// <summary>
        /// Represents a referential corrdinate criteria
        /// </summary>
        public DefaultReferentialCoordinateCriteria() {
		}

        /// <summary>
        /// Gets a boolean informing the validation of  the coordinate
        /// </summary>
        public bool IsValid(Dictionary<String, Color> leanrtCache, int x, int y)
        {
            #region Entries validation

            if (leanrtCache == null)
            {
                throw new ArgumentNullException("leanrtCache");
            }

            #endregion

            this.provideLastCoordinate(leanrtCache);

            return y < this._lastY;
        }

        /// <summary>
        /// Feed the last coordinate
        /// </summary>
        /// <param name="leanrtCache"></param>
        private void provideLastCoordinate(Dictionary<String, Color> leanrtCache)
        {
            #region Entries validation

            if (leanrtCache == null)
            {
                throw new ArgumentNullException("leanrtCache");
            }
            if (this._lastX != 0 && this._lastY != 0)
            {
                return;
            }

            #endregion

            foreach (var keyItem in leanrtCache.Keys)
            {
                Match match = this._regExCoordinate.Match(keyItem);
                if (!match.Success)
                {
                    throw new ArgumentException("Error to find coordinates");
                }

                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);

                if (x > this._lastX)
                {
                    this._lastX = x;
                }
                if (y > this._lastY)
                {
                    this._lastY = y;
                }
            }
        }
    }
}