namespace smartSuite.smartSpriteFX.Forms
{
    partial class EffectModeBatchForm
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
            this.grpOriginFolder = new System.Windows.Forms.GroupBox();
            this.grpFilterSet = new System.Windows.Forms.GroupBox();
            this.grpCommand = new System.Windows.Forms.GroupBox();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpOriginFolder
            // 
            this.grpOriginFolder.Location = new System.Drawing.Point(12, 39);
            this.grpOriginFolder.Name = "grpOriginFolder";
            this.grpOriginFolder.Size = new System.Drawing.Size(199, 142);
            this.grpOriginFolder.TabIndex = 0;
            this.grpOriginFolder.TabStop = false;
            this.grpOriginFolder.Text = "1) Select the top folder";
            // 
            // grpFilterSet
            // 
            this.grpFilterSet.Location = new System.Drawing.Point(217, 39);
            this.grpFilterSet.Name = "grpFilterSet";
            this.grpFilterSet.Size = new System.Drawing.Size(199, 142);
            this.grpFilterSet.TabIndex = 1;
            this.grpFilterSet.TabStop = false;
            this.grpFilterSet.Text = "2) Select the filter set";
            // 
            // grpCommand
            // 
            this.grpCommand.Controls.Add(this.btnApplyAll);
            this.grpCommand.Location = new System.Drawing.Point(422, 39);
            this.grpCommand.Name = "grpCommand";
            this.grpCommand.Size = new System.Drawing.Size(199, 142);
            this.grpCommand.TabIndex = 2;
            this.grpCommand.TabStop = false;
            this.grpCommand.Text = "3) Let\'s go!!";
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyAll.Location = new System.Drawing.Point(51, 65);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(102, 29);
            this.btnApplyAll.TabIndex = 0;
            this.btnApplyAll.Text = "APPLY ALL";
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::smartSuite.smartSpriteFX.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(629, 28);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // EffectModeBatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 193);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grpCommand);
            this.Controls.Add(this.grpFilterSet);
            this.Controls.Add(this.grpOriginFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EffectModeBatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "smartSpriteFX - Effect Mode Batch";
            this.Load += new System.EventHandler(this.EffectModeBatchForm_Load);
            this.grpCommand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOriginFolder;
        private System.Windows.Forms.GroupBox grpFilterSet;
        private System.Windows.Forms.GroupBox grpCommand;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}