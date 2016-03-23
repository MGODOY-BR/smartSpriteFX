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
                             where ((Piece)treeNodeItem.Tag).Contains((Piece)dataTreeNode.Tag)
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

                if (piece.Contains(pieceItem))
                {
                    list.Add(treeNodeItem);
                }
            }

            // Maintaining unique children
            List<TreeNode> rejectedChildrenList = new List<TreeNode>();
            foreach (var item in list)
            {
                /*
                if (TreeViewUtil.DetermineChildren(item, list).Count > 0)
                {
                    rejectedChildrenList.Add(item);
                }
                */
                if (((Piece)item.Tag).Parent != null)
                {
                    rejectedChildrenList.Add(item);
                }
            }

            list.RemoveAll(delegate (TreeNode item)
            {
                return rejectedChildrenList.Contains(item);
            });

            foreach (var item in list)
            {
                ((Piece)item.Tag).Parent = piece;
            }

            return list;
        }

        /// <summary>
        /// Reset the parent of each nodes
        /// </summary>
        /// <param name="dataTreeNodeList"></param>
        public static void ResetParent(List<TreeNode> dataTreeNodeList)
        {
            #region Entries validation

            if (dataTreeNodeList == null)
            {
                throw new ArgumentNullException("dataTreeNodeList");
            }

            #endregion

            foreach (TreeNode treeNodeItem in dataTreeNodeList)
            {
                Piece piece = (Piece)treeNodeItem.Tag;

                piece.Parent = null;

                TreeViewUtil.RemoveFromParent(treeNodeItem);
            }
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

        /// <summary>
        /// Sets the family of pieces
        /// </summary>
        /// <param name="dataTreeNodeList"></param>
        public static List<Piece> OrganizeFamily(List<TreeNode> dataTreeNodeList)
        {
            // Extracting Pieces
            List<Piece> pieceList = GetPieceList(dataTreeNodeList);

            // Cleaning the previous bindings
            foreach (var piece in pieceList)
            {
                piece.Parent = null;
            }

            #region Setting parents
            foreach (var previousPiece in pieceList)
            {
                for (int i = 0; i < pieceList.Count; i++)
                {
                    var pieceItem = pieceList[i];

                    if (previousPiece != null)
                    {
                        if (previousPiece.Contains(pieceItem))
                        {
                            pieceItem.Parent = previousPiece;
                        }
                    }
                }
            }

            #endregion

            return pieceList;
        }

        /// <summary>
        /// Gets a build of tree hierarchy created from dataTreeNode
        /// </summary>
        /// <param name="pieceList"></param>
        /// <param name="dataTreeNodeList"></param>
        /// <returns></returns>
        public static List<TreeNode> BuildTreeHierarchy(List<Piece> pieceList, List<TreeNode> dataTreeNodeList)
        {
            #region Entries validation

            if (pieceList == null)
            {
                throw new ArgumentNullException("pieceList");
            }
            if (dataTreeNodeList == null)
            {
                throw new ArgumentNullException("dataTreeNodeList");
            }

            #endregion

            List<TreeNode> returnValue = new List<TreeNode>();

            foreach (var pieceItem in pieceList)
            {
                // Getting the node
                TreeNode treeNode = TreeViewUtil.GetTreeNode(dataTreeNodeList, pieceItem);

                TreeViewUtil.RemoveFromParent(treeNode);

                #region Getting the parentNode

                if (pieceItem.Parent != null)
                {
                    TreeNode parentTreeNode = TreeViewUtil.GetTreeNode(dataTreeNodeList, pieceItem.Parent);
                    var childList = TreeViewUtil.DetermineChildren(parentTreeNode, dataTreeNodeList);

                    foreach (TreeNode childNode in childList)
                    {
                        if (parentTreeNode.Nodes.Contains(childNode))
                        {
                            continue;
                        }

                        TreeViewUtil.RemoveFromParent(childNode);
                        parentTreeNode.Nodes.Add(childNode);
                    }

                    parentTreeNode.Nodes.Add(treeNode);
                    parentTreeNode.ExpandAll();
                }
                else
                {
                    returnValue.Add(treeNode);
                }

                #endregion
            }

            return returnValue;
        }

        /// <summary>
        /// Removesa node from its parent
        /// </summary>
        /// <param name="treeNode"></param>
        private static void RemoveFromParent(TreeNode treeNode)
        {
            #region Entries validation

            if (treeNode == null)
            {
                throw new ArgumentNullException("treeNode");
            }

            #endregion

            if (treeNode.TreeView != null)
            {
                treeNode.TreeView.Nodes.Remove(treeNode);
            }
            if (treeNode.Parent != null)
            {
                treeNode.Parent.Nodes.Remove(treeNode);
            }
        }

        /// <summary>
        /// Gets a treenode respective to piece item.
        /// </summary>
        /// <param name="dataTreeNodeList"></param>
        /// <param name="pieceItem"></param>
        /// <returns></returns>
        private static TreeNode GetTreeNode(List<TreeNode> dataTreeNodeList, Piece pieceItem)
        {
            #region Entries validation

            if (dataTreeNodeList == null)
            {
                throw new ArgumentNullException("dataTreeNodeList");
            }
            if (pieceItem == null)
            {
                throw new ArgumentNullException("pieceItem");
            }

            #endregion

            var treeViewList = from treeNodeItem in dataTreeNodeList
                               where treeNodeItem.Tag == pieceItem
                               select treeNodeItem;

            return treeViewList.FirstOrDefault();
        }

        /// <summary>
        /// Gets the piece list from TreeNode
        /// </summary>
        /// <param name="dataTreeNodeList"></param>
        /// <returns></returns>
        private static List<Piece> GetPieceList(List<TreeNode> dataTreeNodeList)
        {
            #region Entries validation

            if (dataTreeNodeList == null)
            {
                throw new ArgumentNullException("dataTreeNodeList");
            }

            #endregion
            List<Piece> pieceList = new List<Piece>();

            foreach (TreeNode dataTreeNodeItem in dataTreeNodeList)
            {
                pieceList.Add(
                    (Piece)dataTreeNodeItem.Tag);
            }

            // Ordering pieces
            pieceList.Sort();
            return pieceList;
        }
    }
}
