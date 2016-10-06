using smartSprite.Forms.Controls.Animations.Frames;
using smartSprite.Forms.Controls.Browsers;
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
    public partial class AnimationForm : Form
    {
        public AnimationForm()
        {
            InitializeComponent();

            // Animation browser
            SmartBrowser smartBrowser = new SmartBrowser();
            smartBrowser.BrowserType = SmartBrowserTypeEnum.Folder;
            smartBrowser.DialogTitle = "Open a animation folder";
            smartBrowser.FrameTitle = "Animation folder";
            smartBrowser.Dock = DockStyle.Top;
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
            FrameSelectionControl frameSelectionControl = new FrameSelectionControl();
            frameSelectionControl.Dock = DockStyle.Fill;
            frameSelectionControl.SetPath(e.UserChoice);
            frameSelectionControl.SelectingFrame += FrameSelectionControl_SelectingFrame;
            frameSelectionControl.LoadThumbNails();

            this.frameBox.Controls.Add(frameSelectionControl);
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
    }
}
