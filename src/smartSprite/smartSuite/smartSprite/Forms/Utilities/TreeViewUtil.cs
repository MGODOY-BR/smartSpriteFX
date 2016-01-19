using smartSuite.smartSprite.Pictures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSprite.Forms.Utilities
{
    /// <summary>
    /// Offers utilities to work with treeviews and pieces
    /// </summary>
    public static class TreeViewUtil
    {

        /// <summary>
        /// Convert a piece to a TreeNode object
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static TreeNode ConvertToTreeNode(Piece piece)
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
        /// Gets a clone of list
        /// </summary>
        /// <param name="dataTreeNodeList"></param>
        /// <returns></returns>
        public static List<TreeNode> CloneDataTreeNodeList(List<TreeNode> dataTreeNodeList)
        {
            List<TreeNode> returneValue = new List<TreeNode>();

            foreach (TreeNode treeNodeItem in dataTreeNodeList)
            {
                returneValue.Add((TreeNode)treeNodeItem.Clone());
            }

            return returneValue;
        }

        /// <summary>
        /// Gets the group applicable to treeNodeList
        /// </summary>
        /// <returns></returns>
        public static TreeNode GetParentNode(TreeNode dataTreeNode, List<TreeNode> treeNodeSet)
        {
            #region Entries validation

            if (dataTreeNode == null)
            {
                throw new ArgumentNullException("dataTreeNode");
            }
            if (treeNodeSet == null)
            {
                throw new ArgumentNullException("treeNodeSet");
            }

            #endregion

            Piece piece = dataTreeNode.Tag as Piece;
            var parentList = from treeNodeItem in treeNodeSet
                             where ((Piece)treeNodeItem.Tag).IsChild((Piece)dataTreeNode.Tag)
                             select treeNodeItem;

            TreeNode groupNode = null;

            if (parentList.Count() > 0)
            {
                groupNode = parentList.FirstOrDefault();
                if (groupNode == dataTreeNode)
                {
                    return null;
                }
            }
            else
            {
                groupNode = null;
            }

            return groupNode;
        }

        /// <summary>
        /// Checks if the data tree node exists in data buffer, based on Piece associed
        /// </summary>
        public static bool ContainsTreeNodeList(TreeNode dataTreeNode, List<TreeNode> treeNodeList)
        {
            return TreeViewUtil.DoContainsTreeNodeList(dataTreeNode, treeNodeList);
        }

        /// <summary>
        /// Checks if the data tree node exists in data buffer, based on Piece associed
        /// </summary>
        public static bool ContainsTreeNodeList(TreeNode dataTreeNode, TreeNodeCollection treeNodeList)
        {
            return TreeViewUtil.DoContainsTreeNodeList(dataTreeNode, treeNodeList);
        }

        /// <summary>
        /// Checks if the data tree node exists in data buffer, based on Piece associed
        /// </summary>
        private static bool DoContainsTreeNodeList(TreeNode dataTreeNode, IList treeNodeList)
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
                if (
                    ((TreeNode)dataTreeNodeItem).Tag == dataTreeNode.Tag &&
                    ((TreeNode)dataTreeNodeItem).Tag != null)
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
        public static void MoveToNodeList(TreeNode item, TreeNode treeNodeGroup)
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
                item.TreeView.Nodes.Remove(item);
            }
        }

        /// <summary>
        /// Determines the children of treeNode
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        public static List<TreeNode> DetermineChildren(TreeNode treeNode, List<TreeNode> nodeList)
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

                if (piece.IsChild(pieceItem))
                {
                    list.Add(treeNodeItem);
                }
            }

            // Maintaining just the first child
            List<TreeNode> rejectedChildrenList = new List<TreeNode>();
            foreach (var item in list)
            {
                if (TreeViewUtil.DetermineChildren(item, list).Count > 0)
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
        public static List<TreeNode> GetAllTreeNodes(TreeNodeCollection treeNodeList)
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
                        TreeViewUtil.GetAllTreeNodes(item.Nodes));
                }
            }

            return list;
        }


    }
}
