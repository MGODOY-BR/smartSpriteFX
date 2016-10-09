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
        /// Occurs when user interact with control
        /// </summary>
        public event EventHandler<EffectControlEventArgs> UserInteracted;

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
                return;
            }

            #endregion

            this._filter = filter;
            this.label1.Text = this._filter.GetIdentification().GetName();
        }

        /// <summary>
        /// Gets the filter
        /// </summary>
        /// <returns></returns>
        public IEffectFilter GetFilter()
        {
            return this._filter;
        }

        private void btnExclude_Click(object sender, EventArgs e)
        {
            this.OnUserInteracted(EffectControlCommandEnum.EXCLUDE);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            this.OnUserInteracted(EffectControlCommandEnum.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            this.OnUserInteracted(EffectControlCommandEnum.DOW);
        }

        /// <summary>
        /// Triggers the event
        /// </summary>
        /// <param name="command"></param>
        private void OnUserInteracted(EffectControlCommandEnum command)
        {
            #region Entries validation

            if (this.UserInteracted == null)
            {
                return;
            }

            #endregion

            this.UserInteracted(this, new EffectControlEventArgs
            {
                CommandType = command,
                Control = this
            });
        }

        /// <summary>
        /// Enumates the command for EffectControl events
        /// </summary>
        /// <seealso cref="EffectControlEventArgs"/>
        public enum EffectControlCommandEnum
        {
            EXCLUDE,
            UP,
            DOW,
        }

        /// <summary>
        /// Represents the state for EffectControl events
        /// </summary>
        public class EffectControlEventArgs : EventArgs
        {
            /// <summary>
            /// It´s the command type
            /// </summary>
            public EffectControlCommandEnum CommandType { get; internal set; }

            /// <summary>
            /// It´s the EffectControl
            /// </summary>
            public EffectControl Control { get; internal set; }
        }
    }
}
