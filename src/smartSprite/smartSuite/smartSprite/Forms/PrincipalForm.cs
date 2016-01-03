using smartSprite.Properties;
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
        public PrincipalForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Refresh the tree view list
        /// </summary>
        private void RefreshTreeViewList(String sourceFolder, TreeNodeCollection nodes)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(sourceFolder))
            {
                throw new ArgumentNullException("sourceFolder");
            }
            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }

            #endregion

            this.ShowRunningResult(false);

            nodes.Clear();

            var directoryList = Directory.GetDirectories(sourceFolder);
            var directoryNodeList = ConvertNodeList(directoryList, false);

            /*
            this.treeView1.CheckBoxes = true;
            this.treeView1.AfterCheck += TreeView1_AfterCheck;
            this.treeView1.Nodes.AddRange(directoryNodeList.ToArray());
            this.treeView1.ExpandAll();
            */
        }

        /// <summary>
        /// Converts the list to list of treenodes.
        /// </summary>
        private List<TreeNode> ConvertNodeList(string[] uriList, bool handleUriAsFiles)
        {
            #region Entries validation

            if (uriList == null)
            {
                throw new ArgumentNullException("directoryList");
            }

            #endregion

            List<TreeNode> returnList = new List<TreeNode>();

            foreach (var uriItem in uriList)
            {
                try
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Tag = uriItem;
                    treeNode.Text = Path.GetFileName(uriItem);

                    // Adding files
                    if (!handleUriAsFiles)
                    {
                        // Adding sub directories
                        treeNode.Nodes.AddRange(
                            ConvertNodeList(
                                Directory.GetDirectories(uriItem),
                                false).ToArray());

                        treeNode.Nodes.AddRange(
                            ConvertNodeList(
                                GetExceptSmartMap(
                                    Directory.GetFiles(uriItem, "*.png")),
                                true).ToArray());

                        treeNode.Nodes.AddRange(
                            ConvertNodeList(
                                GetExceptSmartMap(
                                    Directory.GetFiles(uriItem, "*.bmp")),
                                true).ToArray());
                    }
                    // Handling files
                    else
                    {
                        treeNode.Checked = true;
                    }

                    returnList.Add(treeNode);
                }
                catch (IOException)
                {
                    // Errors of this kind in this location cannot turns around the normal flow of algoritmn
                    continue;
                }
                catch (UnauthorizedAccessException)
                {
                    // Errors of this kind in this location cannot turns around the normal flow of algoritmn
                    continue;
                }
            }

            return returnList;
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
            e.HuckOn = this.toolHuckButton.Checked;  
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

        private void toolHuckButton_Click(object sender, EventArgs e)
        {
            this.draftControl1.LastSettings =
                new smartSprite.Forms.Controls.ToolboxState.DraftSettings
                {
                    HuckOn = toolHuckButton.Checked
                };
        }

        #endregion
    }
}
