using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.FilterEngine;
using smartSuite.smartSpriteFX.Forms.Utilities;

namespace smartSuite.smartSpriteFX.Forms.Controls.EffectFilterPallete
{
    /// <summary>
    /// Represents an UI interface where user selects the filteres
    /// </summary>
    public partial class EffectFilterPalleteControl : UserControl
    {
        /// <summary>
        /// It´s a dataSource of researchs
        /// </summary>
        private KeyValuePair<String, IEnumerable<TreeNode>>? _searchDataSource;
        /// <summary>
        /// It´s the current treenode
        /// </summary>
        private TreeNode _currentTreeNode;

        public EffectFilterPalleteControl()
        {
            InitializeComponent();

            this.treeView1.DoubleClick += TreeView1_DoubleClick;
        }

        /// <summary>
        /// Occurs when the selection of some filter is seleted by user.
        /// </summary>
        public event EventHandler<SelectionFilterEventArgs> SelectedFilterEvent;

        /// <summary>
        /// Occurs then the user selects the filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            #region Entries validation

            if (this.SelectedFilterEvent == null)
            {
                return;
            }
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            if (this.treeView1.SelectedNode.Tag == null)
            {
                return;
            }

            #endregion

                this.SelectedFilterEvent(
                this.treeView1,
                new SelectionFilterEventArgs
                {
                    Filter = (IEffectFilter)this.treeView1.SelectedNode.Tag
                });
        }

        /// <summary>
        /// Represents states to events of selection of filters
        /// </summary>
        public class SelectionFilterEventArgs : EventArgs
        {
            /// <summary>
            /// It´s the selected filter
            /// </summary>
            public IEffectFilter Filter { get; internal set; }
        }

        private void EffectFilterPalleteControl_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Fills the control
        /// </summary>
        public void Bind()
        {
            // Filling the list
            var pallete = FilterCollection.GetFilterPallete();

            // Grouping filteres
            var effectByFunctionList = from palleteItem in pallete
                                       group palleteItem by palleteItem.GetIdentification().GetGroup();

            this.treeView1.Nodes.Clear();

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

        /// <summary>
        /// Performs a find operation
        /// </summary>
        private void DoFind()
        {
            String criteria = this.txtFind.Text;
            if (String.IsNullOrWhiteSpace(criteria))
            {
                return;
            }
            if (this._searchDataSource == null || this._searchDataSource.Value.Key != criteria)
            {
                this._searchDataSource = 
                    new KeyValuePair<string, IEnumerable<TreeNode>>(
                        criteria, 
                        this.FindTreeNode(criteria));
            }

            if (this.treeView1.SelectedNode != null)
            {
                this._currentTreeNode = this.treeView1.SelectedNode;
            }

            IEnumerable<TreeNode> treeViewList = this._searchDataSource.Value.Value;
            foreach (var treeViewItem in treeViewList)
            {
                if (treeViewItem == this._currentTreeNode)
                {
                    continue;
                }
                if (treeViewItem.Parent != null)
                {
                    treeViewItem.Parent.Expand();
                }
                treeViewItem.Checked = true;
                this._currentTreeNode = treeViewItem;
                break;
            }
        }

        /// <summary>
        /// Gets a list of nodes for the criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private IEnumerable<TreeNode> FindTreeNode(string criteria)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(criteria))
            {
                throw new ArgumentNullException("criteria");
            }

            #endregion

            var treeViewNodeList = TreeViewUtil.GetAllTreeNodes(this.treeView1.Nodes);
            var treeViewList = from treeViewItem in treeViewNodeList
                               where treeViewItem.Text.IndexOf(criteria, StringComparison.CurrentCultureIgnoreCase) > -1
                               select treeViewItem;
            return treeViewList;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DoFind();
        }

        private void txtFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.DoFind();
            }
        }
    }
}
