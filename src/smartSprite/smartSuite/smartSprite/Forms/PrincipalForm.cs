using smartSprite.Forms.Controls;
using smartSprite.Forms.Controls.TreeViewState;
using smartSprite.Forms.Utilities;
using smartSprite.Properties;
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            LoadDefaultSetting(Settings.Default.lastDraftFolder, this.txtDraftPicture, this.openDraftFileDialog1);
            LoadDefaultSetting(Settings.Default.lastProjectFolder, this.txtLoadSprite, this.openSmartSpriteFileDialog1);
            LoadDefaultSetting(Settings.Default.lastExportFolder, null, this.exportToUnityDialog1);

            // this.lblVersion.Text = this.GetType().Assembly.GetName().Version.ToString() + "(Alpha)";
            var myFileVersionInfo =
                FileVersionInfo.GetVersionInfo(
                    this.GetType().Assembly.Location);
            this.lblVersion.Text = myFileVersionInfo.FileVersion + "(Alpha)";
        }

        /// <summary>
        /// Loads the specific default value
        /// </summary>
        private static void LoadDefaultSetting(string lastFolder, TextBox txtFile, CommonDialog openDialog)
        {
            if (!string.IsNullOrEmpty(lastFolder))
            {
                if (txtFile != null)
                {
                    txtFile.Text = lastFolder;
                }

                if (openDialog is OpenFileDialog)
                {
                    ((OpenFileDialog)openDialog).FileName = lastFolder;
                }
                else if (openDialog is FolderBrowserDialog)
                {
                    ((FolderBrowserDialog)openDialog).SelectedPath = lastFolder;
                }
            }
            else
            {
                if (txtFile != null)
                {
                    txtFile.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                }
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
        /// Opens the open draft dialog
        /// </summary>
        /// <param name="draftTextBox"></param>
        /// <param name="openDialog"></param>
        private void DoOpenDraftDialog(TextBox draftTextBox, OpenFileDialog openDialog)
        {
            var result = openDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                draftTextBox.Text = openDialog.FileName;

                // Loading the picture
                this.draftControl1.LoadDraftPicture(this.txtDraftPicture.Text.Trim());
                this.SetupScroll();
                this._dataTreeNodeList.Clear();
                this.txtLoadSprite.Text = "";
                this.RebuidTreeView();
            }
        }

        /// <summary>
        /// Opens the open project dialog
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="openDialog"></param>
        private void DoOpenProjectDialog(TextBox sourceFolder, OpenFileDialog openDialog)
        {
            var result = openDialog.ShowDialog();
            if(result == DialogResult.OK)
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
                this.SetupScroll();
                this.txtDraftPicture.Text = "";
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
                this.treeView1.Nodes.Clear();
                this.ShowControls();
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
            this.ShowControls();
        }

        /// <summary>
        /// Show main controls
        /// </summary>
        private void ShowControls()
        {
            this.toolStrip1.Visible = true;
            tableLayoutPieceBlock.Visible = true;
        }

        /// <summary>
        /// Setup the scroll bars
        /// </summary>
        /// <remarks>
        /// I prefered use this than AutoScroll because of the behavior of scroll towards form use.
        /// We are using image when it focused, that is a different aproach.
        /// </remarks>
        private void SetupScroll()
        {
            this.hScrollBar1.Visible = true;
            this.vScrollBar1.Visible = true;

            this.hScrollBar1.Maximum = Math.Abs(this.draftControl1.Width - this.pnlImage.Width) + 100;
            this.vScrollBar1.Maximum = Math.Abs(this.draftControl1.Height - this.pnlImage.Height) + 100;

            this.hScrollBar1.LargeChange = this.hScrollBar1.Maximum / 4;
            this.vScrollBar1.LargeChange = this.vScrollBar1.Maximum / 4;

            this.hScrollBar1.SmallChange = 1;
            this.vScrollBar1.SmallChange = 1;
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

                if (treeNode.Nodes.Count > 0)
                {
                    var subTreeNode = this.GetTreeNode(piece, treeNode.Nodes);
                    if (subTreeNode != null)
                    {
                        return subTreeNode;
                    }
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
        /// Applies the text in textBox to project
        /// </summary>
        private void ApplyProject()
        {
            #region Entries validation

            if (String.IsNullOrEmpty(this.txtLoadSprite.Text.Trim()))
            {
                return;
            }

            #endregion

            this._dataTreeNodeList.Clear();
            // Loading the picture
            this.draftControl1.LoadProject(this.txtLoadSprite.Text.Trim());
            // Filling TreeNodeList
            foreach (var pieceItem in this.draftControl1.Pieces.PieceList)
            {
                this.FillTreeNodeList(pieceItem);
            }

            this.RebuidTreeView();
            this.SetupScroll();
            this.ShowControls();
            this.txtDraftPicture.Text = "";
        }

        /// <summary>
        /// Applies the draft
        /// </summary>
        private void ApplyDraft()
        {
            #region Entries validation

            if (String.IsNullOrEmpty(this.txtDraftPicture.Text.Trim()))
            {
                return;
            }

            #endregion

            // Loading the picture
            this.draftControl1.LoadDraftPicture(this.txtDraftPicture.Text.Trim());
            this.SetupScroll();
            this.RebuidTreeView();
            this.txtLoadSprite.Text = "";
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

            this.draftControl1 = new DraftControl();
            this.draftControl1.BorderStyle = BorderStyle.FixedSingle;
            this.pnlImage.Controls.Add(this.draftControl1);

            this.FormClosed += PrincipalForm_FormClosed;
            this.draftControl1.GettingSettings += DraftControl1_GettingSettings;
            this.treeView1.LabelEdit = true;
            this.treeView1.PreviewKeyDown += TreeView1_PreviewKeyDown;

            this.draftControl1.PieceSetChanged += DraftControl1_PieceSetChanged;
            this.treeView1.AfterSelect += TreeView1_AfterSelect;
            this.treeView1.AfterLabelEdit += TreeView1_AfterLabelEdit;
            this.KeyDown += PrincipalForm_KeyDown;

            this.hScrollBar1.Visible = false;
            this.vScrollBar1.Visible = false;
            this.hScrollBar1.Scroll += HScrollBar1_Scroll;
            this.vScrollBar1.Scroll += VScrollBar1_Scroll;

            // Only enable this in debug progresses
            //
            // this.txtDraftPicture.Leave += txtDraftPicture_Leave;
            // this.txtLoadSprite.Leave += txtLoadSprite_Leave;

            this.MouseWheel += PrincipalForm_MouseWheel;
        }

        private void TreeView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    treeView1.SelectedNode.BeginEdit();
                    break;

                case Keys.Enter:
                    treeView1.SelectedNode.EndEdit(false);
                    break;

                case Keys.Escape:
                    treeView1.SelectedNode.EndEdit(true);
                    break;
            }
        }

        private void TreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(e.Label))
            {
                return;
            }

            #endregion

            ((Piece)e.Node.Tag).Name = e.Label;
        }

        private void savePiecesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            String selectedPath = e.Argument as String;
            this.draftControl1.SendToUnity(selectedPath);
            e.Result = selectedPath;
        }

        private void savePiecesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressTime.Enabled = false;
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            this.Enabled = true;
            this.toolStripStatusLabel1.Text = "";
            this.Cursor = Cursors.Default;
            String selectedPath = e.Result as String;

            Settings.Default.lastExportFolder = selectedPath;
            Settings.Default.Save();

            MessageBox.Show("Pieces has cut with success!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void progressTime_Tick(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value++;

            if (toolStripProgressBar1.Value == 100)
            {
                toolStripProgressBar1.Value = 0;
            }
        }

        private void btnDraftApply_Click(object sender, EventArgs e)
        {
            this.ApplyDraft();
        }

        private void btnProjectApply_Click(object sender, EventArgs e)
        {
            this.ApplyProject();
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

        private void PrincipalForm_MouseWheel(object sender, MouseEventArgs e)
        {
            #region Entries validation

            if (this.vScrollBar1.Visible == false)
            {
                return;
            }

            #endregion

            int newValue = this.vScrollBar1.Value + (e.Delta * -1) / 20;

            if (newValue > this.vScrollBar1.Maximum)
            {
                newValue = this.vScrollBar1.Maximum;
            }
            else if (newValue < this.vScrollBar1.Minimum)
            {
                newValue = 0;
            }

            this.vScrollBar1.Value = newValue;
            this.VScrollBar1_Scroll(this, new ScrollEventArgs(ScrollEventType.LargeDecrement, newValue));
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.draftControl1.Top = e.NewValue * -1;
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.draftControl1.Left = e.NewValue * -1;
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
            this.DoOpenDraftDialog(this.txtDraftPicture, this.openDraftFileDialog1);
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
            ApplyDraft();
        }

        private void toolHookButton_Click(object sender, EventArgs e)
        {
            RefreshHookCreationMode();
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
                        return;
                        // throw new ArgumentException("The piece " + e.Piece.Name + " hasn't found.");
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

        /// <summary>
        /// Occurs when the user exports the project to Unity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportToUnity_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtLoadSprite.Text))
            {
                this.exportToUnityDialog1.SelectedPath = Path.GetDirectoryName(this.txtLoadSprite.Text);
            }
            var result = this.exportToUnityDialog1.ShowDialog();

            var selectedPath = this.exportToUnityDialog1.SelectedPath;
            if (result == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                this.toolStripStatusLabel1.Text = "Please wait and stay on touch! I can need you to ask for some doubts.";
                this.Enabled = false;

                toolStripProgressBar1.Visible = true;
                progressTime.Enabled = true;
                this.savePiecesBackgroundWorker.RunWorkerAsync(selectedPath);
            }
        }

        /// <summary>
        /// Loads a PieceCollection saved in disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadSprite_Leave(object sender, EventArgs e)
        {
            ApplyProject();
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
                throw new ArgumentNullException("You need open a draft picture and draw some pieces before use this function.");
            }

            #endregion

            var projectFullPath = this.txtLoadSprite.Text;

            if (string.IsNullOrEmpty(projectFullPath))
            {
                var result = this.saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    projectFullPath = this.saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
            }

            this.draftControl1.Pieces.Save(projectFullPath);

            Settings.Default.lastProjectFolder = projectFullPath;
            Settings.Default.Save();

            this.draftControl1.ProjectFullPath = projectFullPath;

            MessageBox.Show("Project has been saved with success!!", "Save project", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
