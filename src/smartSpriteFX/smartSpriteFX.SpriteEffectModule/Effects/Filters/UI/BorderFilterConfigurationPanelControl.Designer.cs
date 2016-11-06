namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    partial class BorderFilterConfigurationPanelControl
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
            this.tckWeight = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.panelColor = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.tckWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(36, 151);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 32;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tckWeight
            // 
            this.tckWeight.Location = new System.Drawing.Point(25, 40);
            this.tckWeight.Minimum = 1;
            this.tckWeight.Name = "tckWeight";
            this.tckWeight.Size = new System.Drawing.Size(166, 45);
            this.tckWeight.TabIndex = 33;
            this.tckWeight.Value = 1;
            this.tckWeight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tckWeight_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Weight";
            // 
            // panelColor
            // 
            this.panelColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelColor.Location = new System.Drawing.Point(25, 78);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(202, 67);
            this.panelColor.TabIndex = 35;
            // 
            // BorderFilterConfigurationPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tckWeight);
            this.Controls.Add(this.btnReset);
            this.Name = "BorderFilterConfigurationPanelControl";
            this.Size = new System.Drawing.Size(240, 205);
            ((System.ComponentModel.ISupportInitialize)(this.tckWeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar tckWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelColor;
    }
}
