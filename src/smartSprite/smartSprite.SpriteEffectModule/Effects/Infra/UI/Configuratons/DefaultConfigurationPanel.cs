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
    public class DefaultConfigurationPanel : IConfigurationPanel
    {
        public Panel GetPanel(IEffectFilter effectFilter)
        {
            throw new NotImplementedException();
        }
    }
}
