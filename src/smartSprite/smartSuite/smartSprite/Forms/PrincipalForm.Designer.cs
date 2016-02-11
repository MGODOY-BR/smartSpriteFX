namespace smartSprite.Forms
{
    partial class PrincipalForm
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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSourceFolder = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpenResumeWork = new System.Windows.Forms.Button();
            this.txtLoadSprite = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenDraft = new System.Windows.Forms.Button();
            this.txtDraftPicture = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.draftControl1 = new smartSprite.Forms.Controls.DraftControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolHookButton = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveState = new System.Windows.Forms.Button();
            this.btnExportToUnity = new System.Windows.Forms.Button();
            this.openDraftFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openSmartSpriteFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.exportToUnityDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlSourceFolder.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.pnlImage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 494);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1102, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1102, 494);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 44);
            this.panel1.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(7, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(51, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Indefined";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::smartSprite.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(494, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.27648F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.72353F));
            this.tableLayoutPanel2.Controls.Add(this.pnlSourceFolder, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1096, 438);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // pnlSourceFolder
            // 
            this.pnlSourceFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlSourceFolder.Controls.Add(this.groupBox2);
            this.pnlSourceFolder.Controls.Add(this.groupBox1);
            this.pnlSourceFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSourceFolder.Location = new System.Drawing.Point(3, 3);
            this.pnlSourceFolder.Name = "pnlSourceFolder";
            this.pnlSourceFolder.Size = new System.Drawing.Size(172, 432);
            this.pnlSourceFolder.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenResumeWork);
            this.groupBox2.Controls.Add(this.txtLoadSprite);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 113);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resume work...";
            // 
            // btnOpenResumeWork
            // 
            this.btnOpenResumeWork.Location = new System.Drawing.Point(6, 18);
            this.btnOpenResumeWork.Name = "btnOpenResumeWork";
            this.btnOpenResumeWork.Size = new System.Drawing.Size(37, 24);
            this.btnOpenResumeWork.TabIndex = 3;
            this.btnOpenResumeWork.Text = "...";
            this.btnOpenResumeWork.UseVisualStyleBackColor = true;
            this.btnOpenResumeWork.Click += new System.EventHandler(this.btnOpenResumeWork_Click);
            // 
            // txtLoadSprite
            // 
            this.txtLoadSprite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoadSprite.Location = new System.Drawing.Point(6, 48);
            this.txtLoadSprite.Multiline = true;
            this.txtLoadSprite.Name = "txtLoadSprite";
            this.txtLoadSprite.Size = new System.Drawing.Size(160, 44);
            this.txtLoadSprite.TabIndex = 4;
            this.txtLoadSprite.Leave += new System.EventHandler(this.txtLoadSprite_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenDraft);
            this.groupBox1.Controls.Add(this.txtDraftPicture);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New from draft...";
            // 
            // btnOpenDraft
            // 
            this.btnOpenDraft.Location = new System.Drawing.Point(6, 19);
            this.btnOpenDraft.Name = "btnOpenDraft";
            this.btnOpenDraft.Size = new System.Drawing.Size(37, 24);
            this.btnOpenDraft.TabIndex = 1;
            this.btnOpenDraft.Text = "...";
            this.btnOpenDraft.UseVisualStyleBackColor = true;
            this.btnOpenDraft.Click += new System.EventHandler(this.btnOpenDraft_Click);
            // 
            // txtDraftPicture
            // 
            this.txtDraftPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDraftPicture.Location = new System.Drawing.Point(6, 49);
            this.txtDraftPicture.Multiline = true;
            this.txtDraftPicture.Name = "txtDraftPicture";
            this.txtDraftPicture.Size = new System.Drawing.Size(160, 44);
            this.txtDraftPicture.TabIndex = 2;
            this.txtDraftPicture.Leave += new System.EventHandler(this.txtDraftPicture_Leave);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(181, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.splitContainer1.Panel1.Controls.Add(this.toolStripContainer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(912, 432);
            this.splitContainer1.SplitterDistance = 687;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlImage);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(687, 407);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(687, 432);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // pnlImage
            // 
            this.pnlImage.AutoScroll = true;
            this.pnlImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlImage.Controls.Add(this.draftControl1);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(687, 407);
            this.pnlImage.TabIndex = 0;
            // 
            // draftControl1
            // 
            this.draftControl1.AutoSize = true;
            this.draftControl1.LastSettings = null;
            this.draftControl1.Location = new System.Drawing.Point(0, 0);
            this.draftControl1.Name = "draftControl1";
            this.draftControl1.ProjectFullPath = null;
            this.draftControl1.Size = new System.Drawing.Size(742, 374);
            this.draftControl1.TabIndex = 0;
            this.draftControl1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolHookButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolHookButton
            // 
            this.toolHookButton.CheckOnClick = true;
            this.toolHookButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolHookButton.Image = global::smartSprite.Properties.Resources.Hook;
            this.toolHookButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHookButton.Name = "toolHookButton";
            this.toolHookButton.Size = new System.Drawing.Size(23, 22);
            this.toolHookButton.ToolTipText = "Hook";
            this.toolHookButton.Click += new System.EventHandler(this.toolHookButton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.260726F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.73927F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(217, 432);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GameObject hierarchy";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 29);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(211, 331);
            this.treeView1.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnSaveState, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnExportToUnity, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 366);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(211, 63);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // btnSaveState
            // 
            this.btnSaveState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveState.Location = new System.Drawing.Point(3, 3);
            this.btnSaveState.Name = "btnSaveState";
            this.btnSaveState.Size = new System.Drawing.Size(99, 57);
            this.btnSaveState.TabIndex = 0;
            this.btnSaveState.Text = "Save";
            this.btnSaveState.UseVisualStyleBackColor = true;
            this.btnSaveState.Click += new System.EventHandler(this.btnSaveState_Click);
            // 
            // btnExportToUnity
            // 
            this.btnExportToUnity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToUnity.Location = new System.Drawing.Point(108, 3);
            this.btnExportToUnity.Name = "btnExportToUnity";
            this.btnExportToUnity.Size = new System.Drawing.Size(100, 57);
            this.btnExportToUnity.TabIndex = 1;
            this.btnExportToUnity.Text = "Send to Unity";
            this.btnExportToUnity.UseVisualStyleBackColor = true;
            this.btnExportToUnity.Click += new System.EventHandler(this.btnExportToUnity_Click);
            // 
            // openDraftFileDialog1
            // 
            this.openDraftFileDialog1.Filter = "PNG Files|*.png|JPEG files|*.JPEG|JPG files|*.JPG|All files|*.*";
            this.openDraftFileDialog1.SupportMultiDottedExtensions = true;
            this.openDraftFileDialog1.Title = "Open draft";
            // 
            // openSmartSpriteFileDialog1
            // 
            this.openSmartSpriteFileDialog1.Filter = "smartSprite File|*.smartSprite";
            this.openSmartSpriteFileDialog1.Title = "Open project";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "smartSprite";
            this.saveFileDialog1.Filter = "SmartSprite Project|*.smartSprite";
            this.saveFileDialog1.Title = "Save project as";
            // 
            // exportToUnityDialog1
            // 
            this.exportToUnityDialog1.Description = "Select an Unity Asset folder";
            this.exportToUnityDialog1.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // PrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 516);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "PrincipalForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnlSourceFolder.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.pnlImage.ResumeLayout(false);
            this.pnlImage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.OpenFileDialog openDraftFileDialog1;
        private System.Windows.Forms.OpenFileDialog openSmartSpriteFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnlSourceFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpenResumeWork;
        private System.Windows.Forms.TextBox txtLoadSprite;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenDraft;
        private System.Windows.Forms.TextBox txtDraftPicture;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel pnlImage;
        private Controls.DraftControl draftControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolHookButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnSaveState;
        private System.Windows.Forms.Button btnExportToUnity;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog exportToUnityDialog1;
    }
}