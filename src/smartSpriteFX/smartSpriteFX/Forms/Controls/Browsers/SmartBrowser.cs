using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Forms.Controls.Browsers
{
    /// <summary>
    /// Represents a default browser
    /// </summary>
    public partial class SmartBrowser : UserControl
    {
        public SmartBrowser()
        {
            InitializeComponent();

            this.Load += SmartBrowser_Load;
        }

        #region Events

        /// <summary>
        /// Occurs when the user concludes the choice
        /// </summary>
        public event EventHandler<SmartBrowserEventArgs> ChosenByUserEvent;

        #endregion

        /// <summary>
        /// It´s the kind of browse
        /// </summary>
        public SmartBrowserTypeEnum BrowserType { get; set; }

        /// <summary>
        /// It´s the frame title
        /// </summary>
        public String FrameTitle { get; set; }

        /// <summary>
        /// It´s the title of dialog
        /// </summary>
        public String DialogTitle { get; set; }

        /// <summary>
        /// It´s the filter of files to be shown.
        /// </summary>
        public String DialogFilter { get; set; }

        /// <summary>
        /// It´s the choice of user (file or folder)
        /// </summary>
        public String UserChoice { get; set; }

        /// <summary>
        /// Indicates that apply button must be hidden
        /// </summary>
        public bool HideApplyButton
        {
            get;
            set;
        }

        /// <summary>
        /// Loads the last user choice
        /// </summary>
        /// <param name="userChoice"></param>
        public void LoadLastUserChoice(String userChoice)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(userChoice))
            {
                throw new ArgumentNullException("userChoice");
            }

            #endregion

            this.UserChoice = userChoice;
            this.openFileDialog1.FileName = userChoice;
            this.folderBrowserDialog1.SelectedPath = userChoice;
            this.txtFileName.Text = userChoice;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            #region Entries validation

            if (this.ChosenByUserEvent == null)
            {
                throw new NotImplementedException("ChosenByUserEvent");
            }

            #endregion

            this.DoChoice(openFileDialog1.FileName);
        }

        /// <summary>
        /// Selects the option
        /// </summary>
        /// <param name="fileName"></param>
        private void DoChoice(string fileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            #endregion

            String previousValue = this.txtFileName.Text;
            this.txtFileName.Text = fileName;
            this.UserChoice = fileName;
            try
            {
                if (this.ChosenByUserEvent != null)
                {
                    this.ChosenByUserEvent(
                        this,
                        new SmartBrowserEventArgs
                        {
                            UserChoice = this.UserChoice
                        });
                }
            }
            catch
            {
                // Undo the text
                this.txtFileName.Text = previousValue;
                throw;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            switch (this.BrowserType)
            {
                case SmartBrowserTypeEnum.File:
                    this.openFileDialog1.Title = this.DialogTitle;
                    this.openFileDialog1.Filter = this.DialogFilter;
                    this.openFileDialog1.ShowDialog();
                    break;

                case SmartBrowserTypeEnum.Folder:
                    this.folderBrowserDialog1.Description = this.DialogTitle;
                    if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        this.DoChoice(this.folderBrowserDialog1.SelectedPath);
                    }
                    break;

                default:
                    throw new NotSupportedException(this.BrowserType.ToString());
            }
        }

        private void SmartBrowser_Load(object sender, EventArgs e)
        {
            Reload();
        }

        /// <summary>
        /// Updates the controls
        /// </summary>
        public void Reload()
        {
            this.groupBox1.Text = this.FrameTitle;
            this.openFileDialog1.Filter = this.DialogFilter;

            this.txtFileName.Text = this.UserChoice;
            this.btnApply.Visible = !this.HideApplyButton;
        }

        /// <summary>
        /// Clears the textbox
        /// </summary>
        public void ClearText()
        {
            this.txtFileName.Text = "";
            this.UserChoice = "";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DoChoice(this.txtFileName.Text);
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            this.UserChoice = this.txtFileName.Text;
        }
    }

    /// <summary>
    /// Relates the type of smart browser instance
    /// </summary>
    public enum SmartBrowserTypeEnum
    {
        File,
        Folder
    }
}
