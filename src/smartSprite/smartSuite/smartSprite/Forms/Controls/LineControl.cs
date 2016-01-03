using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSprite.Forms.Controls.LineControlState;

namespace smartSprite.Forms.Controls
{
    public partial class LineControl : UserControl
    {
        /// <summary>
        /// Defines the style of linecontrol
        /// </summary>
        public LineControlStyle Style { get; private set; }

        public LineControl()
        {
            InitializeComponent();
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
                    this.Height = 3;
                    this.Width = (int)size;
                    break;

                case LineControlStyle.Vertical:
                    this.Height = (int)size;
                    this.Width = 3;
                    break;

                default:
                    throw new NotSupportedException();
            }

            this.Style = style;
        }
    }
}
