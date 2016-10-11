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
        private UserControl _userControl;

        public GenericConfigurationPanel(UserControl userControl)
        {
            #region Entries validation

            if (userControl == null)
            {
                throw new ArgumentNullException("userControl");
            }

            #endregion

            _userControl = userControl;
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            #region Entries validation

            if (effectFilter == null)
            {
                throw new ArgumentNullException("effectFilter");
            }

            #endregion

            _userControl.Tag = effectFilter;
            _userControl.BackColor = System.Drawing.Color.Silver;
            return this._userControl;
        }
    }
}
