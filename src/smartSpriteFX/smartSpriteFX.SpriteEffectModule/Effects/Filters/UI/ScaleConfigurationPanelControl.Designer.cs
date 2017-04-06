namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    partial class ScaleConfigurationPanelControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tckScale = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tckScale)).BeginInit();
            this.SuspendLayout();
            // 
            // tckScale
            // 
            this.tckScale.LargeChange = 10;
            this.tckScale.Location = new System.Drawing.Point(3, 31);
            this.tckScale.Maximum = 25;
            this.tckScale.Minimum = -25;
            this.tckScale.Name = "tckScale";
            this.tckScale.Size = new System.Drawing.Size(284, 45);
            this.tckScale.SmallChange = 2;
            this.tckScale.TabIndex = 22;
            this.tckScale.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tckScale_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Scale";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(199, 68);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 31;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ScaleConfigurationPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tckScale);
            this.Name = "ScaleConfigurationPanelControl";
            this.Size = new System.Drawing.Size(290, 108);
            ((System.ComponentModel.ISupportInitialize)(this.tckScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tckScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReset;
    }
}
