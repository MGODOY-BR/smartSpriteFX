using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSprite.Effects.Filters;

namespace smartSprite.Forms.Controls.Animations.Effects
{
    public partial class EffectControl : UserControl
    {
        /// <summary>
        /// It´s the filter handled.
        /// </summary>
        private IEffectFilter _filter;

        public EffectControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the filter
        /// </summary>
        /// <param name="filter"></param>
        public void SetFilter(IEffectFilter filter)
        {
            #region Entries validation

            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            #endregion

            this._filter = filter;
            this.label1.Text = this._filter.GetIdentification().GetName();
        }
    }
}
