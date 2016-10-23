﻿namespace smartSprite.Forms
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.topPanel = new System.Windows.Forms.Panel();
            this.frameBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.previewBoard = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlFilterPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.txtSettingsDescription = new System.Windows.Forms.TextBox();
            this.pnlSettingsMain = new System.Windows.Forms.Panel();
            this.pnlSettingTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelToolContainer = new System.Windows.Forms.TableLayoutPanel();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelTool = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.pnlSettingTop.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.panelToolContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(806, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.frameBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 36);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(806, 108);
            this.topPanel.TabIndex = 3;
            // 
            // frameBox
            // 
            this.frameBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.frameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameBox.Location = new System.Drawing.Point(0, 0);
            this.frameBox.Name = "frameBox";
            this.frameBox.Size = new System.Drawing.Size(806, 108);
            this.frameBox.TabIndex = 0;
            this.frameBox.TabStop = false;
            this.frameBox.Text = "Frames";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(179, 144);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(627, 311);
            this.splitContainer1.SplitterDistance = 406;
            this.splitContainer1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.BackgroundImage = global::smartSprite.Properties.Resources.background;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.previewBoard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 311);
            this.panel1.TabIndex = 0;
            // 
            // previewBoard
            // 
            this.previewBoard.BackColor = System.Drawing.Color.Transparent;
            this.previewBoard.Location = new System.Drawing.Point(0, 0);
            this.previewBoard.Name = "previewBoard";
            this.previewBoard.Size = new System.Drawing.Size(100, 50);
            this.previewBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.previewBoard.TabIndex = 0;
            this.previewBoard.TabStop = false;
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
            this.splitContainer2.Size = new System.Drawing.Size(217, 311);
            this.splitContainer2.SplitterDistance = 101;
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
            this.pnlFilterPanel.Size = new System.Drawing.Size(217, 73);
            this.pnlFilterPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.btnPreview);
            this.panel2.Controls.Add(this.btnApplyAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 28);
            this.panel2.TabIndex = 1;
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyAll.Location = new System.Drawing.Point(3, 2);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(91, 23);
            this.btnApplyAll.TabIndex = 0;
            this.btnApplyAll.Text = "APPLY ALL";
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
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
            this.splitContainer3.Size = new System.Drawing.Size(217, 173);
            this.splitContainer3.SplitterDistance = 65;
            this.splitContainer3.TabIndex = 2;
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
            this.txtSettingsDescription.Size = new System.Drawing.Size(217, 65);
            this.txtSettingsDescription.TabIndex = 0;
            // 
            // pnlSettingsMain
            // 
            this.pnlSettingsMain.AutoScroll = true;
            this.pnlSettingsMain.AutoSize = true;
            this.pnlSettingsMain.BackColor = System.Drawing.Color.Silver;
            this.pnlSettingsMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettingsMain.Location = new System.Drawing.Point(0, 0);
            this.pnlSettingsMain.Name = "pnlSettingsMain";
            this.pnlSettingsMain.Size = new System.Drawing.Size(217, 104);
            this.pnlSettingsMain.TabIndex = 1;
            // 
            // pnlSettingTop
            // 
            this.pnlSettingTop.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlSettingTop.Controls.Add(this.label1);
            this.pnlSettingTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSettingTop.Location = new System.Drawing.Point(0, 0);
            this.pnlSettingTop.Name = "pnlSettingTop";
            this.pnlSettingTop.Size = new System.Drawing.Size(217, 33);
            this.pnlSettingTop.TabIndex = 0;
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
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelContainer.Controls.Add(this.panelToolContainer);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelContainer.Location = new System.Drawing.Point(0, 144);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(179, 311);
            this.panelContainer.TabIndex = 4;
            // 
            // panelToolContainer
            // 
            this.panelToolContainer.ColumnCount = 1;
            this.panelToolContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelToolContainer.Controls.Add(this.panelBrowser, 0, 0);
            this.panelToolContainer.Controls.Add(this.groupBox1, 0, 1);
            this.panelToolContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelToolContainer.Location = new System.Drawing.Point(0, 0);
            this.panelToolContainer.Name = "panelToolContainer";
            this.panelToolContainer.RowCount = 2;
            this.panelToolContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.44373F));
            this.panelToolContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.55627F));
            this.panelToolContainer.Size = new System.Drawing.Size(179, 311);
            this.panelToolContainer.TabIndex = 0;
            // 
            // panelBrowser
            // 
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBrowser.Location = new System.Drawing.Point(3, 3);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(173, 125);
            this.panelBrowser.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelTool);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 174);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Toolbox";
            // 
            // panelTool
            // 
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTool.Location = new System.Drawing.Point(3, 16);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(167, 155);
            this.panelTool.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::smartSprite.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(806, 36);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(100, 2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "PREVIEW";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // AnimationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 477);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AnimationForm";
            this.Text = "AnimationForm";
            this.Load += new System.EventHandler(this.AnimationForm_Load);
            this.topPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBoard)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.pnlSettingTop.ResumeLayout(false);
            this.pnlSettingTop.PerformLayout();
            this.panelContainer.ResumeLayout(false);
            this.panelToolContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox previewBoard;
        private System.Windows.Forms.Panel topPanel;
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
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.TableLayoutPanel panelToolContainer;
        private System.Windows.Forms.Panel panelBrowser;
        private System.Windows.Forms.Panel panelTool;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.Button btnPreview;
    }
}