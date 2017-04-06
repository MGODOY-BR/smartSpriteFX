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
    public partial class ScaleConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It´s the filter which is been settled down
        /// </summary>
        private IScaleOrientedObject _filterSettable;

        /// <summary>
        /// Gets or sets the maximum scale allowed
        /// </summary>
        public float MaxScaleAllowed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the minimum scale allowed
        /// </summary>
        public float MinScaleAllowed
        {
            get;
            private set;
        }

        public ScaleConfigurationPanelControl()
        {
            InitializeComponent();
        }

        public ScaleConfigurationPanelControl(float minScaleAllowed, float maxScaleAllowed)
        {
            #region Entries validation

            if(maxScaleAllowed <= minScaleAllowed)
            {
                throw new ArgumentOutOfRangeException("Invalid range of scale");
            }
            if(minScaleAllowed == 0)
            {
                throw new ArgumentOutOfRangeException("minScaleAllowed", "maxScaleAllowed needs to be greater than 0");
            }

            #endregion

            InitializeComponent();

            this.MinScaleAllowed = minScaleAllowed;
            this.MaxScaleAllowed = maxScaleAllowed;
        }

        UserControl IConfigurationPanel.GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }
            if (!(effectFilter is IScaleOrientedObject))
            {
                throw new NotSupportedException("This control only suports filteres which implements " + typeof(IScaleOrientedObject).Name);
            }

            #endregion
            _filterSettable = (IScaleOrientedObject)effectFilter;

            this.Tag = effectFilter;
            this.BackColor = System.Drawing.Color.Silver;

            RefreshForm();

            return this;
        }

        /// <summary>
        /// Refreshs the form
        /// </summary>
        private void RefreshForm()
        {
            #region Entries validation

            if (this._filterSettable == null)
            {
                throw new ArgumentNullException("this._filterSettable");
            }

            #endregion

            this.tckScale.Minimum = Convert.ToInt32(this.MinScaleAllowed / 0.10);
            this.tckScale.Maximum = Convert.ToInt32(this.MaxScaleAllowed / 0.10);
            this.tckScale.SmallChange = 1;
            this.tckScale.LargeChange = 1;

            SetScaleControl();
        }

        /// <summary>
        /// Sets the scale control
        /// </summary>
        private void SetScaleControl()
        {
            var scale = this._filterSettable.Scale / 0.10;
            this.tckScale.Value = Convert.ToInt32(scale);
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

        private void tckScale_MouseUp(object sender, MouseEventArgs e)
        {
            var scale = this.tckScale.Value * 0.10F;
            this._filterSettable.Scale = scale;
        }
    }
}
