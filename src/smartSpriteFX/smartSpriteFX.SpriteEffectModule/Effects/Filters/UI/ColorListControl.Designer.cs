namespace smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    partial class ColorListControl
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
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.panelList = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelBrowser
            // 
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBrowser.Location = new System.Drawing.Point(0, 0);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(263, 65);
            this.panelBrowser.TabIndex = 0;
            // 
            // panelList
            // 
            this.panelList.AutoScroll = true;
            this.panelList.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelList.Location = new System.Drawing.Point(0, 65);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(263, 127);
            this.panelList.TabIndex = 1;
            // 
            // ColorListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelBrowser);
            this.Name = "ColorListControl";
            this.Size = new System.Drawing.Size(263, 192);
            this.Load += new System.EventHandler(this.ColorListControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBrowser;
        private System.Windows.Forms.Panel panelList;
    }
}
