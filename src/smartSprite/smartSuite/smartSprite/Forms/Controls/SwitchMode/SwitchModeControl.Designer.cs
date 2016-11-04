namespace smartSprite.Forms.Controls.SwitchMode
{
    partial class SwitchModeControl
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnSwitchModel = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Black;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblVersion.ForeColor = System.Drawing.Color.Yellow;
            this.lblVersion.Location = new System.Drawing.Point(0, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(157, 23);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Version";
            // 
            // btnSwitchModel
            // 
            this.btnSwitchModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitchModel.Location = new System.Drawing.Point(143, -1);
            this.btnSwitchModel.Name = "btnSwitchModel";
            this.btnSwitchModel.Size = new System.Drawing.Size(84, 23);
            this.btnSwitchModel.TabIndex = 1;
            this.btnSwitchModel.Text = "Switch Mode";
            this.btnSwitchModel.UseVisualStyleBackColor = true;
            this.btnSwitchModel.Click += new System.EventHandler(this.btnSwitchModel_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(233, -1);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(56, 23);
            this.btnAbout.TabIndex = 2;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            // 
            // SwitchModeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnSwitchModel);
            this.Controls.Add(this.lblVersion);
            this.Name = "SwitchModeControl";
            this.Size = new System.Drawing.Size(292, 23);
            this.Load += new System.EventHandler(this.SwitchModeControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnSwitchModel;
        private System.Windows.Forms.Button btnAbout;
    }
}
