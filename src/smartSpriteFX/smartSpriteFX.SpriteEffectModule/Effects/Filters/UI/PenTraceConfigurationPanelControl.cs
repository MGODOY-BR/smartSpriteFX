using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Effects.Filters;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    public partial class PenTraceConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It's the pen trace
        /// </summary>
        private PenTraceFilter _innerFilter;

        public PenTraceConfigurationPanelControl()
        {
            InitializeComponent();

            this.numExtraDarknessFactor.LostFocus += (sender, eventHandler) => this._innerFilter.ExtraDarknessFactor = (int)this.numExtraDarknessFactor.Value;
            this.colorSelectionControl1.SelectedColorEvent += (sender, eventHandler) => this._innerFilter.NewTraceColor = eventHandler.SelectedColor;
        }

        private void chkChangePenColor_CheckedChanged(object sender, EventArgs e)
        {
            this.grpNewTraceColor.Enabled = chkChangePenColor.Checked;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _innerFilter.Reset();

            this.RefreshForm();
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }
            if (!(effectFilter is PenTraceFilter))
            {
                throw new NotSupportedException("This control only suports filteres which implements " + typeof(PenTraceFilter).Name);
            }

            #endregion
            _innerFilter = (PenTraceFilter)effectFilter;

            RefreshForm();

            return this;
        }

        private void RefreshForm()
        {
            this.numExtraDarknessFactor.Value = this._innerFilter.ExtraDarknessFactor;
            this.chkChangePenColor.Checked = this._innerFilter.NewTraceColor != null;
            if (this.chkChangePenColor.Checked)
            {
                this.colorSelectionControl1.SelectedColor = this._innerFilter.NewTraceColor.Value;
                this.grpNewTraceColor.Enabled = true;
            }
        }
    }
}
