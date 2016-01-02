using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSprite.Controls.ToolboxState;

namespace smartSprite.Controls
{
    public partial class DraftControl : UserControl
    {
        public DraftControl()
        {
            InitializeComponent();
        }

        #region Delegates

        /// <summary>
        /// Occurs when the control needs to consults the choices of another control by the user
        /// </summary>
        /// <returns></returns>
        public event EventHandler<DraftSettings> GettingSettings;

        /// <summary>
        /// Gets the settings to draft control, specified for the parent controler
        /// </summary>
        /// <returns></returns>
        private DraftSettings OnGettingSettings()
        {
            if (this.GettingSettings == null)
            {
                throw new NotImplementedException("GettingSettings event is missed.");
            }

            DraftSettings draftSettings = new DraftSettings();
            this.GettingSettings(this, draftSettings);

            return draftSettings;
        }

        #endregion

        #region Events

        private void imgDraft_MouseClick(object sender, MouseEventArgs e)
        {
            var settings = this.OnGettingSettings();

            if (settings.HuckOn)
            {
                HuckControl huckControl = new HuckControl();

                huckControl.Top = e.Y - huckControl.Height / 2;
                huckControl.Left = e.X - huckControl.Width / 2;

                this.imgDraft.Controls.Add(huckControl);
            }
        }

        #endregion
    }
}
