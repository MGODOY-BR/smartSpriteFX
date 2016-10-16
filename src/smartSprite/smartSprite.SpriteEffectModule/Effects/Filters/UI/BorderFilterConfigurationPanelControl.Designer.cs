namespace smartSprite.SpriteEffectModule.Effects.Filters.UI
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
            this.btnPreview = new System.Windows.Forms.Button();
            this.tckWeight = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.panelColorPreview = new System.Windows.Forms.Panel();
            this.btnBrowserColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tckWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(116, 153);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 32;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(36, 153);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 31;
            this.btnPreview.Text = "PREVIEW";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Color";
            // 
            // panelColorPreview
            // 
            this.panelColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorPreview.Location = new System.Drawing.Point(36, 104);
            this.panelColorPreview.Name = "panelColorPreview";
            this.panelColorPreview.Size = new System.Drawing.Size(25, 23);
            this.panelColorPreview.TabIndex = 36;
            // 
            // btnBrowserColor
            // 
            this.btnBrowserColor.Location = new System.Drawing.Point(67, 104);
            this.btnBrowserColor.Name = "btnBrowserColor";
            this.btnBrowserColor.Size = new System.Drawing.Size(107, 23);
            this.btnBrowserColor.TabIndex = 37;
            this.btnBrowserColor.Text = "Select color ...";
            this.btnBrowserColor.UseVisualStyleBackColor = true;
            this.btnBrowserColor.Click += new System.EventHandler(this.btnBrowserColor_Click);
            // 
            // BorderFilterConfigurationPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowserColor);
            this.Controls.Add(this.panelColorPreview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tckWeight);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPreview);
            this.Name = "BorderFilterConfigurationPanelControl";
            this.Size = new System.Drawing.Size(240, 205);
            ((System.ComponentModel.ISupportInitialize)(this.tckWeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.TrackBar tckWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelColorPreview;
        private System.Windows.Forms.Button btnBrowserColor;
    }
}
