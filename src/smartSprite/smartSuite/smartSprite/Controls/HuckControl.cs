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
        public HuckControl()
        {
            InitializeComponent();

            this.MouseMove += HuckControl_MouseMove;
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
