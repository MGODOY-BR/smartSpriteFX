
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures.ColorPattern{
	/// <summary>
	/// Defines how an algorithm to ask support from user to define the background color definition  must be.
	/// </summary>
	public interface IAskingForColorDelegate {

		/// <summary>
		/// Asks for support from user to indicate how of colors patterns identified in search is the background color.
		/// </summary>
		/// <param name="piece">The piece that has been analysed.</param>
		/// <param name="colorList"></param>
		/// <returns></returns>
		Color AnswerMe(Piece piece, List<Color> colorList);
	}
}