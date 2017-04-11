using smartSuite.smartSpriteFX.Forms.Controls.SwitchMode;
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
            this.Close();
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

        private void btnEffectModeBatch_Click(object sender, EventArgs e)
        {
            EffectModeBatchForm form = new EffectModeBatchForm();
            form.ShowDialog();
        }
    }
}
