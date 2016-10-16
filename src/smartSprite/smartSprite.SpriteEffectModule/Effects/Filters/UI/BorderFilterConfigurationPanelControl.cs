using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSprite.Effects.Facade;
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;

namespace smartSprite.SpriteEffectModule.Effects.Filters.UI
{
    public partial class BorderFilterConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It´s the filter controlled by this control
        /// </summary>
        private BorderFilter _filterSettable;

        public BorderFilterConfigurationPanelControl()
        {
            InitializeComponent();
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (!(effectFilter is BorderFilter))
            {
                throw new NotSupportedException("This control requires " + typeof(BorderFilter).Name);
            }

            #endregion

            this._filterSettable = (BorderFilter)effectFilter;
            this.RefreshForm();

            return this;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EffectFacade.UpdatePreviewBoard();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ((IEffectFilter)this._filterSettable).Reset();
                this.RefreshForm();
                EffectFacade.UpdatePreviewBoard();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Refreshs the form
        /// </summary>
        private void RefreshForm()
        {
            tckWeight.Value = (int)this._filterSettable.TraceBorderWidth;
            colorDialog1.Color = this._filterSettable.BorderColor;
            panelColorPreview.BackColor = this._filterSettable.BorderColor;
        }

        private void tckWeight_MouseUp(object sender, MouseEventArgs e)
        {
            this._filterSettable.TraceBorderWidth = tckWeight.Value;
        }

        private void btnBrowserColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this._filterSettable.BorderColor = colorDialog1.Color;
                this.panelColorPreview.BackColor = colorDialog1.Color;
            }
        }
    }
}
