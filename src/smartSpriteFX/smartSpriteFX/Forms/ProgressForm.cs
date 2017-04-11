using smartSuite.smartSpriteFX.SpriteEffectModule.Infra.UI;
using smartSuite.smartSpriteFX.Effects.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Forms.Utilities;

namespace smartSuite.smartSpriteFX.Forms
{
    public partial class ProgressForm : Form, IApplyFilterCallback
    {
        /// <summary>
        /// It´s a conclusion message that must be shown after the processing
        /// </summary>
        private IConclusionMessage _conclusionMessage;

        public ProgressForm()
        {
            InitializeComponent();

            this._conclusionMessage = new EffectDoneDefaultConclusionMessage(this);
        }

        public ProgressForm(IConclusionMessage conclusionMessage)
        {
            InitializeComponent();

            this._conclusionMessage = conclusionMessage;
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

        public void ApplyFilter(Picture frame)
        {
            this.backgroundWorker1.RunWorkerAsync(frame);
            this.ShowUpdateProgress();
        }

        public void ApplyFilter(List<Picture> pictureList)
        {
            this.backgroundWorker1.RunWorkerAsync(pictureList);
            this.ShowUpdateProgress();
        }

        public void UpdateProgress(float percentage, bool completed)
        {
            backgroundWorker1.ReportProgress((int)percentage);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Cancel the filter applying", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
            EffectEngine.CancelApplying();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Picture selectedFrame = e.Argument as Picture;
            List<Picture> pictureList = e.Argument as List<Picture>;

            if (selectedFrame != null && pictureList == null)
            {
                EffectEngine.Apply(this, selectedFrame);
            }
            else if (selectedFrame == null && pictureList != null)
            {
                EffectEngine.Apply(this, pictureList.ToArray());
            }
            else
            {
                EffectEngine.Apply(this);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this._conclusionMessage.Show(null);
        }
    }
}
