using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSprite.SpriteEffectModule.Effects.Filters.UI
{
    public partial class CustomResolutionControl : UserControl
    {
        public CustomResolutionControl()
        {
            InitializeComponent();

            txtMaxColorAmount.KeyPress += JustNumberEvent;
            txtMaxScreenWidth.KeyPress += JustNumberEvent;
            txtMaxScreenHeight.KeyPress += JustNumberEvent;
            txtScreenWidth.KeyPress += JustNumberEvent;
            txtScreenHeight.KeyPress += JustNumberEvent;
        }

        /// <summary>
        /// Allows just number to be entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JustNumberEvent(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
