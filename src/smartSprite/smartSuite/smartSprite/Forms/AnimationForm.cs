using smartSprite.Forms.Controls.Animations.Effects;
using smartSprite.Forms.Controls.Animations.Frames;
using smartSprite.Forms.Controls.Browsers;
using smartSprite.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Effects.Infra;

namespace smartSprite.Forms
{
    public partial class AnimationForm : Form
    {
        /// <summary>
        /// Controls the sort of effect control.
        /// </summary>
        private EffectControlOrderCollection _effectControlOrderCollection = new EffectControlOrderCollection();

        public AnimationForm()
        {
            InitializeComponent();

            // Animation browser
            SmartBrowser smartBrowser = new SmartBrowser();
            smartBrowser.BrowserType = SmartBrowserTypeEnum.Folder;
            smartBrowser.DialogTitle = "Open a animation folder";
            smartBrowser.FrameTitle = "Animation folder";
            smartBrowser.Dock = DockStyle.Top;
            if (!String.IsNullOrEmpty(Settings.Default.lastAnimationFolder))
            {
                smartBrowser.LoadLastUserChoice(Settings.Default.lastAnimationFolder);
            }
            smartBrowser.ChosenByUserEvent += SmartBrowser_ChosenByUserEvent;
            this.panelBrowser.Controls.Add(smartBrowser);
        }

        /// <summary>
        /// It´s occurs when the user selects the animation folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmartBrowser_ChosenByUserEvent(object sender, SmartBrowserEventArgs e)
        {
            // FrameSelectionControl
            FrameSelectionControl frameSelectionControl = new FrameSelectionControl();
            frameSelectionControl.Dock = DockStyle.Fill;
            frameSelectionControl.SetPath(e.UserChoice);
            frameSelectionControl.SelectingFrame += FrameSelectionControl_SelectingFrame;
            frameSelectionControl.LoadThumbNails();
            this.frameBox.Controls.Add(frameSelectionControl);

            Settings.Default.lastAnimationFolder = e.UserChoice;
            Settings.Default.Save();

            // Showing pallete tool window
            EffectFilterPalleteForm effectFilterPalleteForm = new EffectFilterPalleteForm();
            effectFilterPalleteForm.Show();
            effectFilterPalleteForm.SelectedFilterEvent += EffectFilterPalleteForm_SelectedFilterEvent;
        }

        /// <summary>
        /// Occurs when the user selects a filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffectFilterPalleteForm_SelectedFilterEvent(object sender, EffectFilterPalleteForm.SelectionFilterEventArgs e)
        {
            EffectControl effectControl = new EffectControl();
            effectControl.SetFilter(e.Filter);
            effectControl.Dock = DockStyle.Top;
            effectControl.UserInteracted += EffectControl_UserInteracted;

            _effectControlOrderCollection.Register(effectControl);
            this.EffectBind(_effectControlOrderCollection);
        }

        /// <summary>
        /// Refresh the effect list
        /// </summary>
        private void EffectBind(EffectControlOrderCollection effectControlOrderCollection)
        {
            var effectList = effectControlOrderCollection.GetSortedList();

            this.pnlFilterPanel.Controls.Clear();
            foreach (var item in effectControlOrderCollection.GetSortedList())
            {
                this.pnlFilterPanel.Controls.Add(item);
            }
        }

        /// <summary>
        /// Occurs when user interacts with EffectControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffectControl_UserInteracted(object sender, EffectControl.EffectControlEventArgs e)
        {
            txtSettingsDescription.Clear();
            switch (e.CommandType)
            {
                case EffectControl.EffectControlCommandEnum.EXCLUDE:
                    this._effectControlOrderCollection.Remove(e.Control);
                    break;

                case EffectControl.EffectControlCommandEnum.UP:
                    this._effectControlOrderCollection.Up(e.Control);
                    break;

                case EffectControl.EffectControlCommandEnum.DOW:
                    this._effectControlOrderCollection.Down(e.Control);
                    break;

                case EffectControl.EffectControlCommandEnum.SETTINGS:
                    this.ShowSettings(e.Control.GetFilter());
                    break;

                default:
                    throw new NotSupportedException(e.CommandType.ToString());
            }

            this.EffectBind(this._effectControlOrderCollection);
        }

