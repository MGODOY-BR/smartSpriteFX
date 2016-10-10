using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSprite.Effects.Filters;
using System.Windows.Forms;

namespace smartSprite.SpriteEffectModule.Effects.Infra.UI.Configuratons
{
    /// <summary>
    /// Offers an default configuration Panel with no implementation remarkable
    /// </summary>
    public class GenericConfigurationPanel : IConfigurationPanel
    {
        /// <summary>
        /// It´s the panel
        /// </summary>
        private Panel _panel;

        public GenericConfigurationPanel(Panel panel)
        {
            #region Entries validation

            if (panel == null)
            {
                throw new ArgumentNullException("panel");
            }

            #endregion

            _panel = panel;
        }

        public Panel GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }

            #endregion

            _panel.Tag = effectFilter;
            return this._panel;
        }
    }
}
