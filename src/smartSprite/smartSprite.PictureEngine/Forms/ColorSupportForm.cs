using smartSuite.smartSprite.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// It´s a piece which owns the image
        /// </summary>
        private Piece _piece;

        /// <summary>
        /// It´s a selected color
        /// </summary>
        private Color _selectedColor;

        public ColorSupportForm()
        {
            InitializeComponent();

            this.pieceImageBox.MouseClick += PieceImageBox_MouseClick;
            this.zoomFactor.ValueChanged += ZoomFactor_ValueChanged;
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
        public virtual Color AnswerMe(Piece piece, List<Color> colorList)
        {
            #region Entries validation

            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }

            #endregion

            this._piecePicture =
               Picture.GetInstance(piece.GetTakenPictureFullFileName());

            this.pieceImageBox.Load(
                piece.GetTakenPictureFullFileName());

            this._piece = piece;

            this.ShowDialog();

            this._piecePicture.Dispose();
            this.colorBox.Dispose();
            this.pieceImageBox.Dispose();

            return this._selectedColor;
        }

        private void PieceImageBox_MouseClick(object sender, MouseEventArgs e)
        {
            var pixelInfo =
                this._piecePicture.GetPixel(
                    e.X / (int)zoomFactor.Value, 
                    e.Y / (int)zoomFactor.Value);


            #region Entries validation

            if (pixelInfo == null)
            {
                throw new ArgumentNullException("pixelInfo");
            }

            #endregion

            colorBox.BackColor = pixelInfo.Value;
            this._selectedColor = pixelInfo.Value;
        }

        private void ZoomFactor_ValueChanged(object sender, EventArgs e)
        {
            int currentZoomFactor = (int)zoomFactor.Value;

            this.pieceImageBox.SizeMode = PictureBoxSizeMode.Zoom;

            var width = Math.Abs(this._piece.PointC.X - this._piece.PointA.X);
            var height = Math.Abs(this._piece.PointB.Y - this._piece.PointA.Y);

            this.pieceImageBox.Width = (int)(width * currentZoomFactor);
            this.pieceImageBox.Height = (int)(height * currentZoomFactor);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
