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
using smartSuite.smartSpriteFX.Forms.Controls;
using smartSuite.smartSpriteFX.Effects.Facade;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Forms.Controls.HookState;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    public partial class MarginPanelControl : UserControl, IConfigurationPanel
    {
        /// <summary>
        /// Gets or sets de respective margin
        /// </summary>
        public int LeftMargin { get; set; }
        /// <summary>
        /// Gets or sets de respective margin
        /// </summary>
        public int RightMargin { get; set; }
        /// <summary>
        /// Gets or sets de respective margin
        /// </summary>
        public int TopMargin { get; set; }
        /// <summary>
        /// Gets or sets de respective margin
        /// </summary>
        public int BottomMargin { get; set; }

        /// <summary>
        /// It's the main hook
        /// </summary>
        private HookControl _mainHook = new HookControl();

        /// <summary>
        /// It's the main hook
        /// </summary>
        private HookControl _secondHook = new HookControl();

        /// <summary>
        /// It's the effect filter based on margin
        /// </summary>
        private IMarginEnabledEffect _marginEnabledEffect;

        public MarginPanelControl()
        {
            InitializeComponent();
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            this._marginEnabledEffect = (IMarginEnabledEffect)effectFilter;
            this.LoadHooks(this._marginEnabledEffect);
            this.RefreshForm();
            return this;
        }

        /// <summary>
        /// Loads hooks
        /// </summary>
        /// <param name="marginEnabledEffect"></param>
        private void LoadHooks(IMarginEnabledEffect marginEnabledEffect)
        {
            var previewBoard = EffectEngine.GetPreviewBoard();
            this._mainHook.Pair = this._secondHook;
            // TODO: Get it from effect
            this._mainHook.Top = 0;
            this._mainHook.Left = 0;

            this._secondHook.Pair = this._mainHook;
            this._secondHook.Left = previewBoard.Width - this._secondHook.Width;
            this._secondHook.Top = previewBoard.Height - this._secondHook.Height;

            previewBoard.Controls.Add(this._mainHook);
            previewBoard.Controls.Add(this._secondHook);

            this._mainHook.PositionChanged += delegate (object sender, HookEventArgs e)
            {
                this.LeftMargin = (int)e.MainHook.Point.X;
                this.TopMargin = (int)e.MainHook.Point.Y;

                this.LeftMargin = (this.LeftMargin < 0) ? 0 : this.LeftMargin;
                this.TopMargin = (this.TopMargin < 0) ? 0 : this.TopMargin;

                this.RefreshForm();
            };
            this._secondHook.PositionChanged += delegate (object sender, HookEventArgs e)
            {
                var frame =
                    EffectEngine.GetPreviewBoard().Image;

                this.RightMargin = frame.Width - (int)e.MainHook.Pair.Point.X;
                this.BottomMargin = frame.Height - (int)e.MainHook.Pair.Point.Y;

                this.RightMargin = (this.LeftMargin < 0) ? 0 : this.RightMargin;
                this.BottomMargin = (this.TopMargin < 0) ? 0 : this.BottomMargin;

                #region Applying margins in filters

                if (marginEnabledEffect is IBottomMarginEffectFilter)
                {
                    IBottomMarginEffectFilter bottomMarginEffectFilter =
                        (IBottomMarginEffectFilter)marginEnabledEffect;

                    bottomMarginEffectFilter.BottomMargin = this.BottomMargin;
                }

                #endregion

                this.RefreshForm();
            };

            this._mainHook.CreateLines();

            #region Applying constaints

            if(marginEnabledEffect is IOnlyBottomMarginEffectFilter)
            {
                this._mainHook.Visible = false;
                this._mainHook.Pair.HorizontalMovement = false;
            }

            #endregion
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this._marginEnabledEffect.Reset();
            this.RefreshForm();
        }

        /// <summary>
        /// Refreshes the form
        /// </summary>
        private void RefreshForm()
        {
            if(this._marginEnabledEffect is IBottomMarginEffectFilter)
            {
                IBottomMarginEffectFilter bottomFilter = (IBottomMarginEffectFilter)this._marginEnabledEffect;
                this.BottomMargin = bottomFilter.BottomMargin;

                this._secondHook.Top = EffectEngine.GetPreviewBoard().Image.Height - this.BottomMargin - this._secondHook.Height;
                this._secondHook.RefreshLines();
            }

            this.label1.Text =
                String.Format(
                    "Left={0} Top={1} Right={2} Bottom={3}",
                    this.LeftMargin,
                    this.TopMargin,
                    this.RightMargin,
                    this.BottomMargin);
        }
    }
}
