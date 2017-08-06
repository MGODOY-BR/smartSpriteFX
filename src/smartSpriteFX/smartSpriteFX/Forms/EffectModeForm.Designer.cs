namespace smartSuite.smartSpriteFX.Forms
{
    partial class EffectModeForm
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.frameBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.previewBoard = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chkBoxFitImage = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.filterSetFrame = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnApplyOne = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.pnlFilterPanel = new System.Windows.Forms.Panel();
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.filterSetOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.filterSetSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
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
            this.filterSetFrame.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 693);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1162, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.frameBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 29);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1162, 130);
            this.topPanel.TabIndex = 3;
            // 
            // frameBox
            // 
            this.frameBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.frameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameBox.Location = new System.Drawing.Point(0, 0);
            this.frameBox.Name = "frameBox";
            this.frameBox.Size = new System.Drawing.Size(1162, 130);
            this.frameBox.TabIndex = 0;
            this.frameBox.TabStop = false;
            this.frameBox.Text = "Frames";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(179, 159);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(983, 534);
            this.splitContainer1.SplitterDistance = 605;
            this.splitContainer1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.background;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.previewBoard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 534);
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
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Brown;
            this.splitContainer2.Panel1.Controls.Add(this.chkBoxFitImage);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2.Controls.Add(this.pnlSettingTop);
            this.splitContainer2.Size = new System.Drawing.Size(374, 534);
            this.splitContainer2.SplitterDistance = 233;
            this.splitContainer2.TabIndex = 0;
            // 
            // chkBoxFitImage
            // 
            this.chkBoxFitImage.AutoSize = true;
            this.chkBoxFitImage.ForeColor = System.Drawing.Color.White;
            this.chkBoxFitImage.Location = new System.Drawing.Point(3, 61);
            this.chkBoxFitImage.Name = "chkBoxFitImage";
            this.chkBoxFitImage.Size = new System.Drawing.Size(68, 17);
            this.chkBoxFitImage.TabIndex = 3;
            this.chkBoxFitImage.Text = "Fit image";
            this.toolTip1.SetToolTip(this.chkBoxFitImage, "Forces the image to fit in the preview area (it doesn\'t modify the real image, or" +
        " the result of filters)");
            this.chkBoxFitImage.UseVisualStyleBackColor = true;
            this.chkBoxFitImage.CheckedChanged += new System.EventHandler(this.chkBoxFitImage_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.filterSetFrame);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.pnlFilterPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(77, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(297, 233);
            this.panel2.TabIndex = 2;
            // 
            // filterSetFrame
            // 
            this.filterSetFrame.Controls.Add(this.btnSave);
            this.filterSetFrame.Controls.Add(this.btnLoad);
            this.filterSetFrame.Location = new System.Drawing.Point(3, 146);
            this.filterSetFrame.Name = "filterSetFrame";
            this.filterSetFrame.Size = new System.Drawing.Size(131, 82);
            this.filterSetFrame.TabIndex = 2;
            this.filterSetFrame.TabStop = false;
            this.filterSetFrame.Text = "Filter set";
            this.filterSetFrame.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.pendrive_0_filtered;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Location = new System.Drawing.Point(66, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(59, 59);
            this.btnSave.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnSave, "Save the current filter set");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.opened_folder_0_filtered;
            this.btnLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoad.Location = new System.Drawing.Point(6, 17);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(54, 59);
            this.btnLoad.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnLoad, "Open an existent filter set");
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnApplyOne);
            this.groupBox2.Controls.Add(this.btnPreview);
            this.groupBox2.Controls.Add(this.btnApplyAll);
            this.groupBox2.Location = new System.Drawing.Point(24, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(104, 134);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnApplyOne
            // 
            this.btnApplyOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyOne.Location = new System.Drawing.Point(5, 44);
            this.btnApplyOne.Name = "btnApplyOne";
            this.btnApplyOne.Size = new System.Drawing.Size(93, 31);
            this.btnApplyOne.TabIndex = 2;
            this.btnApplyOne.Text = "Apply One";
            this.toolTip1.SetToolTip(this.btnApplyOne, "Applies all the filters in the current frame only");
            this.btnApplyOne.UseVisualStyleBackColor = true;
            this.btnApplyOne.Click += new System.EventHandler(this.btnApplyOne_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(6, 19);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(93, 23);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "PREVIEW";
            this.toolTip1.SetToolTip(this.btnPreview, "Previews the result of filters");
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyAll.Location = new System.Drawing.Point(6, 90);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(93, 31);
            this.btnApplyAll.TabIndex = 0;
            this.btnApplyAll.Text = "APPLY ALL";
            this.toolTip1.SetToolTip(this.btnApplyAll, "Applies all the filters against all the frames in animation folder");
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // pnlFilterPanel
            // 
            this.pnlFilterPanel.AutoScroll = true;
            this.pnlFilterPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlFilterPanel.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.FilterBox;
            this.pnlFilterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFilterPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlFilterPanel.Location = new System.Drawing.Point(140, 0);
            this.pnlFilterPanel.Name = "pnlFilterPanel";
            this.pnlFilterPanel.Size = new System.Drawing.Size(157, 233);
            this.pnlFilterPanel.TabIndex = 0;
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
            this.splitContainer3.Size = new System.Drawing.Size(374, 264);
            this.splitContainer3.SplitterDistance = 95;
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
            this.txtSettingsDescription.Size = new System.Drawing.Size(374, 95);
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
            this.pnlSettingsMain.Size = new System.Drawing.Size(374, 165);
            this.pnlSettingsMain.TabIndex = 1;
            // 
            // pnlSettingTop
            // 
            this.pnlSettingTop.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlSettingTop.Controls.Add(this.label1);
            this.pnlSettingTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSettingTop.Location = new System.Drawing.Point(0, 0);
            this.pnlSettingTop.Name = "pnlSettingTop";
            this.pnlSettingTop.Size = new System.Drawing.Size(374, 33);
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
            this.panelContainer.Location = new System.Drawing.Point(0, 159);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(179, 534);
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
            this.panelToolContainer.Size = new System.Drawing.Size(179, 534);
            this.panelToolContainer.TabIndex = 0;
            // 
            // panelBrowser
            // 
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBrowser.Location = new System.Drawing.Point(3, 3);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(173, 220);
            this.panelBrowser.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelTool);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 302);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Toolbox";
            // 
            // panelTool
            // 
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTool.Location = new System.Drawing.Point(3, 16);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(167, 283);
            this.panelTool.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::smartSuite.smartSpriteFX.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1162, 29);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // filterSetOpenDialog
            // 
            this.filterSetOpenDialog.DefaultExt = "*.filterSet";
            this.filterSetOpenDialog.Filter = "Current version filter set file|*.3.0.0.0-filterSet|Regardless of version Filter " +
    "Set|*.filterSet";
            this.filterSetOpenDialog.InitialDirectory = "My Filter Sets";
            this.filterSetOpenDialog.Title = "Open a filter set to fill filter box";
            this.filterSetOpenDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.filterSetOpenDialog_FileOk);
            // 
            // filterSetSaveDialog
            // 
            this.filterSetSaveDialog.DefaultExt = "*.3.0.0.0-filterSet";
            this.filterSetSaveDialog.Filter = "Current version filter set file|*.3.0.0.0-filterSet|Regardless of version Filter " +
    "Set|*.filterSet";
            this.filterSetSaveDialog.InitialDirectory = "My Filter Sets";
            this.filterSetSaveDialog.Title = "Save filter box as filter set";
            this.filterSetSaveDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.filterSetSaveDialog_FileOk);
            // 
            // EffectModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 715);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "EffectModeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "smartSpriteFX - Effect Mode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EffectModeForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
            this.filterSetFrame.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkBoxFitImage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnApplyOne;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox filterSetFrame;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog filterSetOpenDialog;
        private System.Windows.Forms.SaveFileDialog filterSetSaveDialog;
    }
}