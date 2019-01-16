using smartSuite.smartSpriteFX.Forms.Controls.Browsers;
using smartSuite.smartSpriteFX.Forms.Utilities.SpriteSheet;
using smartSuite.smartSpriteFX.Properties;
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
    public partial class ToolsForm : Form
    {
        /// <summary>
        /// It's the browser of origin in join proccess
        /// </summary>
        private SmartBrowser _originSpriteSheetBrowser = new SmartBrowser();
        /// <summary>
        /// It's the browser of destination in join proccess
        /// </summary>
        private SmartBrowser _destinationSpriteSheetBrowser = new SmartBrowser();

        public ToolsForm()
        {
            InitializeComponent();
        }

        private void ToolsForm_Load(object sender, EventArgs e)
        {
            this._originSpriteSheetBrowser.UserChoice = Settings.Default.joinSpriteOriginFolder;
            this._destinationSpriteSheetBrowser.UserChoice = Settings.Default.joinSpriteDestinyFile;

            _originSpriteSheetBrowser.Dock = DockStyle.Fill;
            _destinationSpriteSheetBrowser.Dock = DockStyle.Fill;

            _originSpriteSheetBrowser.BrowserType = SmartBrowserTypeEnum.Folder;

            _originSpriteSheetBrowser.HideApplyButton = true;
            _destinationSpriteSheetBrowser.HideApplyButton = true;

            this.grpOriginFolderJoin.Controls.Add(_originSpriteSheetBrowser);
            this.grpDestinationJoin.Controls.Add(_destinationSpriteSheetBrowser);
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(this._originSpriteSheetBrowser.UserChoice))
            {
                throw new ArgumentNullException("this._originSpriteSheetBrowser.UserChoice");
            }
            if (String.IsNullOrEmpty(this._destinationSpriteSheetBrowser.UserChoice))
            {
                throw new ArgumentNullException("this._destinationSpriteSheetBrowser.UserChoice");
            }

            #endregion

            this.grpJoinSprites.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            Settings.Default.joinSpriteOriginFolder = this._originSpriteSheetBrowser.UserChoice;
            Settings.Default.joinSpriteDestinyFile = this._destinationSpriteSheetBrowser.UserChoice;
            Settings.Default.Save();

            joinSpritesBackgroundWorker.RunWorkerAsync(
                new string[2] 
                {
                    this._originSpriteSheetBrowser.UserChoice,
                    this._destinationSpriteSheetBrowser.UserChoice
                });
        }

        private void joinSpritesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var paramList = (string[])e.Argument;

            SpriteSheetMaker spriteSheetMaker = 
                new SpriteSheetMaker(
                    new SpriteFileListOrganizer(paramList[0]));

            spriteSheetMaker.Generate(paramList[1]);
        }

        private void joinSpritesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.grpJoinSprites.Enabled = true;
            this.Cursor = Cursors.Default;

            if (e.Error == null)
            {
                string path = Path.GetDirectoryName(this._destinationSpriteSheetBrowser.UserChoice);

                PathAlertForm alertBox = new PathAlertForm();
                alertBox.Open("SpriteSheet has been generate successfully!", path);
            }
            else
            {
                throw e.Error;
            }
        }
    }
}
