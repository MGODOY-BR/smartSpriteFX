namespace smartSuite.smartSpriteFX.Forms
{
    partial class ToolsForm
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
            this.grpJoinSprites = new System.Windows.Forms.GroupBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.grpDestinationJoin = new System.Windows.Forms.GroupBox();
            this.grpOriginFolderJoin = new System.Windows.Forms.GroupBox();
            this.joinSpritesBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.grpJoinSprites.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpJoinSprites
            // 
            this.grpJoinSprites.Controls.Add(this.btnJoin);
            this.grpJoinSprites.Controls.Add(this.grpDestinationJoin);
            this.grpJoinSprites.Controls.Add(this.grpOriginFolderJoin);
            this.grpJoinSprites.Location = new System.Drawing.Point(12, 12);
            this.grpJoinSprites.Name = "grpJoinSprites";
            this.grpJoinSprites.Size = new System.Drawing.Size(365, 309);
            this.grpJoinSprites.TabIndex = 0;
            this.grpJoinSprites.TabStop = false;
            this.grpJoinSprites.Text = "Join sprite to SpriteSheet";
            // 
            // btnJoin
            // 
            this.btnJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoin.Location = new System.Drawing.Point(126, 257);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(96, 42);
            this.btnJoin.TabIndex = 2;
            this.btnJoin.Text = "Join";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // grpDestinationJoin
            // 
            this.grpDestinationJoin.Location = new System.Drawing.Point(15, 138);
            this.grpDestinationJoin.Name = "grpDestinationJoin";
            this.grpDestinationJoin.Size = new System.Drawing.Size(333, 113);
            this.grpDestinationJoin.TabIndex = 1;
            this.grpDestinationJoin.TabStop = false;
            this.grpDestinationJoin.Text = "Destination File";
            // 
            // grpOriginFolderJoin
            // 
            this.grpOriginFolderJoin.Location = new System.Drawing.Point(15, 19);
            this.grpOriginFolderJoin.Name = "grpOriginFolderJoin";
            this.grpOriginFolderJoin.Size = new System.Drawing.Size(333, 113);
            this.grpOriginFolderJoin.TabIndex = 0;
            this.grpOriginFolderJoin.TabStop = false;
            this.grpOriginFolderJoin.Text = "Origin Folder";
            // 
            // joinSpritesBackgroundWorker
            // 
            this.joinSpritesBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.joinSpritesBackgroundWorker_DoWork);
            this.joinSpritesBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.joinSpritesBackgroundWorker_RunWorkerCompleted);
            // 
            // ToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 368);
            this.Controls.Add(this.grpJoinSprites);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolsForm";
            this.Text = "Tools";
            this.Load += new System.EventHandler(this.ToolsForm_Load);
            this.grpJoinSprites.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpJoinSprites;
        private System.Windows.Forms.GroupBox grpDestinationJoin;
        private System.Windows.Forms.GroupBox grpOriginFolderJoin;
        private System.Windows.Forms.Button btnJoin;
        private System.ComponentModel.BackgroundWorker joinSpritesBackgroundWorker;
    }
}