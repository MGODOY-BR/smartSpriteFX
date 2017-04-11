namespace smartSuite.smartSpriteFX.Forms
{
    partial class SelectModeScreenForm
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
            this.btnLandscapeMode = new System.Windows.Forms.Button();
            this.btnEffectMode = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnEffectModeBatch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLandscapeMode
            // 
            this.btnLandscapeMode.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnLandscapeMode.FlatAppearance.BorderSize = 5;
            this.btnLandscapeMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLandscapeMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLandscapeMode.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLandscapeMode.Location = new System.Drawing.Point(68, 44);
            this.btnLandscapeMode.Name = "btnLandscapeMode";
            this.btnLandscapeMode.Size = new System.Drawing.Size(242, 65);
            this.btnLandscapeMode.TabIndex = 1;
            this.btnLandscapeMode.Text = "Landscape Mode";
            this.btnLandscapeMode.UseVisualStyleBackColor = true;
            this.btnLandscapeMode.Click += new System.EventHandler(this.btnLandscapeMode_Click);
            // 
            // btnEffectMode
            // 
            this.btnEffectMode.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnEffectMode.FlatAppearance.BorderSize = 5;
            this.btnEffectMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEffectMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEffectMode.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEffectMode.Location = new System.Drawing.Point(316, 44);
            this.btnEffectMode.Name = "btnEffectMode";
            this.btnEffectMode.Size = new System.Drawing.Size(258, 65);
            this.btnEffectMode.TabIndex = 1;
            this.btnEffectMode.Text = "Effect Mode";
            this.btnEffectMode.UseVisualStyleBackColor = true;
            this.btnEffectMode.Click += new System.EventHandler(this.btnEffectMode_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(641, 29);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Location = new System.Drawing.Point(341, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(296, 26);
            this.panel4.TabIndex = 1;
            // 
            // btnEffectModeBatch
            // 
            this.btnEffectModeBatch.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnEffectModeBatch.FlatAppearance.BorderSize = 5;
            this.btnEffectModeBatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEffectModeBatch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEffectModeBatch.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEffectModeBatch.Location = new System.Drawing.Point(316, 115);
            this.btnEffectModeBatch.Name = "btnEffectModeBatch";
            this.btnEffectModeBatch.Size = new System.Drawing.Size(258, 65);
            this.btnEffectModeBatch.TabIndex = 2;
            this.btnEffectModeBatch.Text = "Effect Mode Batch";
            this.btnEffectModeBatch.UseVisualStyleBackColor = true;
            this.btnEffectModeBatch.Click += new System.EventHandler(this.btnEffectModeBatch_Click);
            // 
            // SelectModeScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 214);
            this.Controls.Add(this.btnEffectModeBatch);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnEffectMode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLandscapeMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectModeScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please, select some edition mode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectModeScreenForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectModeScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLandscapeMode;
        private System.Windows.Forms.Button btnEffectMode;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnEffectModeBatch;
    }
}