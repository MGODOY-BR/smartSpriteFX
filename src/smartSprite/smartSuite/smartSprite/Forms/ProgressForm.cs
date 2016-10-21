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
            this.ShowDialog();
        }

        public void ApplyFilter()
        {
            this.backgroundWorker1.RunWorkerAsync();
            this.ShowUpdateProgress();
        }

        public void UpdateProgress(float percentage, bool completed)
        {
            backgroundWorker1.ReportProgress((int)percentage);
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            EffectEngine.Apply(this);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string path = EffectEngine.GetOutputPath();
            PathAlertForm pathAlertForm = new PathAlertForm();
            pathAlertForm.Open("Concluded!", path);
            this.Close();
        }
    }
}
