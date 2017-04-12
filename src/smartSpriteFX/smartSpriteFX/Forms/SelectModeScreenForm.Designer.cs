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
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnEffectModeBatch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imgDemo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDemo)).BeginInit();
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
            this.btnLandscapeMode.Location = new System.Drawing.Point(3, 3);
            this.btnLandscapeMode.Name = "btnLandscapeMode";
            this.btnLandscapeMode.Size = new System.Drawing.Size(196, 41);
            this.btnLandscapeMode.TabIndex = 1;
            this.btnLandscapeMode.Text = "Landscape Mode";
            this.btnLandscapeMode.UseVisualStyleBackColor = true;
            this.btnLandscapeMode.Click += new System.EventHandler(this.btnLandscapeMode_Click);
            this.btnLandscapeMode.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnLandscapeMode_MouseMove);
            // 
            // btnEffectMode
            // 
            this.btnEffectMode.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnEffectMode.FlatAppearance.BorderSize = 5;
            this.btnEffectMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEffectMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEffectMode.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEffectMode.Location = new System.Drawing.Point(280, 3);
            this.btnEffectMode.Name = "btnEffectMode";
            this.btnEffectMode.Size = new System.Drawing.Size(212, 41);
            this.btnEffectMode.TabIndex = 1;
            this.btnEffectMode.Text = "Effect Mode";
            this.btnEffectMode.UseVisualStyleBackColor = true;
            this.btnEffectMode.Click += new System.EventHandler(this.btnEffectMode_Click);
            this.btnEffectMode.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnEffectMode_MouseMove);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel4.Location = new System.Drawing.Point(455, 3);
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
            this.btnEffectModeBatch.Location = new System.Drawing.Point(539, 4);
            this.btnEffectModeBatch.Name = "btnEffectModeBatch";
            this.btnEffectModeBatch.Size = new System.Drawing.Size(212, 41);
            this.btnEffectModeBatch.TabIndex = 2;
            this.btnEffectModeBatch.Text = "Effect Mode Batch";
            this.btnEffectModeBatch.UseVisualStyleBackColor = true;
            this.btnEffectModeBatch.Click += new System.EventHandler(this.btnEffectModeBatch_Click);
            this.btnEffectModeBatch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnEffectModeBatch_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLandscapeMode);
            this.panel1.Controls.Add(this.btnEffectMode);
            this.panel1.Controls.Add(this.btnEffectModeBatch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 48);
            this.panel1.TabIndex = 4;
            // 
            // imgDemo
            // 
            this.imgDemo.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.FxDemo;
            this.imgDemo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgDemo.Location = new System.Drawing.Point(0, 29);
            this.imgDemo.Name = "imgDemo";
            this.imgDemo.Size = new System.Drawing.Size(754, 401);
            this.imgDemo.TabIndex = 5;
            this.imgDemo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::smartSuite.smartSpriteFX.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(754, 29);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Black;
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(257, 295);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(485, 104);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Visible = false;
            // 
            // SelectModeScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(754, 478);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.imgDemo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectModeScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please, select some edition mode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectModeScreenForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectModeScreenForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLandscapeMode;
        private System.Windows.Forms.Button btnEffectMode;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnEffectModeBatch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox imgDemo;
        private System.Windows.Forms.Label lblDescription;
    }
}