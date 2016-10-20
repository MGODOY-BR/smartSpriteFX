using smartSprite.SpriteEffectModule.Infra.UI;
using smartSuite.smartSprite.Effects.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSprite.Forms
{
    public partial class ProgressForm : Form, IApplyFilterCallback
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void ShowUpdateProgress()
        {
            this.Show();
        }

        public void UpdateProgress(float percentage, bool completed)
        {
            backgroundWorker1.ReportProgress((int)percentage);

            if (completed)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EffectEngine.CancelApplying();
            this.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }
    }
}
