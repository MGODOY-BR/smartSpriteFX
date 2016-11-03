using smartSprite.SpriteEffectModule.Effects.Filters.UI;
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
            if (!this.CheckCanProceed<LandscapeForm>())
            {
                return;
            }

            _currentMode = new LandscapeForm();
            _currentMode.Show();
        }

        private void btnEffectMode_Click(object sender, EventArgs e)
        {
            if (!this.CheckCanProceed<AnimationForm>())
            {
                return;
            }

            _currentMode = new AnimationForm();
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
            }
            this.Close();
            _currentMode.Focus();

            return false;
        }

    }
}
