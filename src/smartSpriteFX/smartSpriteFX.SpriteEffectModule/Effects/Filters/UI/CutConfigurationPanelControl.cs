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
using smartSuite.smartSpriteFX.Effects.Facade;
using smartSuite.smartSpriteFX.Forms.Controls;
using smartSuite.smartSpriteFX.Effects.Core;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    public partial class CutConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// It´s the filter controlled by this control
        /// </summary>
        private CutFilter _filterSettable;

        /// <summary>
        /// It´s the hook A
        /// </summary>
        private HookControl _hookControlA = new HookControl();
        /// <summary>
        /// It´s the hook B
        /// </summary>
        private HookControl _hookControlB = new HookControl();

        public CutConfigurationPanelControl()
        {
            InitializeComponent();

            this.txtA_X.KeyPress += this.JustNumberEvent;
            this.txtA_Y.KeyPress += this.JustNumberEvent;
            this.txtB_X.KeyPress += this.JustNumberEvent;
            this.txtB_Y.KeyPress += this.JustNumberEvent;

            this.txtA_X.LostFocus += TextNumber_RefreshHook;
            this.txtA_Y.LostFocus += TextNumber_RefreshHook;
            this.txtB_X.LostFocus += TextNumber_RefreshHook;
            this.txtB_Y.LostFocus += TextNumber_RefreshHook;
        }

        UserControl IConfigurationPanel.GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }
            if (!(effectFilter is CutFilter))
            {
                throw new ArgumentNullException("Filter not supported");
            }

            #endregion

            this._filterSettable = (CutFilter)effectFilter;
            this.RefreshBoard();
            return this;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this._filterSettable.Reset();
            this.RefreshBoard();
        }

        /// <summary>
        /// Refreshes the board
        /// </summary>
        private void RefreshBoard()
        {
            _hookControlA.Name = "A";
            _hookControlB.Name = "B";

            _hookControlA.PositionChanged += HookControl_PositionChanged;
            _hookControlB.PositionChanged += HookControl_PositionChanged;

            _hookControlA.Pair = _hookControlB;
            _hookControlB.Pair = _hookControlA;

            var controlList = EffectFacade.GetControlCollectionFromPreviewBoard();
            controlList.Clear(); 
            controlList.Add(_hookControlA);
            controlList.Add(_hookControlB);

            if (this._filterSettable.PointA == null)
            {
                var previewBoard = EffectEngine.GetPreviewBoard();
                float margin = previewBoard.Width / 10;
                if (margin > previewBoard.Height)
                {
                    margin = previewBoard.Height / 10;
                }
                float maxWidth = Math.Abs(previewBoard.Width - margin);
                float maxHeight = Math.Abs(previewBoard.Height - margin);

                this._filterSettable.SetPoint(
                    new Pictures.Point(margin, margin),
                    new Pictures.Point(maxWidth, maxHeight));
            }

            _hookControlA.Point = this._filterSettable.PointA;
            _hookControlB.Point = this._filterSettable.PointB;

            this.UpdateTextNumber();

            _hookControlA.CreateLines();
        }

        private void HookControl_PositionChanged(object sender, smartSpriteFX.Forms.Controls.HookState.HookEventArgs e)
        {
            this._filterSettable.SetPoint(
                this._hookControlA.Point,
                this._hookControlB.Point);

            UpdateTextNumber();
        }

        private void TextNumber_RefreshHook(object sender, EventArgs e)
        {
            this._hookControlA.Point = new Pictures.Point(float.Parse(txtA_X.Text), float.Parse(txtA_Y.Text));
            this._hookControlB.Point = new Pictures.Point(float.Parse(txtB_X.Text), float.Parse(txtB_Y.Text));

            this._hookControlA.RefreshLines();
        }

        /// <summary>
        /// Updates text numbers
        /// </summary>
        private void UpdateTextNumber()
        {
            txtA_X.Text = this._filterSettable.PointA.X.ToString();
            txtA_Y.Text = this._filterSettable.PointA.Y.ToString();

            txtB_X.Text = this._filterSettable.PointB.X.ToString();
            txtB_Y.Text = this._filterSettable.PointB.Y.ToString();
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
