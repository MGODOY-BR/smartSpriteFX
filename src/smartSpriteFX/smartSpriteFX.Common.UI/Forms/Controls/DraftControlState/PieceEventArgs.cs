using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Controls.DraftControlState
{
    /// <summary>
    /// Represents a state of an event involving pieces
    /// </summary>
    public class PieceEventArgs : EventArgs
    {
        /// <summary>
        /// The piece
        /// </summary>
        public Piece Piece { get; private set; }

        /// <summary>
        /// What has happend to piece
        /// </summary>
        public ActionEnum Action { get; private set; }

        /// <summary>
        /// Create an instance of the object
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="action"></param>
        public PieceEventArgs(Piece piece, ActionEnum action)
        {
            #region Entries validation

            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }

            #endregion

            this.Piece = piece;
            this.Action = action;
        }
    }
}
