namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    partial class ContrastConfigurationPanelControl
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
            this.btnReset = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tckContrast = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tckContrast)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(106, 41);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 33;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Contrast";
            // 
            // tckContrast
            // 
            this.tckContrast.LargeChange = 10;
            this.tckContrast.Location = new System.Drawing.Point(65, 3);
            this.tckContrast.Maximum = 25;
            this.tckContrast.Minimum = -25;
            this.tckContrast.Name = "tckContrast";
            this.tckContrast.Size = new System.Drawing.Size(116, 45);
            this.tckContrast.SmallChange = 2;
            this.tckContrast.TabIndex = 32;
            this.tckContrast.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tckContrast_MouseUp);
            // 
            // ContrastConfigurationPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tckContrast);
            this.Name = "ContrastConfigurationPanelControl";
            this.Size = new System.Drawing.Size(188, 81);
            ((System.ComponentModel.ISupportInitialize)(this.tckContrast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tckContrast;
    }
}
