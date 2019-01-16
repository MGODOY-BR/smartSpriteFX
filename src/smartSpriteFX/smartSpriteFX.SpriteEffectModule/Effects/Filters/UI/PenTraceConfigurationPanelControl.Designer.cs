namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    partial class PenTraceConfigurationPanelControl
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
            this.colorSelectionControl1 = new smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI.ColorSelectionControl();
            this.label1 = new System.Windows.Forms.Label();
            this.numExtraDarknessFactor = new System.Windows.Forms.NumericUpDown();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpNewTraceColor = new System.Windows.Forms.GroupBox();
            this.chkChangePenColor = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraDarknessFactor)).BeginInit();
            this.grpNewTraceColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorSelectionControl1
            // 
            this.colorSelectionControl1.Location = new System.Drawing.Point(24, 12);
            this.colorSelectionControl1.Name = "colorSelectionControl1";
            this.colorSelectionControl1.SelectedColor = System.Drawing.Color.Empty;
            this.colorSelectionControl1.Size = new System.Drawing.Size(169, 58);
            this.colorSelectionControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Extra darkness factor";
            // 
            // numExtraDarknessFactor
            // 
            this.numExtraDarknessFactor.Location = new System.Drawing.Point(134, 140);
            this.numExtraDarknessFactor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numExtraDarknessFactor.Name = "numExtraDarknessFactor";
            this.numExtraDarknessFactor.Size = new System.Drawing.Size(82, 20);
            this.numExtraDarknessFactor.TabIndex = 2;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(98, 180);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // grpNewTraceColor
            // 
            this.grpNewTraceColor.Controls.Add(this.colorSelectionControl1);
            this.grpNewTraceColor.Enabled = false;
            this.grpNewTraceColor.Location = new System.Drawing.Point(24, 32);
            this.grpNewTraceColor.Name = "grpNewTraceColor";
            this.grpNewTraceColor.Size = new System.Drawing.Size(214, 76);
            this.grpNewTraceColor.TabIndex = 4;
            this.grpNewTraceColor.TabStop = false;
            this.grpNewTraceColor.Text = "New Trace Color";
            // 
            // chkChangePenColor
            // 
            this.chkChangePenColor.AutoSize = true;
            this.chkChangePenColor.Location = new System.Drawing.Point(18, 12);
            this.chkChangePenColor.Name = "chkChangePenColor";
            this.chkChangePenColor.Size = new System.Drawing.Size(170, 17);
            this.chkChangePenColor.TabIndex = 5;
            this.chkChangePenColor.Text = "Change the original pen color?";
            this.chkChangePenColor.UseVisualStyleBackColor = true;
            this.chkChangePenColor.CheckedChanged += new System.EventHandler(this.chkChangePenColor_CheckedChanged);
            // 
            // PenTraceConfigurationPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkChangePenColor);
            this.Controls.Add(this.grpNewTraceColor);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.numExtraDarknessFactor);
            this.Controls.Add(this.label1);
            this.Name = "PenTraceConfigurationPanelControl";
            this.Size = new System.Drawing.Size(257, 236);
            ((System.ComponentModel.ISupportInitialize)(this.numExtraDarknessFactor)).EndInit();
            this.grpNewTraceColor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorSelectionControl colorSelectionControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numExtraDarknessFactor;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox grpNewTraceColor;
        private System.Windows.Forms.CheckBox chkChangePenColor;
    }
}
