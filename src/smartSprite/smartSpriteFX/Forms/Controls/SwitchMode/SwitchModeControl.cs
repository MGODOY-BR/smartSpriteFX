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

namespace smartSpriteFX.Forms.Controls.SwitchMode
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

        private void btnSwitchModel_Click(object sender, EventArgs e)
        {
            Form selectModeScreenForm = null;
            foreach (Form formItem in Application.OpenForms)
            {
                if (formItem is SelectModeScreenForm)
                {
                    selectModeScreenForm = formItem;
                    break;
                }
            }

            if (selectModeScreenForm == null)
            {
                selectModeScreenForm = new SelectModeScreenForm();
                selectModeScreenForm.Show();
            }

            selectModeScreenForm.Focus();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBoxForm form = new AboutBoxForm();
            form.ShowDialog();
        }
    }
}
