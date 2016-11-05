using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSpriteFX.Forms.Controls.LineControlState;

namespace smartSpriteFX.Forms.Controls
{
    public partial class LineControl : UserControl, IRemarkable
    {
        /// <summary>
        /// Gets or sets the hook owner
        /// </summary>
        private HookControl _hookOwner;

        /// <summary>
        /// Defines the style of linecontrol
        /// </summary>
        public LineControlStyle Style { get; private set; }

        public LineControl()
        {
            InitializeComponent();

            this.GotFocus += LineControl_GotFocus;
            this.LostFocus += LineControl_LostFocus;
        }

        public LineControl(HookControl huckControl)
        {
            InitializeComponent();

            this.GotFocus += LineControl_GotFocus;
            this.LostFocus += LineControl_LostFocus;

            this._hookOwner = huckControl;
        }

        /// <summary>
        /// Resize the line
        /// </summary>
        /// <param name="style"></param>
        /// <param name="size"></param>
        public new void Resize(LineControlStyle style, float size)
        {
            switch (style)
            {
                case LineControlStyle.Horizontal:
                    this.Height = 1;
                    this.Width = (int)size;
                    break;

                case LineControlStyle.Vertical:
                    this.Height = (int)size;
                    this.Width = 1;
                    break;

                default:
                    throw new NotSupportedException();
            }

            this.Style = style;
        }

        #region Events

        private void LineControl_LostFocus(object sender, EventArgs e)
        {
            this.Mark(false);
            this._hookOwner.Mark(false);
        }

        private void LineControl_GotFocus(object sender, EventArgs e)
        {
            this.Mark(true);
            this._hookOwner.Mark(true);
            this._hookOwner.Focus();
        }

        #endregion

        #region IRemarkable operations

        public void Mark(bool bold)
        {
            if (bold)
            {
                this.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                this.BorderStyle = BorderStyle.None;
            }
        }

        #endregion
    }
}
