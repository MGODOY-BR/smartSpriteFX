﻿using System;
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

namespace smartSpriteFX.Forms.Controls.EffectFilterPallete
{
    /// <summary>
    /// Represents an UI interface where user selects the filteres
    /// </summary>
    public partial class EffectFilterPalleteControl : UserControl
    {
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
    }
}
