using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.SpriteEffectModule.Properties;
using System.IO;
using smartSuite.smartSpriteFX.Effects.Facade;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    /// <summary>
    /// Represents a control to offer color selection capabilities
    /// </summary>
    public partial class ColorSelectionControl : UserControl
    {
        /// <summary>
        /// Indicates if it in dropper mode
        /// </summary>
        private bool _inDropperMode;

        /// <summary>
        /// Occurs when the color is selected
        /// </summary>
        public event EventHandler<SelectionColorEventArgs> SelectedColorEvent;

        /// <summary>
        /// It´s the selected color
        /// </summary>
        private Color _selectedColor;

        public ColorSelectionControl()
        {
            InitializeComponent();
        }

        private void btnBrowserColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this._selectedColor = colorDialog1.Color;
                this.panelColorPreview.BackColor = colorDialog1.Color;

                if (SelectedColorEvent != null)
                {
                    SelectedColorEvent(this, new SelectionColorEventArgs
                    {
                        SelectedColor = this._selectedColor
                    });
                }
            }
        }

        /// <summary>
        /// Gets or sets a color
        /// </summary>
        public Color SelectedColor
        {
            get
            {
                return this._selectedColor;
            }
            set
            {
                this._selectedColor = value;
                colorDialog1.Color = value;
            }
        }

        /// <summary>
        /// It´s argument for selection color events
        /// </summary>
        public class SelectionColorEventArgs : EventArgs
        {
            /// <summary>
            /// It´s the selected color
            /// </summary>
            public Color SelectedColor { get; set; }
        }

        private void ColorSelectionControl_Load(object sender, EventArgs e)
        {
            this.panelColorPreview.BackColor = this._selectedColor;
        }

        private void btnDropper_Click(object sender, EventArgs e)
        {
            this._inDropperMode = !this._inDropperMode;

            PictureBox pictureBox = EffectFacade.GetPreviewBoardBox();
            pictureBox.MouseClick += delegate (object mouseSender, MouseEventArgs mouseEventArgs)
            {
                if (!this._inDropperMode) return;
                this._selectedColor =
                    ((Bitmap)EffectFacade.UpdatePreviewBoard()).GetPixel(mouseEventArgs.X, mouseEventArgs.Y);

                this.panelColorPreview.BackColor = this._selectedColor;

                if (SelectedColorEvent != null)
                {
                    SelectedColorEvent(this, new SelectionColorEventArgs
                    {
                        SelectedColor = this._selectedColor
                    });
                }
            };

            pictureBox.FindForm().KeyDown += delegate (object keySender, KeyEventArgs keyDownEventArgs)
            {
                if(keyDownEventArgs.KeyCode == Keys.Escape)
                {
                    this._inDropperMode = false;
                    pictureBox.Cursor = Cursors.Default;
                }
            };

            if (this._inDropperMode)
            {
                pictureBox.Cursor = new Cursor(Resources.get_color_png.GetHicon());
            }
            else
            {
                pictureBox.Cursor = Cursors.Default;
            }
        }
    }
}
