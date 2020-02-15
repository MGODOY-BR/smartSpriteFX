using smartSuite.smartSpriteFX.Forms.Controls.Browsers;
using smartSuite.smartSpriteFX.Properties;
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
    public partial class BrowseFolder : Form
    {
        public BrowseFolder()
        {
            InitializeComponent();

            SmartBrowser browser = new SmartBrowser();
            this.Controls.Add(browser);

            browser.BrowserType = SmartBrowserTypeEnum.Folder;
            browser.ChosenByUserEvent += Browser_ChosenByUserEvent;
            browser.LoadLastUserChoice(Settings.Default.lastAnimationFolder);
        }

        private void Browser_ChosenByUserEvent(object sender, SmartBrowserEventArgs e)
        {
            Settings.Default.lastAnimationFolder = e.UserChoice;
            Settings.Default.Save();

            WatchAnimation watchAnimation = new WatchAnimation(e.UserChoice);
            watchAnimation.ShowDialog();
        }
    }
}
