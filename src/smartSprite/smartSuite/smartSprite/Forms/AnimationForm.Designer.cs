namespace smartSprite.Forms
{
    partial class AnimationForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.previewBoard = new System.Windows.Forms.PictureBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.frameBox = new System.Windows.Forms.GroupBox();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlFilterPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlSettingTop = new System.Windows.Forms.Panel();
            this.pnlSettingsMain = new System.Windows.Forms.Panel();
            this.txtSettingsDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBoard)).BeginInit();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnlSettingTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::smartSprite.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(630, 36);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 612);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(630, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.previewBoard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 468);
            this.panel1.TabIndex = 0;
            // 
            // previewBoard
            // 
            this.previewBoard.Location = new System.Drawing.Point(0, 0);
            this.previewBoard.Name = "previewBoard";
            this.previewBoard.Size = new System.Drawing.Size(100, 50);
            this.previewBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.previewBoard.TabIndex = 0;
            this.previewBoard.TabStop = false;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.frameBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 36);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(630, 108);
            this.topPanel.TabIndex = 3;
            // 
            // frameBox
            // 
            this.frameBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.frameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameBox.Location = new System.Drawing.Point(0, 0);
            this.frameBox.Name = "frameBox";
            this.frameBox.Size = new System.Drawing.Size(630, 108);
            this.frameBox.TabIndex = 0;
            this.frameBox.TabStop = false;
            this.frameBox.Text = "Frames";
            // 
            // panelBrowser
            // 
            this.panelBrowser.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBrowser.Location = new System.Drawing.Point(0, 144);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(126, 468);
            this.panelBrowser.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(126, 144);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(504, 468);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pnlFilterPanel);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2.Controls.Add(this.pnlSettingTop);
            this.splitContainer2.Size = new System.Drawing.Size(173, 468);
            this.splitContainer2.SplitterDistance = 195;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnlFilterPanel
            // 
            this.pnlFilterPanel.AutoScroll = true;
            this.pnlFilterPanel.AutoSize = true;
            this.pnlFilterPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlFilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFilterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilterPanel.Location = new System.Drawing.Point(0, 28);
            this.pnlFilterPanel.Name = "pnlFilterPanel";
            this.pnlFilterPanel.Size = new System.Drawing.Size(173, 167);
            this.pnlFilterPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(173, 28);
            this.panel2.TabIndex = 1;
            // 
            // pnlSettingTop
            // 
            this.pnlSettingTop.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlSettingTop.Controls.Add(this.label1);
            this.pnlSettingTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSettingTop.Location = new System.Drawing.Point(0, 0);
            this.pnlSettingTop.Name = "pnlSettingTop";
            this.pnlSettingTop.Size = new System.Drawing.Size(173, 33);
            this.pnlSettingTop.TabIndex = 0;
            // 
            // pnlSettingsMain
            // 
            this.pnlSettingsMain.AutoScroll = true;
            this.pnlSettingsMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettingsMain.Location = new System.Drawing.Point(0, 0);
            this.pnlSettingsMain.Name = "pnlSettingsMain";
            this.pnlSettingsMain.Size = new System.Drawing.Size(173, 181);
            this.pnlSettingsMain.TabIndex = 1;
            // 
            // txtSettingsDescription
            // 
            this.txtSettingsDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSettingsDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSettingsDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSettingsDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSettingsDescription.Location = new System.Drawing.Point(0, 0);
            this.txtSettingsDescription.Multiline = true;
            this.txtSettingsDescription.Name = "txtSettingsDescription";
            this.txtSettingsDescription.ReadOnly = true;
            this.txtSettingsDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSettingsDescription.Size = new System.Drawing.Size(173, 51);
            this.txtSettingsDescription.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter Description";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 33);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.txtSettingsDescription);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pnlSettingsMain);
            this.splitContainer3.Size = new System.Drawing.Size(173, 236);
            this.splitContainer3.SplitterDistance = 51;
            this.splitContainer3.TabIndex = 2;
            // 
            // AnimationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 634);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelBrowser);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AnimationForm";
            this.Text = "AnimationForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBoard)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pnlSettingTop.ResumeLayout(false);
            this.pnlSettingTop.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox previewBoard;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel panelBrowser;
        private System.Windows.Forms.GroupBox frameBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pnlFilterPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlSettingTop;
        private System.Windows.Forms.Panel pnlSettingsMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSettingsDescription;
        private System.Windows.Forms.SplitContainer splitContainer3;
    }
}