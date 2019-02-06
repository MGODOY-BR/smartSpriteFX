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
    /// Represents a control to add a list of a color
    /// </summary>
    public partial class ColorListControl : UserControl
    {
        /// <summary>
        /// Occurs when the list has changed
        /// </summary>
        public event EventHandler<ColorListChangeEventArgs> ColorListChanged;

        /// <summary>
        /// It´s a browser for selection
        /// </summary>
        private ColorSelectionControl _colorSelectionControl = new ColorSelectionControl();

        /// <summary>
        /// It´s a list of colors
        /// </summary>
        public List<Color> ColorList
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if the color selection is in dropper mode
        /// </summary>
        public bool InDropperMode
        {
            get
            {
                return this._colorSelectionControl.InDropperMode;
            }
        }

        public ColorListControl()
        {
            InitializeComponent();

            _colorSelectionControl.Dock = DockStyle.Top;
            _colorSelectionControl.SelectedColorEvent += _colorSelectionControl_SelectedColorEvent;
            this.panelBrowser.Controls.Add(_colorSelectionControl);

            this.ColorList = new List<Color>();
        }

        private void _colorSelectionControl_SelectedColorEvent(object sender, ColorSelectionControl.SelectionColorEventArgs e)
        {
            var color = e.SelectedColor;
            Button buttonColor = CreateButtonColor(color);
            panelList.Controls.Add(buttonColor);

            this.ColorList.Add(e.SelectedColor);
            this.OnColorListChanged();
        }

        /// <summary>
        /// Throws the event
        /// </summary>
        private void OnColorListChanged()
        {
            if (ColorListChanged != null)
            {
                ColorListChanged(this, new ColorListChangeEventArgs
                {
                    CurrentColorList = this.ColorList
                });
            }
        }

        /// <summary>
        /// Creates a button color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Button CreateButtonColor(Color color)
        {
            Button buttonColor = new Button();
            buttonColor.BackColor = color;
            buttonColor.Text = "X";
            buttonColor.Dock = DockStyle.Top;
            buttonColor.Click += ExcludeColorClickEvent;
            return buttonColor;
        }

        private void ExcludeColorClickEvent(object sender, EventArgs e)
        {
            Button self = (Button)sender;
            self.Parent.Controls.Remove(self);
            this.ColorList.Remove(self.BackColor);
            self.Dispose();
            this.OnColorListChanged();
        }

        private void ColorListControl_Load(object sender, EventArgs e)
        {
            if (this.ColorList != null)
            {
                foreach (var color in this.ColorList)
                {
                    this.panelList.Controls.Add(this.CreateButtonColor(color));
                }
            }
        }

        /// <summary>
        /// Represents arguments for ColorListChange events
        /// </summary>
        public class ColorListChangeEventArgs : EventArgs
        {
            /// <summary>
            /// It´s the current color list
            /// </summary>
            public List<Color> CurrentColorList { get; set; }
        }

        /// <summary>
        /// Cancels dropper command
        /// </summary>
        public void CancelDropper()
        {
            this._colorSelectionControl.CancelDropper();
        }
    }
}
