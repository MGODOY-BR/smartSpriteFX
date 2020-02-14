using smartSuite.smartSpriteFX.Forms.Controls.SwitchMode;
using smartSuite.smartSpriteFX.Properties;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Forms
{
    /// <summary>
    /// It´s a screen form used to alternate among the available edit modes
    /// </summary>
    public partial class SelectModeScreenForm : Form
    {
        /// <summary>
        /// It´s the current mode.
        /// </summary>
        private static Form _currentMode;

        public SelectModeScreenForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks if the flow can resume
        /// </summary>
        /// <returns></returns>
        private bool CheckCanProceed<T>() where T : Form
        {
            if (_currentMode == null)
            {
                return true;
            }

            if (_currentMode.GetType() != typeof(T))
            {
                if (MessageBoxUtil.Show("Would you like to close the current edit mode?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _currentMode.Close();
                    return true;
                }
                else
                {
                    _currentMode.Focus();
                    return false;
                }
            }
            _currentMode.Focus();

            return false;
        }

        private void SelectModeScreenForm_Load(object sender, EventArgs e)
        {
            var switchModeControl = new SwitchModeControl();
            this.panel4.Controls.Add(switchModeControl);
        }

        private void _currentMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            SelectModeScreenForm._currentMode = null;
        }

        private void SelectModeScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxUtil.Show("Would you like to exit?", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
                if (SelectModeScreenForm._currentMode != null)
                {
                    SelectModeScreenForm._currentMode.Focus();
                }
            }
        }

        private void btnLandscapeMode_Click(object sender, EventArgs e)
        {
            if (!this.CheckCanProceed<LandscapeModeForm>())
            {
                return;
            }

            _currentMode = new LandscapeModeForm();
            _currentMode.FormClosed += _currentMode_FormClosed;
            _currentMode.Show();
        }

        private void btnEffectMode_Click(object sender, EventArgs e)
        {
            if (!this.CheckCanProceed<EffectModeForm>())
            {
                return;
            }

            _currentMode = new EffectModeForm();
            _currentMode.FormClosed += _currentMode_FormClosed;
            _currentMode.Show();
        }

        private void btnEffectModeBatch_Click(object sender, EventArgs e)
        {
            if (!this.CheckCanProceed<EffectModeBatchForm>())
            {
                return;
            }

            _currentMode = new EffectModeBatchForm();
            _currentMode.FormClosed += _currentMode_FormClosed;
            _currentMode.Show();
        }

        private void btnLandscapeMode_MouseMove(object sender, MouseEventArgs e)
        {
            this.ShowDemoPicture(
                Resources.landscapemode, 
                "Cut pieces of images to use them like independent images. Images inside of another images can have their background inferred to transparent automatically");
        }

        private void btnEffectMode_MouseMove(object sender, MouseEventArgs e)
        {
            this.ShowDemoPicture(
                Resources.effectmode,
                "Apply filters to get different and interesting effects to one or more images");
        }

        private void btnEffectModeBatch_MouseMove(object sender, MouseEventArgs e)
        {
            this.ShowDemoPicture(
                Resources.effectbatch,
                "Use filter sets of effect mode to apply on all whole tree of directories");
        }

        /// <summary>
        /// Shows the demo picture
        /// </summary>
        /// <param name="explanationPicture">An explanation picture</param>
        /// <param name="explanationText">An explanation text</param>
        private void ShowDemoPicture(Bitmap explanationPicture, string explanationText)
        {
            #region Entries validation

            if (explanationPicture == null)
            {
                throw new ArgumentNullException("explanationPicture");
            }
            if (String.IsNullOrEmpty(explanationText))
            {
                throw new ArgumentNullException("explanationText");
            }

            #endregion

            this.imgDemo.BackgroundImage = explanationPicture;
            this.lblDescription.Text = explanationText;
            this.lblDescription.Visible = true;
        }

        private void btnLettlerModeBatch_Click(object sender, EventArgs e)
        {
            if (!this.CheckCanProceed<ImageLetterModeForm>())
            {
                return;
            }

            _currentMode = new ImageLetterModeForm();
            _currentMode.FormClosed += _currentMode_FormClosed;
            _currentMode.Show();
        }

        private void btnLettlerModeBatch_MouseMove(object sender, MouseEventArgs e)
        {
            this.ShowDemoPicture(
                Resources.lettlerMode,
                "Generates sets of characters in image galleries");
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            ToolsForm form = new ToolsForm();
            form.ShowDialog();
        }

        private void btnWatchAnimation_Click(object sender, EventArgs e)
        {
            BrowseFolder browseFolder = new BrowseFolder();
            browseFolder.ShowDialog();

            this.Close();
        }
    }
}
