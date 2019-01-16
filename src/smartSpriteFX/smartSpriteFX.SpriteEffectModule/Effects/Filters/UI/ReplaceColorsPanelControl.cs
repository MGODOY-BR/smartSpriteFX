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


        public ReplaceColorsPanelControl()
        {
            InitializeComponent();

            throw new NotImplementedException();
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            return this;
        }
    }
}
