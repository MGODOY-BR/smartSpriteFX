namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    partial class ColorSelectionControl
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
            this.btnBrowserColor = new System.Windows.Forms.Button();
            this.panelColorPreview = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnDropper = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBrowserColor
            // 
            this.btnBrowserColor.Location = new System.Drawing.Point(43, 24);
            this.btnBrowserColor.Name = "btnBrowserColor";
            this.btnBrowserColor.Size = new System.Drawing.Size(107, 23);
            this.btnBrowserColor.TabIndex = 40;
            this.btnBrowserColor.Text = "Select color ...";
            this.btnBrowserColor.UseVisualStyleBackColor = true;
            this.btnBrowserColor.Click += new System.EventHandler(this.btnBrowserColor_Click);
            // 
            // panelColorPreview
            // 
            this.panelColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorPreview.Location = new System.Drawing.Point(12, 24);
            this.panelColorPreview.Name = "panelColorPreview";
            this.panelColorPreview.Size = new System.Drawing.Size(25, 23);
            this.panelColorPreview.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Color";
            // 
            // btnDropper
            // 
            this.btnDropper.Image = global::smartSuite.smartSpriteFX.SpriteEffectModule.Properties.Resources.get_color_png;
            this.btnDropper.Location = new System.Drawing.Point(153, 24);
            this.btnDropper.Name = "btnDropper";
            this.btnDropper.Size = new System.Drawing.Size(26, 23);
            this.btnDropper.TabIndex = 41;
            this.btnDropper.UseVisualStyleBackColor = true;
            this.btnDropper.Click += new System.EventHandler(this.btnDropper_Click);
            // 
            // ColorSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDropper);
            this.Controls.Add(this.btnBrowserColor);
            this.Controls.Add(this.panelColorPreview);
            this.Controls.Add(this.label2);
            this.Name = "ColorSelectionControl";
            this.Size = new System.Drawing.Size(216, 58);
            this.Load += new System.EventHandler(this.ColorSelectionControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowserColor;
        private System.Windows.Forms.Panel panelColorPreview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnDropper;
    }
}
