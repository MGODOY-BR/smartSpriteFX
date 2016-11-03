using smartSprite.Forms.Controls;
using smartSprite.Forms.Controls.Browsers;
using smartSprite.Forms.Controls.SwitchMode;
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
    /// <summary>
    /// Represents a form to handle the effect mode, animation-oriented
    /// </summary>
    public partial class LandscapeForm : Form
    {
        /// <summary>
        /// Contains just the relevant treenode
        /// </summary>
        private List<TreeNode> _dataTreeNodeList = new List<TreeNode>();

        /// <summary>
        /// It´s a browser for start from draft
        /// </summary>
        private SmartBrowser _draftBrowser = new SmartBrowser();

        /// <summary>
        /// It´s a browser for load project
        /// </summary>
        private SmartBrowser _projectBrowser = new SmartBrowser();

        public LandscapeForm()
        {
            InitializeComponent();

            this._draftBrowser.Width = 200;
            this._projectBrowser.Width = this._draftBrowser.Width;

            this._draftBrowser.FrameTitle = "New from DRAFT...";
            this._projectBrowser.FrameTitle = "RESUME Work...";

            this._draftBrowser.DialogTitle = "Select an image to use like a DRAFT";
            this._projectBrowser.DialogTitle = "Select a smartSprite Project to resume your work";

            this._draftBrowser.DialogFilter = "PNG Files (*.PNG)|*.PNG|Bitmap Files (*.BMP)|*.BMP|JPG Files(*.JPG;*.JPEG)|*.JPG;*.JPEG|All files (*.*)|*.*";
            this._projectBrowser.DialogFilter = "smartSprite Project|*.smartSprite";

            this._draftBrowser.ChosenByUserEvent += _draftBrowser_ChosenByUserEvent;
            this._projectBrowser.ChosenByUserEvent += _projectBrowser_ChosenByUserEvent;

            this.browserPanel.Controls.Add(this._draftBrowser);
            this.browserPanel.Controls.Add(this._projectBrowser);
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
            LoadDefaultSetting(Settings.Default.lastDraftFolder, this._draftBrowser);
            LoadDefaultSetting(Settings.Default.lastProjectFolder, this._projectBrowser);
        }

        /// <summary>
        /// Loads the specific default value
        /// </summary>
        private static void LoadDefaultSetting(string lastFolder, SmartBrowser smartBrowser)
        {
            if (!string.IsNullOrEmpty(lastFolder))
            {
                smartBrowser.UserChoice = lastFolder;
            }
            else
            {
                smartBrowser.UserChoice = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }

            smartBrowser.Reload();
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

            this.browserPanel.Enabled = enabled;
        }

        /// <summary>
        /// Opens the draft
        /// </summary>
        private void DoOpenDraft(String fileName)
        {
            // Loading the picture
            this.draftControl1.LoadDraftPicture(fileName);
            this.SetupScroll();
            this._dataTreeNodeList.Clear();
            this._projectBrowser.ClearText();
            this.RebuidTreeView();
        }

        /// <summary>
        /// Opens the project
        /// </summary>
        private void DoOpenProject(String fileName)
        {
            this._dataTreeNodeList.Clear();
            // Loading the picture
            this.draftControl1.LoadProject(fileName);
            // Filling TreeNodeList
            foreach (var pieceItem in this.draftControl1.Pieces.PieceList)
            {
                this.FillTreeNodeList(pieceItem);
            }

            this.RebuidTreeView();
            this.SetupScroll();
            this._draftBrowser.ClearText();
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

            if (String.IsNullOrEmpty(this._projectBrowser.UserChoice))
            {
                return;
            }

            #endregion

            this._dataTreeNodeList.Clear();
            // Loading the picture
            this.draftControl1.LoadProject(this._projectBrowser.UserChoice);
            // Filling TreeNodeList
            foreach (var pieceItem in this.draftControl1.Pieces.PieceList)
            {
                this.FillTreeNodeList(pieceItem);
            }

            this.RebuidTreeView();
            this.SetupScroll();
            this.ShowControls();
            this._draftBrowser.ClearText();
        }

        /// <summary>
        /// Applies the draft
        /// </summary>
        private void ApplyDraft()
        {
            #region Entries validation

            if (String.IsNullOrEmpty(this._draftBrowser.UserChoice))
            {
                return;
            }

            #endregion

            // Loading the picture
            this.draftControl1.LoadDraftPicture(this._draftBrowser.UserChoice);
            this.SetupScroll();
            this.RebuidTreeView();
            this._projectBrowser.ClearText();
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

            var switchModeControl = new SwitchModeControl();
            switchModeControl.Dock = DockStyle.Right;
            this.panel1.Controls.Add(switchModeControl);
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
            //if (!String.IsNullOrEmpty(this.txtLoadSprite.Text))
            //{
            //    this.exportToUnityDialog1.SelectedPath = Path.GetDirectoryName(this.txtLoadSprite.Text);
            //}
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

            String projectFullPath = this._projectBrowser.UserChoice;

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

        private void _projectBrowser_ChosenByUserEvent(object sender, SmartBrowserEventArgs e)
        {
            this.DoOpenProject(e.UserChoice);
        }

        private void _draftBrowser_ChosenByUserEvent(object sender, SmartBrowserEventArgs e)
        {
            this.DoOpenDraft(e.UserChoice);
        }

        private void _amimationModeBrowser_ChosenByUserEvent(object sender, SmartBrowserEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
