using smartSuite.smartSprite.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSprite.Pictures;

namespace smartSprite.Forms
{
    /// <summary>
    /// Represents a form to ask for the user supports for a definition
    /// after a color undefinition created by some color algorithm
    /// </summary>
    public partial class ColorSupportForm : Form, IAskingForColorDelegate
    {
        /// <summary>
        /// It´s  picture based just in generated piece.
        /// </summary>
        private Picture _piecePicture;

        /// <summary>
        /// It´s a selected color
        /// </summary>
        private Color _selectedColor;

        public ColorSupportForm()
        {
            InitializeComponent();

            this.pieceImageBox.MouseClick += PieceImageBox_MouseClick;
        }

        /// <summary>
        /// Sets the asking to do to user.
        /// </summary>
        /// <param name="asking"></param>
        public void SetAsking(String asking)
        {
            this.lblAsking.Text = asking;
        }

        /// <summary>
        /// Asks for support from user to indicate how of colors patterns identified in search is the background color.
        /// </summary>
        /// <param name="piece">The piece that has been analysed.</param>
        /// <param name="colorList"></param>
        /// <returns></returns>
        public Color AnswerMe(Piece piece, List<Color> colorList)
        {
            #region Entries validation

            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }

            #endregion

            this._piecePicture =
                new Picture(piece.GetTakenPictureFullFileName());

            this.pieceImageBox.Load(
                piece.GetTakenPictureFullFileName());

            this.ShowDialog();

            this._piecePicture.Dispose();
            this.colorBox.Dispose();
            this.pieceImageBox.Dispose();

            return this._selectedColor;
        }

        private void PieceImageBox_MouseClick(object sender, MouseEventArgs e)
        {
            var pixelInfo =
                this._piecePicture.GetPixel(e.X, e.Y);

            colorBox.BackColor = pixelInfo;
            this._selectedColor = pixelInfo;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
