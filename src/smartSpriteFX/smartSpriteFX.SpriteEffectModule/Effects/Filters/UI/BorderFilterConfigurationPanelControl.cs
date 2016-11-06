using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.Effects.Facade;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    public partial class BorderFilterConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It´s the filter controlled by this control
        /// </summary>
        private BorderFilter _filterSettable;

        /// <summary>
        /// It´s a control to select colors.
        /// </summary>
        private ColorSelectionControl _colorSelectionControl = new ColorSelectionControl();

        public BorderFilterConfigurationPanelControl()
        {
            InitializeComponent();
            this._colorSelectionControl.SelectedColorEvent += _colorSelectionControl_SelectedColorEvent;
            this.panelColor.Controls.Add(this._colorSelectionControl);
        }

        private void _colorSelectionControl_SelectedColorEvent(object sender, ColorSelectionControl.SelectionColorEventArgs e)
        {
            this._filterSettable.BorderColor = e.SelectedColor;
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

        /// <summary>
        /// Refreshs the form
        /// </summary>
        private void RefreshForm()
        {
            tckWeight.Value = (int)this._filterSettable.TraceBorderWidth;
            this._colorSelectionControl.SelectedColor = this._filterSettable.BorderColor;
        }

        private void tckWeight_MouseUp(object sender, MouseEventArgs e)
        {
            this._filterSettable.TraceBorderWidth = tckWeight.Value;
        }
    }
}
