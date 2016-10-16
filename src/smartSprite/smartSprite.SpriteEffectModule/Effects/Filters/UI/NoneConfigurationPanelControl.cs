using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Effects.Facade;

namespace smartSprite.SpriteEffectModule.Effects.Filters.UI
{
    public partial class NoneConfigurationPanelControl : UserControl, IConfigurationPanel
    {
        public NoneConfigurationPanelControl()
        {
            InitializeComponent();
        }

        public UserControl GetPanel(IEffectFilter effectFilter)
        {
            return this;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EffectFacade.UpdatePreviewBoard();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
