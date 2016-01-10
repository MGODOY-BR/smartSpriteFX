﻿using smartSprite.Forms.Controls.TreeViewState;
using smartSprite.Properties;
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSprite.Forms
{
    public partial class PrincipalForm : Form
    {
        /// <summary>
        /// Contains just the relevant treenode
        /// </summary>
        private List<TreeNode> _dataTreeNodeList = new List<TreeNode>();

        public PrincipalForm()
        {
            InitializeComponent();

            this.draftControl1.PieceSetChanged += DraftControl1_PieceSetChanged;
            this.treeView1.AfterSelect += TreeView1_AfterSelect;
            this.KeyDown += PrincipalForm_KeyDown;
        }

        private void PrincipalForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.toolHookButton.Checked = false;
                    this.RefreshHookCreationMode();
                    break;
            }
        }

        /// <summary>
        /// Get a list informed, except for the smartMaps files.
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        private string[] GetExceptSmartMap(string[] fileList)
        {
            #region Entries validation

            if (fileList == null)
            {
                throw new ArgumentNullException("fileList");
            }

            #endregion

            List<String> fileListTemp = new List<string>(fileList);
            return fileListTemp.FindAll(delegate (String item)
            {
                return !item.Contains(".smartMap.");
            }).ToArray();
        }

        /// <summary>
        /// Shows the runnnig result
        /// </summary>
        private void ShowRunningResult(bool visibility)
        {
        }

        /// <summary>
        /// Gets the list of selected nodes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private String[] GetSelectedNodesString(TreeNodeCollection nodes)
        {
            var selectedNodeList = this.GetSelectedNodes(nodes);

            List<String> retunedList = new List<string>();

            foreach (var item in selectedNodeList)
            {
                retunedList.Add((String)item.Tag);
            }

            return retunedList.ToArray();
        }

        /// <summary>
        /// Gets the list of selected nodes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<TreeNode> GetSelectedNodes(TreeNodeCollection nodes)
        {
            #region Entries validation

            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }

            #endregion

            List<TreeNode> returnValueList = new List<TreeNode>();

            foreach (var item in nodes)
            {
                TreeNode treeNode = (TreeNode)item;

                #region Entries validation

                if (treeNode.Nodes.Count > 0)
                {
                    returnValueList.AddRange(
                        this.GetSelectedNodes(treeNode.Nodes));

                    continue;
                }

                #endregion

                if (treeNode.Checked)
                {
                    returnValueList.Add(treeNode);
                }
            }

            return returnValueList;
        }

        /// <summary>
        /// Loads settings
        /// </summary>
        private void LoadSettings()
        {
            if (!String.IsNullOrEmpty(Settings.Default.lastSourceFolder))
            {
                this.txtDraftSourceFolder.Text = Settings.Default.lastSourceFolder;
                this.openDraftFileDialog1.FileName = this.txtDraftSourceFolder.Text.Trim();
            }
            else
            {
                this.txtDraftSourceFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }

            this.lblVersion.Text = this.GetType().Assembly.GetName().Version.ToString() + "(Alpha)";

            this.RefreshForm();
        }

        /// <summary>
        /// Refreshes the form
        /// </summary>
        private void RefreshForm()
        {
            if (!Directory.Exists(this.txtDraftSourceFolder.Text))
            {
                return;
            }

            try
            {
                this.Interrupt();

                this.openDraftFileDialog1.FileName = this.txtDraftSourceFolder.Text.Trim();
                Settings.Default.lastSourceFolder = this.txtDraftSourceFolder.Text.Trim();
                Settings.Default.Save();
            }
            finally
            {
                this.Resume();
            }
        }

        /// <summary>
        /// Interrupts the user interface
        /// </summary>
        private void Interrupt()
        {
            this.EnableForm(false);
        }

        /// <summary>
        /// Resumes the user interface
        /// </summary>
        private void Resume()
        {
            this.EnableForm(true);
        }

        /// <summary>
        /// Enables / disables form
        /// </summary>
        /// <param name="enabled"></param>
        private void EnableForm(bool enabled)
        {
            if (enabled)
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
            }

            this.pnlSourceFolder.Enabled = enabled;
        }

        /// <summary>
        /// Opens the open source dialog
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="openDialog"></param>
        private void DoOpenSourceDialog(TextBox sourceFolder, OpenFileDialog openDialog)
        {
            openDialog.ShowDialog();
            if (openDialog.FileName != null)
            {
                sourceFolder.Text = openDialog.FileName;
                this.RefreshForm();
            }
        }

        /// <summary>
        /// Convert a piece to a TreeNode object
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private TreeNode ConvertToTreeNode(Piece piece)
        {
            #region Entries validation
            
            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }

            #endregion

            TreeNode treeNode = new TreeNode(piece.Name);
            treeNode.Tag = piece;
            return treeNode;
        }

        /// <summary>
        /// Rebuild the treeView
        /// </summary>
        /// <returns></returns>
        private void RebuidTreeView()
        {
            #region Entries validation

            if (this._dataTreeNodeList.Count == 0)
            {
                return;
            }

            #endregion

            List<TreeNode> treeNodeList = new List<TreeNode>();
            List<TreeNode> childrenTreeNodeList = new List<TreeNode>();

            foreach (var dataTreeNode in this._dataTreeNodeList)
            {
                #region Validation

                if (childrenTreeNodeList.Contains(dataTreeNode))
                {
                    continue;
                }

                #endregion

                var dataChildenList =
                this.DetermineChildren(dataTreeNode, this._dataTreeNodeList);

                if (dataChildenList.Count > 0)
                {
                    TreeNode groupNode = new TreeNode("Group " + dataTreeNode.Text);    // <-- TODO: It´s still needed discovered when a group is above another group.
                    groupNode.Nodes.Add(
                        (TreeNode)dataTreeNode.Clone());

                    TreeNode childrenNode = new TreeNode("Children " + dataTreeNode.Text);
                    groupNode.Nodes.Add(childrenNode);
                    foreach (var dataChildrenItem in dataChildenList)
                    {
                        childrenNode.Nodes.Add(
                            (TreeNode)dataChildrenItem.Clone());

                        childrenTreeNodeList.Add(dataChildrenItem);
                    }
                    treeNodeList.Add(groupNode);
                }
                else
                {
                    treeNodeList.Add(
                        (TreeNode)dataTreeNode.Clone());
                }
            }

            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.AddRange(treeNodeList.ToArray());
            this.treeView1.ExpandAll();
        }

        /// <summary>
        /// Checks if the data tree node exists in data buffer, based on Piece associed
        /// </summary>
        private bool ContainsTreeNodeList(TreeNode dataTreeNode, List<TreeNode> treeNodeList)
        {
            #region Entries validation

            if (dataTreeNode == null)
            {
                throw new ArgumentNullException("dataTreeNode");
            }
            if (treeNodeList == null)
            {
                throw new ArgumentNullException("treeNodeList");
            }

            #endregion

            foreach (var dataTreeNodeItem in treeNodeList)
            {
                if (dataTreeNodeItem.Tag == dataTreeNode.Tag && dataTreeNodeItem.Tag != null)
                {
                    if (dataTreeNode.Tag is Piece)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Move node to a nodelist
        /// </summary>
        /// <param name="item"></param>
        /// <param name="treeNodeGroup"></param>
        private void MoveToNodeList(TreeNode item, TreeNode treeNodeGroup)
        {
            #region Entries validation

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (treeNodeGroup == null)
            {
                throw new ArgumentNullException("treeNodeGroup");
            }

            #endregion

            treeNodeGroup.Nodes.Add((TreeNode)item.Clone());
            if (item.Parent != null)
            {
                item.Parent.Nodes.Remove(item);
            }
            else
            {
                this.treeView1.Nodes.Remove(item);
            }
        }

        /// <summary>
        /// Determines the children of treeNode
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        private List<TreeNode> DetermineChildren(TreeNode treeNode, List<TreeNode> nodeList)
        {
            #region Entries validation

            if (treeNode == null)
            {
                throw new ArgumentNullException("treeNode");
            }
            if (nodeList == null)
            {
                throw new ArgumentNullException("nodeList");
            }
            if (!(treeNode.Tag is Piece))
            {
                return new List<TreeNode>();
            }

            #endregion

            List<TreeNode> list = new List<TreeNode>();
            Piece piece = (Piece)treeNode.Tag;
            foreach (TreeNode treeNodeItem in nodeList)
            {
                #region Entries validation

                if (treeNodeItem == null)
                {
                    throw new ArgumentNullException("treeNodeItem");
                }
                if (treeNodeItem.Tag == null)
                {
                    continue;
                }
                if (!(treeNodeItem.Tag is Piece))
                {
                    continue;
                }

                #endregion

                Piece pieceItem = (Piece)treeNodeItem.Tag;

                if (
                    piece.PointA.X < pieceItem.PointA.X &&
                    piece.PointA.Y < pieceItem.PointA.Y &&
                    piece.PointB.X > pieceItem.PointB.X &&
                    piece.PointB.Y > pieceItem.PointB.Y)
                {
                    list.Add(treeNodeItem);
                }
            }

            // Mainteining just the first child
            List<TreeNode> rejectedChildrenList = new List<TreeNode>();
            foreach (var item in list)
            {
                if (this.DetermineChildren(item, list).Count > 0)
                {
                    rejectedChildrenList.Add(item);
                }
            }
            list.RemoveAll(delegate (TreeNode item)
            {
                return rejectedChildrenList.Contains(item);
            });

            return list;
        }

        /// <summary>
        /// Gets all the treenodes, regardless of levels, from the treeNodeList
        /// </summary>
        /// <returns></returns>
        private List<TreeNode> GetAllTreeNodes(TreeNodeCollection treeNodeList)
        {
            #region Entries validation

            if (treeNodeList == null)
            {
                throw new ArgumentNullException("treeNodeList");
            }

            #endregion

            List<TreeNode> list = new List<TreeNode>();
            foreach (TreeNode item in treeNodeList)
            {
                list.Add(item);

                if (item.Nodes.Count > 0)
                {
                    list.AddRange(
                        this.GetAllTreeNodes(item.Nodes));
                }
            }

            return list;
        }

        #region Events

        /// <summary>
        /// Loads the form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.LoadSettings();

            this.FormClosed += PrincipalForm_FormClosed;
            this.draftControl1.GettingSettings += DraftControl1_GettingSettings;
        }

        /// <summary>
        /// Occurs when the draft controls needs to check toolbars
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DraftControl1_GettingSettings(object sender, Controls.ToolboxState.DraftSettings e)
        {
            e.HookOn = this.toolHookButton.Checked;  
        }

        /// <summary>
        /// Occurs when the form has closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrincipalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the click on check boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                return;
            }

            foreach (var item in e.Node.Nodes)
            {
                ((TreeNode)item).Checked = e.Node.Checked;
            }
        }

        private void btnOpenDraft_Click(object sender, EventArgs e)
        {
            this.DoOpenSourceDialog(this.txtDraftSourceFolder, this.openDraftFileDialog1);
        }

        private void btnOpenResumeWork_Click(object sender, EventArgs e)
        {
            this.DoOpenSourceDialog(this.txtLoadSprite, this.openSmartSpriteFileDialog1);
        }

        /// <summary>
        /// Ocurrs when the user leaves the focus of source folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSourceFolder_Leave(object sender, EventArgs e)
        {
            this.RefreshForm();
        }

        private void toolHookButton_Click(object sender, EventArgs e)
        {
            RefreshHookCreationMode();
        }

        /// <summary>
        /// Refresh the hook creation mode
        /// </summary>
        private void RefreshHookCreationMode()
        {
            this.draftControl1.LastSettings =
                new smartSprite.Forms.Controls.ToolboxState.DraftSettings
                {
                    HookOn = toolHookButton.Checked
                };
        }

        /// <summary>
        /// Occurs then any piece was modified in somehow in draft board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DraftControl1_PieceSetChanged(object sender, Controls.DraftControlState.PieceEventArgs e)
        {
            TreeNode treeNode = null;

            switch (e.Action)
            {
                case Forms.Controls.DraftControlState.ActionEnum.SELECTED:

                    treeNode = this.GetTreeNode(e.Piece, this.treeView1.Nodes);
                    if (treeNode == null)
                    {
                        throw new ArgumentException("The piece " + e.Piece.Name + " hasn't found.");
                    }
                    this.treeView1.SelectedNode = treeNode;
                    // this.treeView1.Focus(); // <-- This solves the problem with bold item in treeview, but breaks the selection on draft board.
                    break;

                case Forms.Controls.DraftControlState.ActionEnum.DELETED:

                    treeNode = this.GetTreeNode(e.Piece, this._dataTreeNodeList);
                    if (treeNode == null)
                    {
                        throw new ArgumentException("The piece " + e.Piece.Name + " hasn't found.");
                    }
                    this._dataTreeNodeList.Remove(treeNode);
                    this.RebuidTreeView();

                    break;

                case Forms.Controls.DraftControlState.ActionEnum.CREATED:

                    this._dataTreeNodeList.Add(
                        this.ConvertToTreeNode(e.Piece));

                    this.RebuidTreeView();
                    this.treeView1.SelectedNode = this.GetTreeNode(e.Piece, this.treeView1.Nodes);
                    this.treeView1.Focus();

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Gets the treeNode from node collection
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private TreeNode GetTreeNode(Piece piece, TreeNodeCollection nodes)
        {
            #region Entries validation

            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }
            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }
            if (nodes.Count == 0)
            {
                return null;
            }

            #endregion

            foreach (TreeNode treeNode in nodes)
            {
                if (treeNode.Tag == piece)
                {
                    return treeNode;
                }
                
                if(treeNode.Nodes.Count > 0)
                {
                    return this.GetTreeNode(piece, treeNode.Nodes);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the treeNode from node collection
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private TreeNode GetTreeNode(Piece piece, List<TreeNode> nodes)
        {
            #region Entries validation

            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }
            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }
            if (nodes.Count == 0)
            {
                return null;
            }

            #endregion

            foreach (TreeNode treeNode in nodes)
            {
                if (treeNode.Tag == piece)
                {
                    return treeNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Occurs when a node is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            object tag = e.Node.Tag;

            if (!(tag is Piece))
            {
                return;
            }

            Piece piece = (Piece)tag;
            this.draftControl1.SelectPiece(piece);
        }

        #endregion
    }
}
