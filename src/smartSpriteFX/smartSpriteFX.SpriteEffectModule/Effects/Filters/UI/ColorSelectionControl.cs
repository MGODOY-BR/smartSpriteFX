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
        /// It's the last control triggered
        /// </summary>
        private static ColorSelectionControl _lastColorSelection;

        /// <summary>
        /// Indicates if it in dropper mode
        /// </summary>
        public bool InDropperMode
        {
            get;
            private set;
        }

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

            PictureBox pictureBox = EffectFacade.GetPreviewBoardBox();

            pictureBox.MouseClick += PictureBox_MouseClick;
            pictureBox.FindForm().KeyDown += ColorSelectionControl_KeyDown;
        }

        private void btnBrowserColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this._selectedColor = colorDialog1.Color;
                this.panelColorPreview.BackColor = colorDialog1.Color;
                _lastColorSelection = this;

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
            _lastColorSelection = null;
            this.InDropperMode = !this.InDropperMode;
            if(!this.InDropperMode)
            {
                this.CancelDropper();
            }
            else
            {
                EffectFacade.GetPreviewBoardBox().Cursor = new Cursor(Resources.get_color_png.GetHicon());
                _lastColorSelection = this;
            }
        }

        private void ColorSelectionControl_KeyDown(object sender, KeyEventArgs keyDownEventArgs)
        {
            if (keyDownEventArgs.KeyCode == Keys.Escape)
            {
                this.CancelDropper();
            }
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (!this.InDropperMode) return;
            if (_lastColorSelection != this) return;

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
        }
        
        /// <summary>
        /// Cancels dropper operation
        /// </summary>
        public void CancelDropper()
        {
            PictureBox pictureBox = EffectFacade.GetPreviewBoardBox();

            this.InDropperMode = false;
            _lastColorSelection = null;
            pictureBox.Cursor = Cursors.Default;
        }
    }
}
