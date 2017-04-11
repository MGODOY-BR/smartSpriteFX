using smartSuite.smartSpriteFX.Effects.Core;
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
    public class EffectDoneDefaultConclusionMessage : IConclusionMessage
    {
        /// <summary>
        /// It´s the parent form
        /// </summary>
        private Form _parent; 

        public EffectDoneDefaultConclusionMessage(Form parent)
        {
            #region Entries validation

            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            #endregion

            this._parent = parent;
        }

        public void Show(string message)
        {
            string path = EffectEngine.GetOutputPath();
            PathAlertForm pathAlertForm = new PathAlertForm();
            pathAlertForm.Open("Concluded!", path);
            this._parent.Close();
        }
    }
}
