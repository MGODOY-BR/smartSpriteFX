namespace smartSpriteFX.Forms.Controls
{
    partial class DraftControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgDraft = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgDraft)).BeginInit();
            this.SuspendLayout();
            // 
            // imgDraft
            // 
            this.imgDraft.Cursor = System.Windows.Forms.Cursors.Default;
            this.imgDraft.Location = new System.Drawing.Point(3, 0);
            this.imgDraft.Name = "imgDraft";
            this.imgDraft.Size = new System.Drawing.Size(736, 371);
            this.imgDraft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgDraft.TabIndex = 1;
            this.imgDraft.TabStop = false;
            this.imgDraft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgDraft_MouseClick);
            this.imgDraft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgDraft_MouseMove);
            // 
            // DraftControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.imgDraft);
            this.Name = "DraftControl";
            this.Size = new System.Drawing.Size(742, 374);
            ((System.ComponentModel.ISupportInitialize)(this.imgDraft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgDraft;
    }
}
