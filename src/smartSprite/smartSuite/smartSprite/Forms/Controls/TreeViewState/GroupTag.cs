using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSpriteFX.Forms.Controls.TreeViewState
{
    /// <summary>
    /// Represents a tag informing the goal of just group itens
    /// </summary>
    class GroupTag
    {
        /// <summary>
        /// It´s the owner of the piece
        /// </summary>
        public Piece PieceOwner { get; private set; }

        public GroupTag(Piece pieceOwner)
        {
            this.PieceOwner = pieceOwner;
        }
    }
}
