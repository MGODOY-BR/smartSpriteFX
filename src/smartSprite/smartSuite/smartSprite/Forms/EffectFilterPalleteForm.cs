using smartSuite.smartSprite.Effects.FilterEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSprite.Forms
{
    public partial class EffectFilterPalleteForm : Form
    {
        public EffectFilterPalleteForm()
        {
            InitializeComponent();
        }

        private void EffectFilterPalleteForm_Load(object sender, EventArgs e)
        {
            // Loading the filtercollection
            FilterCollection.Load();

            // Filling the list
            var pallete = FilterCollection.GetFilterPallete();

            // Grouping filteres
            var effectByFunctionList = from palleteItem in pallete
                                       group palleteItem by palleteItem.GetIdentification().GetGroup();

            // Creating TreeView items
            foreach (var effectByFunctionGroup in effectByFunctionList)
            {
                TreeNode treeNode = new TreeNode(effectByFunctionGroup.Key);
                foreach (var effect in effectByFunctionGroup)
                {
                    var treeNodeItem = new TreeNode(effect.GetIdentification().GetName());
                    treeNodeItem.Tag = effect;
                    treeNode.Nodes.Add(treeNodeItem);
                }
                this.treeView1.Nodes.Add(treeNode);
            }
        }
    }
}
