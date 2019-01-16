
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Settings{
	/// <summary>
	/// Joins the panting IA settings
	/// </summary>
	public class PaintingSettings {

		/// <summary>
		/// It's the color strenght
		/// </summary>
		public float ColorStrenght
        {
            get;
            set;
        }

        /// <summary>
        /// It's the precision of grid
        /// </summary>
        public float PrecisionGrid { get; set; }
    }
}