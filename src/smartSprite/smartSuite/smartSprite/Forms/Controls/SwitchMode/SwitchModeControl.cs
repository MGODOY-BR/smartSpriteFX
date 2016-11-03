using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace smartSprite.Forms.Controls.SwitchMode
{
    public partial class SwitchModeControl : UserControl
    {
        public SwitchModeControl()
        {
            InitializeComponent();
        }

        private void SwitchModeControl_Load(object sender, EventArgs e)
        {
            var myFileVersionInfo =
                FileVersionInfo.GetVersionInfo(
                    this.GetType().Assembly.Location);
                        this.lblVersion.Text = myFileVersionInfo.FileVersion + "(Alpha)";
        }
    }
}
