using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSprite.Forms.Controls.ToolboxState;

namespace smartSprite.Forms.Controls
{
    public partial class DraftControl : UserControl
    {
        /// <summary>
        /// Gets or sets the last huck added
        /// </summary>
        private HuckControl _lastHuck;

        /// <summary>
        /// It is the last settings
        /// </summary>
        private DraftSettings _lastSettings;

        /// <summary>
        /// Gets or sets the last settings
        /// </summary>
        public DraftSettings LastSettings
        {
            get
            {
                return _lastSettings;
            }

            set
            {
                this._lastSettings = value;
            }
        }

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

        /// <summary>
        /// Updates the cursor style
        /// </summary>
        private void UpdateCursorStyle()
        {
            if (this._lastSettings != null && this._lastSettings.HuckOn)
            {
                imgDraft.Cursor = Cursors.Cross;
            }
            else
            {
                imgDraft.Cursor = Cursors.Default;
            }
        }

        #region Events

        private void imgDraft_MouseClick(object sender, MouseEventArgs e)
        {
            this._lastSettings = this.OnGettingSettings();

            if (this._lastSettings.HuckOn)
            {
                AddNewHuck(e);
            }
        }

        /// <summary>
        /// Adds a new huck
        /// </summary>
        /// <param name="e"></param>
        private void AddNewHuck(MouseEventArgs e)
        {
            HuckControl newHuck = new HuckControl();

            newHuck.Top = e.Y - newHuck.Height / 2;
            newHuck.Left = e.X - newHuck.Width / 2;
            newHuck.Pair = this._lastHuck;

            if (this._lastHuck != null)
            {
                this._lastHuck.Pair = newHuck;
                this._lastHuck = null;
            }

            this._lastHuck = newHuck;
            this.imgDraft.Controls.Add(newHuck);
            if (newHuck.Pair != null)
            {
                newHuck.CreateLines();
                this._lastHuck = null;
            }
        }

        private void imgDraft_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursorStyle();
        }

        #endregion
    }
}