        /// <summary>
        /// Shows the Setting panel
        /// </summary>
        /// <param name="effectFilter"></param>
        private void ShowSettings(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }

            #endregion

            Identification identification = null;

            try
            {
                identification = 
                    effectFilter.GetIdentification();
            }
            catch
            {
                // Errors in this part doesn't interrupt the flow
            }

            if (identification != null)
            {
                try
                {
                    txtSettingsDescription.Text =
                        String.Format(
                            "Filter: {0}\r\n\r\nAuthor: {1}\r\n\r\nDescription: {2}",
                            identification.GetName(),
                            identification.GetAuthor(),
                            identification.GetDescription());
                }
                catch (Exception ex)
                {
                    txtSettingsDescription.Text = "Ops! The effect panel returns an error! - " + ex.ToString();
                }
            }

            try
            {
                var configurationPanel = effectFilter.ShowConfigurationPanel();
                pnlSettingsMain.Controls.Clear();
                if (configurationPanel != null)
                {
                    pnlSettingsMain.Controls.Add(
                        configurationPanel.GetPanel(effectFilter));
                }
            }
            catch (Exception ex)
            {
                txtSettingsDescription.Text = "Ops! The effect panel returns an error! - " + ex.ToString();
            }
            
        }

        /// <summary>
        /// It´s occurs when the frame is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameSelectionControl_SelectingFrame(object sender, FrameSelectionEventArgs e)
        {
            previewBoard.Image = Image.FromFile(e.FilePath);
        }

        /// <summary>
        /// Controls the order of effect control
        /// </summary>
        private class EffectControlOrderCollection
        {
            /// <summary>
            /// Relates the effectControl and order index
            /// </summary>
            private Dictionary<EffectControl, int> _effectControlCacheList = new Dictionary<EffectControl, int>();

            /// <summary>
            /// Registers the control
            /// </summary>
            /// <param name="control"></param>
            public void Register(EffectControl control)
            {
                this._effectControlCacheList.Add(control, _effectControlCacheList.Count);
            }

            /// <summary>
            /// Ups the control order
            /// </summary>
            /// <param name="control"></param>
            public void Up(EffectControl control)
            {
                int index = this._effectControlCacheList[control];

                #region Entries validation

                if (index == this._effectControlCacheList.Count - 1)
                {
                    return;
                }

                #endregion

                this._effectControlCacheList[control]++;

                var keyList = this._effectControlCacheList.Keys.ToArray();
                for (int i = 0; i < keyList.Length; i++)
                {
                    var controlItem = keyList[i];
                    if (control == controlItem)
                    {
                        continue;
                    }

                    if (this._effectControlCacheList[controlItem] == this._effectControlCacheList[control])
                    {
                        this._effectControlCacheList[controlItem] = index;
                    }
                }
            }

            /// <summary>
            /// Downs the control order
            /// </summary>
            /// <param name="control"></param>
            public void Down(EffectControl control)
            {
                int index = this._effectControlCacheList[control];

                #region Entries validation

                if (index == 0)
                {
                    return;
                }

                #endregion

                this._effectControlCacheList[control]--;

                var keyList = this._effectControlCacheList.Keys.ToArray();
                for (int i = 0; i < keyList.Length; i++)
                {
                    var controlItem = keyList[i];
                    if (control == controlItem)
                    {
                        continue;
                    }

                    if (this._effectControlCacheList[controlItem] == this._effectControlCacheList[control])
                    {
                        this._effectControlCacheList[controlItem] = index;
                    }
                }
            }

            /// <summary>
            /// Gets the sorted list
            /// </summary>
            /// <returns></returns>
            public List<EffectControl> GetSortedList()
            {
                var sortedList = from item in this._effectControlCacheList
                                 orderby item.Value
                                 select item.Key;

                return new List<EffectControl>(sortedList);
            }

            /// <summary>
            /// Removes control
            /// </summary>
            /// <param name="effectControl"></param>
            public void Remove(EffectControl effectControl)
            {
                #region Entries validation

                if (effectControl == null)
                {
                    throw new ArgumentNullException("effectControl");
                }

                #endregion

                this._effectControlCacheList.Remove(effectControl);

                int index = 0;
                foreach (var item in this.GetSortedList())
                {
                    this._effectControlCacheList[item] = index;
                    index++;
                }
            }
        }
    }
}
