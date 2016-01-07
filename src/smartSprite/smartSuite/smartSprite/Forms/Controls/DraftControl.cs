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
        /// It´s a collection of main (older) hooks.
        /// </summary>
        private List<HookControl> _hookSet = new List<HookControl>();

        /// <summary>
        /// Gets or sets the last hook added
        /// </summary>
        private HookControl _lastHook;

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
            if (this._lastSettings != null && this._lastSettings.HookOn)
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

            if (this._lastSettings.HookOn)
            {
                AddNewHook(e);
            }
        }

        /// <summary>
        /// Adds a new hook
        /// </summary>
        /// <param name="e"></param>
        private void AddNewHook(MouseEventArgs e)
        {
            HookControl newHook = new HookControl();
            newHook.Deleting += NewHook_Deleting;

            newHook.Top = e.Y - newHook.Height / 2;
            newHook.Left = e.X - newHook.Width / 2;
            newHook.Pair = this._lastHook;

            if (this._lastHook != null)
            {
                this._lastHook.Pair = newHook;
                this._lastHook = null;
            }

            this._lastHook = newHook;
            this.imgDraft.Controls.Add(newHook);
            if (newHook.Pair != null)
            {
                newHook.CreateLines();
                this._hookSet.Add(newHook.Pair);
                this._lastHook = null;
            }
        }

        private void NewHook_Deleting(object sender, HookState.HookEventArgs e)
        {
            e.MainHook.DestroyYourSelf();
        }

        private void imgDraft_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursorStyle();
        }

        #endregion
    }
}
