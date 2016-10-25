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
using smartSuite.smartSprite.Effects.Facade;

namespace smartSprite.SpriteEffectModule.Effects.Filters.UI
{
    public partial class ResolutionConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It´s the filter controlled by this control
        /// </summary>
        private IResolutionFilterSetter _filterSettable;

        /// <summary>
        /// It´s a list for selection of avoid colors
        /// </summary>
        private ColorListControl _avoidColorListControl = new ColorListControl();

        public ResolutionConfigurationPanelControl()
        {
            InitializeComponent();

            txtMaxColorAmount.KeyPress += JustNumberEvent;
            txtMaxScreenWidth.KeyPress += JustNumberEvent;
            txtMaxScreenHeight.KeyPress += JustNumberEvent;
            txtScreenWidth.KeyPress += JustNumberEvent;
            txtScreenHeight.KeyPress += JustNumberEvent;

            _avoidColorListControl.Dock = DockStyle.Fill;
            _avoidColorListControl.ColorListChanged += _avoidColorListControl_ColorListChanged;
            panelAvoidColors.Controls.Add(_avoidColorListControl);
        }

        /// <summary>
        /// Occurs then avoided color list has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _avoidColorListControl_ColorListChanged(object sender, ColorListControl.ColorListChangeEventArgs e)
        {
            _filterSettable.AvoidColorList = e.CurrentColorList;
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

            RefreshForm();

            return this;
        }

        /// <summary>
        /// Refreshs the form
        /// </summary>
        private void RefreshForm()
        {
            this.txtMaxColorAmount.Text = this._filterSettable.ColorBufferAmount.ToString();
            this.txtMaxScreenHeight.Text = this._filterSettable.DestinationScreenHeight.ToString();
            this.txtMaxScreenWidth.Text = this._filterSettable.DestinationScreenWidth.ToString();
            this.txtScreenHeight.Text = this._filterSettable.TotalScreenHeight.ToString();
            this.txtScreenWidth.Text = this._filterSettable.TotalScreenWidth.ToString();

            var percentageContrast = (int)(this._filterSettable.Contrast * 100);
            this.tckContrast.Minimum = percentageContrast;
            if (percentageContrast < 0)
            {
                this.tckContrast.Minimum = (int)(this._filterSettable.Contrast * 100) * 3;
                this.tckContrast.Maximum = Math.Abs((int)(this._filterSettable.Contrast * 200));
            }
            else
            {
                this.tckContrast.Minimum = (int)(this._filterSettable.Contrast * 100) / 3;
                this.tckContrast.Maximum = Math.Abs((int)(this._filterSettable.Contrast * 3 * 100));
            }
            this.tckContrast.Value = percentageContrast;
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

        private void tckContrast_MouseUp(object sender, MouseEventArgs e)
        {
            this._filterSettable.Contrast = (float)tckContrast.Value / 100f;
        }

        private void txtMaxColorAmount_Leave(object sender, EventArgs e)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                throw new ArgumentNullException("The value can´t be empty");
            }

            #endregion

            this._filterSettable.ColorBufferAmount = int.Parse(((TextBox)sender).Text);
        }

        private void txtMaxScreenWidth_Leave(object sender, EventArgs e)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                throw new ArgumentNullException("The value can´t be empty");
            }

            #endregion

            this._filterSettable.DestinationScreenWidth = int.Parse(((TextBox)sender).Text);
        }

        private void txtMaxScreenHeight_Leave(object sender, EventArgs e)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                throw new ArgumentNullException("The value can´t be empty");
            }

            #endregion

            this._filterSettable.DestinationScreenHeight = int.Parse(((TextBox)sender).Text);
        }

        private void txtScreenWidth_Leave(object sender, EventArgs e)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                throw new ArgumentNullException("The value can´t be empty");
            }

            #endregion

            this._filterSettable.TotalScreenWidth = int.Parse(((TextBox)sender).Text);
        }

        private void txtScreenHeight_Leave(object sender, EventArgs e)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                throw new ArgumentNullException("The value can´t be empty");
            }

            #endregion

            this._filterSettable.TotalScreenHeight = int.Parse(((TextBox)sender).Text);
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
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
