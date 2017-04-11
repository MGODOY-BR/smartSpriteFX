using smartSuite.smartSpriteFX.Animations;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Forms.Controls.Browsers;
using smartSuite.smartSpriteFX.Forms.Controls.SwitchMode;
using smartSuite.smartSpriteFX.Forms.Utilities;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Forms
{
    public partial class EffectModeBatchForm : Form
    {
        /// <summary>
        /// It is the origin folder
        /// </summary>
        private string _originFolder;
        /// <summary>
        /// It is the filter set path
        /// </summary>
        private string _filterSetPath;

        public EffectModeBatchForm()
        {
            InitializeComponent();
        }

        private void EffectModeBatchForm_Load(object sender, EventArgs e)
        {
            SmartBrowser originBrowser = new SmartBrowser();
            originBrowser.Dock = DockStyle.Fill;
            originBrowser.BrowserType = SmartBrowserTypeEnum.Folder;
            originBrowser.DialogTitle = "Select the parent folder";
            originBrowser.ChosenByUserEvent += OriginBrowser_ChosenByUserEvent;
            originBrowser.HideApplyButton = true;
            this.grpOriginFolder.Controls.Add(originBrowser);

            SmartBrowser filterSetBrowser = new SmartBrowser();
            filterSetBrowser.Dock = DockStyle.Fill;
            filterSetBrowser.BrowserType = SmartBrowserTypeEnum.File;
            filterSetBrowser.DialogTitle = "Select a filter set file";
            filterSetBrowser.DialogFilter = "Filter set|*filterSet";
            filterSetBrowser.ChosenByUserEvent += FilterSetBrowser_ChosenByUserEvent;
            filterSetBrowser.HideApplyButton = true;
            this.grpFilterSet.Controls.Add(filterSetBrowser);

            var switchModeControl = new SwitchModeControl();
            switchModeControl.Dock = DockStyle.Right;
            this.pictureBox1.Controls.Add(switchModeControl);
        }

        private void FilterSetBrowser_ChosenByUserEvent(object sender, SmartBrowserEventArgs e)
        {
            this._filterSetPath = e.UserChoice;
        }

        private void OriginBrowser_ChosenByUserEvent(object sender, SmartBrowserEventArgs e)
        {
            this._originFolder = e.UserChoice;
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            List<Picture> pictureList = new List<Picture>();

            EffectEngine.Initializate();
            EffectEngine.LoadFilterSet(this._filterSetPath);

            // Getting the pictures
            foreach (var extension in FrameIterator.GetSupportedImages())
            {
                var fileNameList =
                    Directory.GetFiles(this._originFolder, extension, SearchOption.AllDirectories);

                foreach (var fileNameItem in fileNameList)
                {
                    #region Entries validation

                    if(Path.GetDirectoryName(fileNameItem).EndsWith("filtered"))
                    {
                        continue;
                    }

                    #endregion

                    pictureList.Add(
                        Picture.GetInstanceLazy(fileNameItem));
                }
            }

            var conclusionMessage = new EffectBatchDoneDefaultConclusionMessage("Applying has concluded!");
            conclusionMessage.Parent = new ProgressForm(conclusionMessage);

            EffectEngine.ApplyFromUI(
                (ProgressForm)conclusionMessage.Parent, 
                pictureList);
        }
    }
}
