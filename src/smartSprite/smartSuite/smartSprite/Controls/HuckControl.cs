using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSprite.Controls
{
    public partial class HuckControl : UserControl
    {
        /// <summary>
        /// Sets or gets the pair of huck
        /// </summary>
        public HuckControl Pair
        {
            get;
            set;
        }

        public HuckControl()
        {
            InitializeComponent();

            this.MouseMove += HuckControl_MouseMove;
        }

        /// <summary>
        /// Updates the lines between the pairs
        /// </summary>
        public void UpdateLines()
        {
            #region Entries validation

            if (this.Pair == null)
            {
                return;
            }

            #endregion

            /*
            Shape:

            A-------C
            |       |
            D-------B
            
            */

            LineControl AC = new LineControl();
            LineControl AD = new LineControl();
            LineControl DB = new LineControl();
            LineControl CB = new LineControl();

            Point currentPoint = new Point(this.Left + (this.Width / 2), this.Top + (this.Height / 2));
            Point pairPoint = new Point(this.Pair.Left + (this.Pair.Width / 2), this.Pair.Top + (this.Pair.Height / 2));

            AC.Resize(LineControlState.LineControlStyle.Horizontal, currentPoint.X - pairPoint.X);
            DB.Resize(AC.Style, AC.Width);

            AD.Resize(LineControlState.LineControlStyle.Vertical, currentPoint.Y - pairPoint.Y);
            CB.Resize(AD.Style, AD.Height);

            AC.Top = pairPoint.Y;
            AC.Left = pairPoint.X;

            DB.Top = AC.Top + AD.Height;
            DB.Left = pairPoint.X;

            AD.Top = pairPoint.Y;
            AD.Left = AC.Left;

            CB.Top = pairPoint.Y;
            CB.Left = AC.Left + AC.Width;

            this.Parent.Controls.Add(AC);
            this.Parent.Controls.Add(DB);
            this.Parent.Controls.Add(AD);
            this.Parent.Controls.Add(CB);
        }

        #region Events

        private void HuckControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top += e.Y - this.Height / 2;
                this.Left += e.X - this.Width / 2;
            }
        }

        #endregion
    }
}
