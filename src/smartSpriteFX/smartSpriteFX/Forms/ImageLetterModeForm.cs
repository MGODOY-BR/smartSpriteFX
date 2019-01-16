using smartSuite.smartSpriteFX.Forms.Controls.Browsers;
using smartSuite.smartSpriteFX.Forms.Controls.SwitchMode;
using smartSuite.smartSpriteFX.Forms.Utilities.CharacterSet;
using smartSuite.smartSpriteFX.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace smartSuite.smartSpriteFX.Forms
{
    public partial class ImageLetterModeForm : Form
    {
        /// <summary>
        /// Represents an smartBrowser control
        /// </summary>
        private SmartBrowser smartBrowser1 = new SmartBrowser();

        public ImageLetterModeForm()
        {
            InitializeComponent();

            this.smartBrowser1.HideApplyButton = true;
            this.smartBrowser1.Dock = DockStyle.Fill;
            this.smartBrowser1.BrowserType = SmartBrowserTypeEnum.Folder;
            this.pnlDestinationFolder.Controls.Add(this.smartBrowser1);
        }

        private void ImageLetterModeForm_Load(object sender, EventArgs e)
        {

            var switchModeControl = new SwitchModeControl();
            switchModeControl.Dock = DockStyle.Right;
            this.pictureBox1.Controls.Add(switchModeControl);

            //grpSelectedFont.Text = this.lblSample.Font.ToString();
            this.FillCharacterSetList();
        }

        /// <summary>
        /// Fills the character set list
        /// </summary>
        private void FillCharacterSetList()
        {
            var list = CharacterSet.GetList();
            foreach (var listItem in list)
            {
                this.characterSetList.Items.Add(listItem);
            }
        }

        private void btnOpenFontDialog_Click(object sender, EventArgs e)
        {
            this.fontDialog1.Font = this.lblSample.Font;
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblSample.Font = this.fontDialog1.Font;
                this.lblSample.ForeColor = this.fontDialog1.Color;
                //grpSelectedFont.Text = this.lblSample.Font.ToString();
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.pnlContainer.Enabled = false;
            CheckedItemCollection checkedList = this.characterSetList.CheckedItems;

            List<ICharacterSet> items = new List<ICharacterSet>();
            foreach (var checkedItem in checkedList)
            {
                items.Add((ICharacterSet)checkedItem);
            }

            foreach (var file in Directory.EnumerateFiles(this.smartBrowser1.UserChoice))
            {
                File.Delete(file);
            }

            this.backgroundWorker1.RunWorkerAsync(
                new object[4] 
                {
                    items,
                    this.smartBrowser1.UserChoice,
                    this.lblSample.Font,
                    this.lblSample.ForeColor
                });
        }

        /// <summary>
        /// Generates the images
        /// </summary>
        private void Generate(List<ICharacterSet> characterSetList, string folderPath, Font font, Color backcolor, Color forecolor)
        {
            #region Entries validation

            if (characterSetList == null)
            {
                throw new ArgumentNullException("characterSetList");
            }
            if(characterSetList.Count == 0)
            {
                throw new ApplicationException("Please, select the character set");
            }
            if (String.IsNullOrEmpty(folderPath))
            {
                throw new ApplicationException("Please, select the folder to generate images");
            }

            #endregion

            foreach (var characterSetItem in characterSetList)
            {
                #region Path management

                string path = // Path.Combine(folderPath, characterSetItem.GetShortName());
                    folderPath;

                //if (Directory.Exists(path))
                //{
                //    Directory.Delete(path, true);
                //}

                Directory.CreateDirectory(path);

                #endregion

                var minRange = int.Parse(characterSetItem.GetMinRange(), System.Globalization.NumberStyles.HexNumber);
                var maxRange = int.Parse(characterSetItem.GetMaxRange(), System.Globalization.NumberStyles.HexNumber);

                SolidBrush brush = new SolidBrush(forecolor);

                for (long i = minRange; i < maxRange; i++)
                {
                    var hex = Convert.ToString(i, 16).PadLeft(4, '0');
                    var unicodeChar = System.Convert.ToChar(System.Convert.ToUInt32(hex, 16));

                    var size = System.Windows.Forms.TextRenderer.MeasureText(unicodeChar.ToString(), font);

                    string fileName = Path.Combine(path, i.ToString() + ".png");
                    using (Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb))
                    {
                        RectangleF rectf = new RectangleF(0, 0, size.Width, size.Height);

                        Graphics graphics = Graphics.FromImage(bmp);

                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        if(backcolor != Color.Transparent)
                        {
                            graphics.FillRectangle(
                                new SolidBrush(backcolor),
                                rectf);
                        }
                        graphics.DrawString(unicodeChar.ToString(), font, brush, rectf);

                        // graphics.Flush();
                        bmp.Save(fileName, ImageFormat.Png);
                    }
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] argumentArray = (object[])e.Argument;

            List<ICharacterSet> items = (List<ICharacterSet>)argumentArray[0];
            string folderPath = (string)argumentArray[1];
            Font font = (Font)argumentArray[2];
            Color forecolor = (Color)argumentArray[3];

            this.Generate(items, this.smartBrowser1.UserChoice, this.lblSample.Font, this.previewFont.BackColor, this.lblSample.ForeColor);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.pnlContainer.Enabled = true;
            if (e.Error == null)
            {
                PathAlertForm alertBox = new PathAlertForm();
                alertBox.Open("The characters has been generated successfully!", this.smartBrowser1.UserChoice);
            }
            else
            {
                throw e.Error;
            }
        }

        private void previewFont_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.backgroundColorDialog.Color = this.previewFont.BackColor;
            if (this.backgroundColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.previewFont.BackColor = this.backgroundColorDialog.Color;
            }
            if(previewFont.BackColor != Color.Transparent)
            {
                this.previewFont.BackgroundImage = null;
            }
            else
            {
                this.previewFont.BackgroundImage = Resources.background;
            }
        }
    }
}
