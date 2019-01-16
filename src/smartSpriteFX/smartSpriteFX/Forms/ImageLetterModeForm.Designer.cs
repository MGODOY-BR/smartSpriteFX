namespace smartSuite.smartSpriteFX.Forms
{
    partial class ImageLetterModeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.previewFont = new System.Windows.Forms.Panel();
            this.lblSample = new System.Windows.Forms.Label();
            this.btnOpenFontDialog = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.characterSetList = new System.Windows.Forms.CheckedListBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlDestinationFolder = new System.Windows.Forms.Panel();
            this.backgroundColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.previewFont.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::smartSuite.smartSpriteFX.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(642, 28);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.previewFont);
            this.groupBox1.Controls.Add(this.btnOpenFontDialog);
            this.groupBox1.Location = new System.Drawing.Point(6, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 118);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select your font";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Double click on background to change color -->";
            // 
            // previewFont
            // 
            this.previewFont.AutoScroll = true;
            this.previewFont.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.background;
            this.previewFont.Controls.Add(this.lblSample);
            this.previewFont.Location = new System.Drawing.Point(94, 12);
            this.previewFont.Name = "previewFont";
            this.previewFont.Size = new System.Drawing.Size(492, 100);
            this.previewFont.TabIndex = 2;
            this.previewFont.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.previewFont_MouseDoubleClick);
            // 
            // lblSample
            // 
            this.lblSample.AutoSize = true;
            this.lblSample.BackColor = System.Drawing.Color.Transparent;
            this.lblSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSample.ForeColor = System.Drawing.Color.Black;
            this.lblSample.Location = new System.Drawing.Point(0, 0);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(47, 13);
            this.lblSample.TabIndex = 1;
            this.lblSample.Text = "Aa 0123";
            // 
            // btnOpenFontDialog
            // 
            this.btnOpenFontDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFontDialog.Location = new System.Drawing.Point(17, 28);
            this.btnOpenFontDialog.Name = "btnOpenFontDialog";
            this.btnOpenFontDialog.Size = new System.Drawing.Size(35, 23);
            this.btnOpenFontDialog.TabIndex = 0;
            this.btnOpenFontDialog.Text = "...";
            this.btnOpenFontDialog.UseVisualStyleBackColor = true;
            this.btnOpenFontDialog.Click += new System.EventHandler(this.btnOpenFontDialog_Click);
            // 
            // fontDialog1
            // 
            this.fontDialog1.ShowColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.characterSetList);
            this.groupBox2.Location = new System.Drawing.Point(6, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(614, 118);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inform the Unicode Character Set";
            // 
            // characterSetList
            // 
            this.characterSetList.FormattingEnabled = true;
            this.characterSetList.Location = new System.Drawing.Point(23, 24);
            this.characterSetList.Name = "characterSetList";
            this.characterSetList.Size = new System.Drawing.Size(563, 79);
            this.characterSetList.TabIndex = 0;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(491, 252);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(128, 90);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlDestinationFolder);
            this.pnlContainer.Controls.Add(this.groupBox1);
            this.pnlContainer.Controls.Add(this.btnGenerate);
            this.pnlContainer.Controls.Add(this.groupBox2);
            this.pnlContainer.Location = new System.Drawing.Point(11, 34);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(631, 366);
            this.pnlContainer.TabIndex = 8;
            // 
            // pnlDestinationFolder
            // 
            this.pnlDestinationFolder.Location = new System.Drawing.Point(12, 253);
            this.pnlDestinationFolder.Name = "pnlDestinationFolder";
            this.pnlDestinationFolder.Size = new System.Drawing.Size(457, 100);
            this.pnlDestinationFolder.TabIndex = 8;
            // 
            // ImageLetterModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 416);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ImageLetterModeForm";
            this.Text = "ImageLetterModeForm";
            this.Load += new System.EventHandler(this.ImageLetterModeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.previewFont.ResumeLayout(false);
            this.previewFont.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenFontDialog;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox characterSetList;
        private System.Windows.Forms.Panel previewFont;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.Button btnGenerate;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlDestinationFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog backgroundColorDialog;
    }
}