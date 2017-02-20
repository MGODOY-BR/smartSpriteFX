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
    public partial class ShadowConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// É o filtro da sombra
        /// </summary>
        private ShadowFilter _filterSettable;

        public ShadowConfigurationPanelControl()
        {
            InitializeComponent();

            this.trkStrenght.ValueChanged += TrkStrenght_ValueChanged;
        }

        private void TrkStrenght_ValueChanged(object sender, EventArgs e)
        {
            #region Entries validation

            if (this._filterSettable == null)
            {
                throw new ArgumentNullException("this._filterSettable");
            }

            #endregion

            this._filterSettable.SunStrenght =
                Convert.ToInt32(
                    (float)this.trkStrenght.Value / (float)this.trkStrenght.Maximum * 100);
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }
            if (!(effectFilter is ShadowFilter))
            {
                throw new NotSupportedException("This control only supports " + typeof(ShadowFilter).Name);
            }

            #endregion
            _filterSettable = (ShadowFilter)effectFilter;

            this.Tag = effectFilter;

            RefreshForm();

            return this;
        }

        /// <summary>
        /// Refreshes the form field
        /// </summary>
        private void RefreshForm()
        {
            this.trkStrenght.Minimum = 20;
            this.trkStrenght.Maximum = 200;

            if (this._filterSettable == null)
            {
                this.trkStrenght.Value = 20;
            }
            else
            {
                this.trkStrenght.Value = this._filterSettable.SunStrenght;
            }
        }
    }
}
