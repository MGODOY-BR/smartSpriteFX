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
    public partial class ContrastConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It´s the control oriented by contrast
        /// </summary>
        private IContrastOrientedObject _filterSettable;

        public ContrastConfigurationPanelControl()
        {
            InitializeComponent();
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }
            if (!(effectFilter is IContrastOrientedObject))
            {
                throw new NotSupportedException("This control only suports filteres which implements " + typeof(IContrastOrientedObject).Name);
            }

            #endregion
            _filterSettable = (IContrastOrientedObject)effectFilter;

            RefreshForm();

            return this;
        }

        /// <summary>
        /// Refreshs the form
        /// </summary>
        private void RefreshForm()
        {
            this.tckContrast.Minimum = -50;
            this.tckContrast.Maximum = 256;
            this.tckContrast.Value = this._filterSettable.Contrast;
        }

        private void tckContrast_MouseUp(object sender, MouseEventArgs e)
        {
            this._filterSettable.Contrast = tckContrast.Value;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ((IEffectFilter)this._filterSettable).Reset();
                this.RefreshForm();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
