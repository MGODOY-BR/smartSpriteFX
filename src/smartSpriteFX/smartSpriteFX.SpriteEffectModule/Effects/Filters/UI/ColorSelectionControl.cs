using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    /// <summary>
    /// Represents a control to offer color selection capabilities
    /// </summary>
    public partial class ColorSelectionControl : UserControl
    {
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
    }
}
