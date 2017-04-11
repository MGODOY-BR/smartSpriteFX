using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Forms.Utilities
{
    /// <summary>
    /// Represents a message shown when effect applied has done.
    /// </summary>
    public class EffectBatchDoneDefaultConclusionMessage : IConclusionMessage
    {
        /// <summary>
        /// It´s the parent form
        /// </summary>
        private Form _parent;
        /// <summary>
        /// It´s the message to be shown
        /// </summary>
        private String _message;

        /// <summary>
        /// Gets or sets the parent
        /// </summary>
        public Form Parent { get => _parent; set => _parent = value; }

        public EffectBatchDoneDefaultConclusionMessage(Form parent, string message)
        {
            #region Entries validation

            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            #endregion

            this._parent = parent;
            this._message = message;
        }

        public EffectBatchDoneDefaultConclusionMessage(string message)
        {
            this._message = message;
        }

        public void Show(string message)
        {
            MessageBoxUtil.Show(this._message, MessageBoxIcon.Information);
            this._parent.Close();
        }
    }
}
