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
    public partial class ReplaceColorsPanelControl : UserControl, IConfigurationPanel
    {
        private ColorListControl _sourceColor = new ColorListControl();
        private ColorSelectionControl _toColor = new ColorSelectionControl();
        private ReplaceColorFilter _replaceColorFilter;

        public ReplaceColorsPanelControl()
        {
            InitializeComponent();

            this._sourceColor.Dock = DockStyle.Fill;
            this._toColor.Dock = DockStyle.Fill;

            this._sourceColor.ColorListChanged += _sourceColor_ColorListChanged;
            this._toColor.SelectedColorEvent += _toColor_SelectedColorEvent;

            this.grpFindFor.Controls.Add(this._sourceColor);
            this.grpReplaceFor.Controls.Add(this._toColor);
        }

        private void _sourceColor_ColorListChanged(object sender, ColorListControl.ColorListChangeEventArgs e)
        {
            #region Entries validation

            if (this._replaceColorFilter == null) return;

            #endregion

            this._replaceColorFilter.FromColorList = e.CurrentColorList;
        }

        private void _toColor_SelectedColorEvent(object sender, ColorSelectionControl.SelectionColorEventArgs e)
        {
            #region Entries validation

            if (this._replaceColorFilter == null) return;

            #endregion

            this._sourceColor.CancelDropper();
            this._replaceColorFilter.NewColor = e.SelectedColor;
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }
            if (!(effectFilter is ReplaceColorFilter))
            {
                throw new NotSupportedException("This control only supports " + typeof(ReplaceColorFilter).Name);
            }

            #endregion
            _replaceColorFilter = (ReplaceColorFilter)effectFilter;

            this.Tag = effectFilter;

            RefreshForm();

            return this;
        }

        /// <summary>
        /// Refreshes the form field
        /// </summary>
        private void RefreshForm()
        {
            if (this._replaceColorFilter == null) return;
            this._sourceColor.ColorList = this._replaceColorFilter.FromColorList;
            this._toColor.SelectedColor = this._replaceColorFilter.NewColor;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (this._replaceColorFilter == null) return;

            this._replaceColorFilter.Reset();
            this.RefreshForm();
        }
    }
}
