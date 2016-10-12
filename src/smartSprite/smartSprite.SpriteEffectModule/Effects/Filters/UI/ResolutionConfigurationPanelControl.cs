using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Effects.Filters;

namespace smartSprite.SpriteEffectModule.Effects.Filters.UI
{
    public partial class ResolutionConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It´s the filter controlled by this control
        /// </summary>
        private IResolutionFilterSetter _filterSettable;

        public ResolutionConfigurationPanelControl()
        {
            InitializeComponent();

            txtMaxColorAmount.KeyPress += JustNumberEvent;
            txtMaxScreenWidth.KeyPress += JustNumberEvent;
            txtMaxScreenHeight.KeyPress += JustNumberEvent;
            txtScreenWidth.KeyPress += JustNumberEvent;
            txtScreenHeight.KeyPress += JustNumberEvent;
        }

        UserControl IConfigurationPanel.GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }
            if (!(effectFilter is IResolutionFilterSetter))
            {
                throw new NotSupportedException("This control only suports filteres which implements " + typeof(IResolutionFilterSetter).Name);
            }

            #endregion
            _filterSettable = (IResolutionFilterSetter)effectFilter;

            this.Tag = effectFilter;
            this.BackColor = System.Drawing.Color.Silver;

            return this;
        }

        /// <summary>
        /// Allows just number to be entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JustNumberEvent(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
