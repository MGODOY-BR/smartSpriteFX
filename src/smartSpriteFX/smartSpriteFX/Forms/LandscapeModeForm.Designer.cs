namespace smartSuite.smartSpriteFX.Forms
{
    partial class LandscapeModeForm
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
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.browserPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolHookButton = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPieceBlock = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveState = new System.Windows.Forms.Button();
            this.btnCutInPieces = new System.Windows.Forms.Button();
            this.chkInferTransparentBackground = new System.Windows.Forms.CheckBox();
            this.openDraftFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openSmartSpriteFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cutInPiecesDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.savePiecesBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressTime = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.pnlImage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPieceBlock.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1019, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1019, 389);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 31);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::smartSuite.smartSpriteFX.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
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
            this.tableLayoutPanel2.Controls.Add(this.browserPanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 40);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1013, 346);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // browserPanel
            // 
            this.browserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.browserPanel.Location = new System.Drawing.Point(3, 3);
            this.browserPanel.Name = "browserPanel";
            this.browserPanel.Size = new System.Drawing.Size(158, 340);
            this.browserPanel.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(167, 3);
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
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPieceBlock);
            this.splitContainer1.Size = new System.Drawing.Size(843, 340);
            this.splitContainer1.SplitterDistance = 635;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlImage);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(635, 340);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(635, 340);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // pnlImage
            // 
            this.pnlImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlImage.Controls.Add(this.hScrollBar1);
            this.pnlImage.Controls.Add(this.vScrollBar1);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(635, 340);
            this.pnlImage.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 319);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(614, 17);
            this.hScrollBar1.TabIndex = 1;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(614, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 336);
            this.vScrollBar1.TabIndex = 0;
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
            this.toolStrip1.Visible = false;
            // 
            // toolHookButton
            // 
            this.toolHookButton.CheckOnClick = true;
            this.toolHookButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolHookButton.Image = global::smartSuite.smartSpriteFX.Properties.Resources.Hook;
            this.toolHookButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHookButton.Name = "toolHookButton";
            this.toolHookButton.Size = new System.Drawing.Size(23, 22);
            this.toolHookButton.ToolTipText = "Hook";
            this.toolHookButton.Click += new System.EventHandler(this.toolHookButton_Click);
            // 
            // tableLayoutPieceBlock
            // 
            this.tableLayoutPieceBlock.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tableLayoutPieceBlock.ColumnCount = 1;
            this.tableLayoutPieceBlock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPieceBlock.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPieceBlock.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPieceBlock.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPieceBlock.Controls.Add(this.chkInferTransparentBackground, 0, 3);
            this.tableLayoutPieceBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPieceBlock.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPieceBlock.Name = "tableLayoutPieceBlock";
            this.tableLayoutPieceBlock.RowCount = 4;
            this.tableLayoutPieceBlock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.260726F));
            this.tableLayoutPieceBlock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.73927F));
            this.tableLayoutPieceBlock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPieceBlock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPieceBlock.Size = new System.Drawing.Size(200, 340);
            this.tableLayoutPieceBlock.TabIndex = 1;
            this.tableLayoutPieceBlock.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Piece hierarchy";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 21);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(194, 227);
            this.treeView1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.treeView1, "Piece hierarchy (big things are to comes up from here in near future...)");
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnSaveState, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCutInPieces, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 254);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(194, 58);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // btnSaveState
            // 
            this.btnSaveState.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSaveState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveState.Location = new System.Drawing.Point(3, 3);
            this.btnSaveState.Name = "btnSaveState";
            this.btnSaveState.Size = new System.Drawing.Size(91, 52);
            this.btnSaveState.TabIndex = 0;
            this.btnSaveState.Text = "Save Project";
            this.toolTip1.SetToolTip(this.btnSaveState, "Click to save your work");
            this.btnSaveState.UseVisualStyleBackColor = true;
            this.btnSaveState.Click += new System.EventHandler(this.btnSaveState_Click);
            // 
            // btnCutInPieces
            // 
            this.btnCutInPieces.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCutInPieces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCutInPieces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCutInPieces.Location = new System.Drawing.Point(100, 3);
            this.btnCutInPieces.Name = "btnCutInPieces";
            this.btnCutInPieces.Size = new System.Drawing.Size(91, 52);
            this.btnCutInPieces.TabIndex = 1;
            this.btnCutInPieces.Text = "Save/Cut in Pieces";
            this.toolTip1.SetToolTip(this.btnCutInPieces, "Click to procced to cut the pieces and use them in Unity");
            this.btnCutInPieces.UseVisualStyleBackColor = false;
            this.btnCutInPieces.Click += new System.EventHandler(this.btnCutInPieces_Click);
            // 
            // chkInferTransparentBackground
            // 
            this.chkInferTransparentBackground.AutoSize = true;
            this.chkInferTransparentBackground.Checked = true;
            this.chkInferTransparentBackground.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInferTransparentBackground.Location = new System.Drawing.Point(3, 318);
            this.chkInferTransparentBackground.Name = "chkInferTransparentBackground";
            this.chkInferTransparentBackground.Size = new System.Drawing.Size(163, 17);
            this.chkInferTransparentBackground.TabIndex = 3;
            this.chkInferTransparentBackground.Text = "Infer transparent background";
            this.chkInferTransparentBackground.UseVisualStyleBackColor = true;
            // 
            // openDraftFileDialog1
            // 
            this.openDraftFileDialog1.Filter = "Bitmap Files|*.bmp|PNG Files|*.png|JPEG files|*.JPEG|JPG files|*.JPG|All files|*." +
    "*";
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
            // cutInPiecesDialog
            // 
            this.cutInPiecesDialog.Description = "Select an piece folder";
            // 
            // savePiecesBackgroundWorker
            // 
            this.savePiecesBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.savePiecesBackgroundWorker_DoWork);
            this.savePiecesBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.savePiecesBackgroundWorker_RunWorkerCompleted);
            // 
            // progressTime
            // 
            this.progressTime.Interval = 5;
            this.progressTime.Tick += new System.EventHandler(this.progressTime_Tick);
            // 
            // LandscapeModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 411);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "LandscapeModeForm";
            this.Text = "smartSpriteFX - Landscape Mode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPieceBlock.ResumeLayout(false);
            this.tableLayoutPieceBlock.PerformLayout();
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
        private System.Windows.Forms.OpenFileDialog openDraftFileDialog1;
        private System.Windows.Forms.OpenFileDialog openSmartSpriteFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel pnlImage;
        private Controls.DraftControl draftControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolHookButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPieceBlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnSaveState;
        private System.Windows.Forms.Button btnCutInPieces;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog cutInPiecesDialog;
        private System.ComponentModel.BackgroundWorker savePiecesBackgroundWorker;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Timer progressTime;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel browserPanel;
        private System.Windows.Forms.CheckBox chkInferTransparentBackground;
    }
}