namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    partial class ReplaceColorsPanelControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpFindFor = new System.Windows.Forms.GroupBox();
            this.grpReplaceFor = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 302);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 54);
            this.panel1.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(152, 15);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // grpFindFor
            // 
            this.grpFindFor.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFindFor.Location = new System.Drawing.Point(0, 0);
            this.grpFindFor.Name = "grpFindFor";
            this.grpFindFor.Size = new System.Drawing.Size(397, 205);
            this.grpFindFor.TabIndex = 1;
            this.grpFindFor.TabStop = false;
            this.grpFindFor.Text = "Find for...";
            // 
            // grpReplaceFor
            // 
            this.grpReplaceFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReplaceFor.Location = new System.Drawing.Point(0, 205);
            this.grpReplaceFor.Name = "grpReplaceFor";
            this.grpReplaceFor.Size = new System.Drawing.Size(397, 97);
            this.grpReplaceFor.TabIndex = 2;
            this.grpReplaceFor.TabStop = false;
            this.grpReplaceFor.Text = "Replace for...";
            // 
            // ReplaceColorsPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpReplaceFor);
            this.Controls.Add(this.grpFindFor);
            this.Controls.Add(this.panel1);
            this.Name = "ReplaceColorsPanelControl";
            this.Size = new System.Drawing.Size(397, 356);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox grpFindFor;
        private System.Windows.Forms.GroupBox grpReplaceFor;
    }
}
