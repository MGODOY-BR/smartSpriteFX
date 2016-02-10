using smartSprite.Forms.Controls.TreeViewState;
using smartSprite.Forms.Utilities;
using smartSprite.Properties;
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections;
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
                this.txtDraftPicture.Text = Settings.Default.lastSourceFolder;
                this.openDraftFileDialog1.FileName = this.txtDraftPicture.Text.Trim();
            }
            else
            {
                this.txtDraftPicture.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }

            this.lblVersion.Text = this.GetType().Assembly.GetName().Version.ToString() + "(Alpha)";
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

                // Loading the picture
                this.draftControl1.LoadDraftPicture(this.txtDraftPicture.Text.Trim());
            }
        }

        /// <summary>
        /// Opens the open project dialog
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="openDialog"></param>
        private void DoOpenProjectDialog(TextBox sourceFolder, OpenFileDialog openDialog)
        {
            /*
            openDialog.ShowDialog();
            if (openDialog.FileName != null)
            {
                sourceFolder.Text = openDialog.FileName;

                // Loading the picture
                this.draftControl1.LoadDraftPicture(@"C:\Users\Marcelo\Documents\Repositorio GIT\smartSprite\[imageLibrary]\DraftSample.png");
            }
            */
            openDialog.ShowDialog();
            if (openDialog.FileName != null)
            {
                sourceFolder.Text = openDialog.FileName;

                this._dataTreeNodeList.Clear();
                // Loading the picture
                this.draftControl1.LoadProject(sourceFolder.Text.Trim());
                // Filling TreeNodeList
                foreach (var pieceItem in this.draftControl1.Pieces.PieceList)
                {
                    this.FillTreeNodeList(pieceItem);
                }

                this.RebuidTreeView();
            }
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

            // Reset the dataTreeNodeList
            TreeViewUtil.ResetParent(this._dataTreeNodeList);

            // Organizing all parent the pieces
            var pieceList = TreeViewUtil.OrganizeFamily(this._dataTreeNodeList);

            // Build the hierarchy
            var treeviewList = TreeViewUtil.BuildTreeHierarchy(pieceList, this._dataTreeNodeList);

            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.AddRange(treeviewList.ToArray());
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
            this.DoOpenSourceDialog(this.txtDraftPicture, this.openDraftFileDialog1);
        }

        private void btnOpenResumeWork_Click(object sender, EventArgs e)
        {
            this.DoOpenProjectDialog(this.txtLoadSprite, this.openSmartSpriteFileDialog1);
        }

        /// <summary>
        /// Occurs when the user leaves the focus of source folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDraftPicture_Leave(object sender, EventArgs e)
        {
            // Loading the picture
            this.draftControl1.LoadDraftPicture(this.txtDraftPicture.Text.Trim());
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

                    FillTreeNodeList(e.Piece);
                    break;

                case Forms.Controls.DraftControlState.ActionEnum.UPDATED:

                    this.RebuidTreeView();
                    break;

                default:
                    throw new NotSupportedException(e.Action.ToString() + " isn't supported.");
            }
        }

        /// <summary>
        /// Fills and refresh the treeNodeList with the piece assigned
        /// </summary>
        /// <param name="piece"></param>
        private void FillTreeNodeList(Piece piece)
        {
            #region Entries validation

            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }

            #endregion

            this._dataTreeNodeList.Add(
                TreeViewUtil.ConvertToTreeNode(piece));

            this.RebuidTreeView();
            this.treeView1.SelectedNode = this.GetTreeNode(piece, this.treeView1.Nodes);
            this.treeView1.Focus();
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

        private void btnExportToUnity_Click(object sender, EventArgs e)
        {
            this.draftControl1.SendToUnity(
                Path.Combine(
                    Path.GetDirectoryName(
                        this.txtDraftPicture.Text),
                    Path.GetFileNameWithoutExtension(
                        this.txtDraftPicture.Text)));
        }

        /// <summary>
        /// Loads a PieceCollection saved in disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadSprite_Leave(object sender, EventArgs e)
        {
            openDraftFileDialog1.ShowDialog();

            #region Entries validation

            if (String.IsNullOrEmpty(this.openDraftFileDialog1.FileName))
            {
                return;
            }

            #endregion

            this.draftControl1.LoadProject(this.txtLoadSprite.Text.Trim());
        }

        /// <summary>
        /// Saves the PieceCollection to resume the work in the next time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveState_Click(object sender, EventArgs e)
        {
            #region Entries validation

            if (this.draftControl1.Pieces == null)
            {
                throw new ArgumentNullException("this.draftControl1.Pieces");
            }

            #endregion

            if (String.IsNullOrEmpty(this.draftControl1.ProjectFullPath))
            {
                this.saveFileDialog1.ShowDialog();
                if (this.saveFileDialog1.FileName != null)
                {
                    this.draftControl1.ProjectFullPath = this.saveFileDialog1.FileName;
                }
            }

            this.draftControl1.Pieces.Save(this.draftControl1.ProjectFullPath);

            MessageBox.Show("Project has been saved with success!!", "Save project", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
