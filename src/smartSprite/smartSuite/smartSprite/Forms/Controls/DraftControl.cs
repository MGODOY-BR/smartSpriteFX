using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSprite.Forms.Controls.ToolboxState;
using smartSuite.smartSprite.Pictures;
using smartSprite.Forms.Controls.DraftControlState;

namespace smartSprite.Forms.Controls
{
    public partial class DraftControl : UserControl
    {
        /// <summary>
        /// Relates the owner hook control with a piece
        /// </summary>
        private Dictionary<HookControl, Piece> _pieceSet = new Dictionary<HookControl, Piece>();

        /// <summary>
        /// It´s a collection of main (older) hooks.
        /// </summary>
        private List<HookControl> _hookSet = new List<HookControl>();

        /// <summary>
        /// Gets or sets the last hook added
        /// </summary>
        private HookControl _lastHook;

        /// <summary>
        /// It is the last settings
        /// </summary>
        private DraftSettings _lastSettings;

        /// <summary>
        /// Gets or sets the last settings
        /// </summary>
        public DraftSettings LastSettings
        {
            get
            {
                return _lastSettings;
            }

            set
            {
                this._lastSettings = value;
            }
        }

        public DraftControl()
        {
            InitializeComponent();
        }

        #region Delegates

        /// <summary>
        /// It happens when some piece it has created, modified or deleted
        /// </summary>
        public event EventHandler<PieceEventArgs> PieceSetChanged;

        /// <summary>
        /// Occurs when the control needs to consults the choices of another control by the user
        /// </summary>
        /// <returns></returns>
        public event EventHandler<DraftSettings> GettingSettings;

        /// <summary>
        /// Throws the respective event
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="action"></param>
        private void OnPieceSetChanged(Piece piece, ActionEnum action)
        {
            #region Entries validation

            if (PieceSetChanged == null)
            {
                return;
            }

            #endregion

            this.PieceSetChanged(this, new PieceEventArgs(piece, action));
        }

        /// <summary>
        /// Gets the settings to draft control, specified for the parent controler
        /// </summary>
        /// <returns></returns>
        private DraftSettings OnGettingSettings()
        {
            if (this.GettingSettings == null)
            {
                throw new NotImplementedException("GettingSettings event is missed.");
            }

            DraftSettings draftSettings = new DraftSettings();
            this.GettingSettings(this, draftSettings);

            return draftSettings;
        }

        #endregion

        /// <summary>
        /// Updates the cursor style
        /// </summary>
        private void UpdateCursorStyle()
        {
            if (this._lastSettings != null && this._lastSettings.HookOn)
            {
                imgDraft.Cursor = Cursors.Cross;
            }
            else
            {
                imgDraft.Cursor = Cursors.Default;
            }
        }

        #region Events

        private void imgDraft_MouseClick(object sender, MouseEventArgs e)
        {
            this._lastSettings = this.OnGettingSettings();

            if (this._lastSettings.HookOn)
            {
                AddNewHook(e);
            }
        }

        /// <summary>
        /// Adds a new hook
        /// </summary>
        /// <param name="e"></param>
        private void AddNewHook(MouseEventArgs e)
        {
            HookControl newHook = new HookControl();
            newHook.BeenSelected += NewHook_BeenSelected;
            newHook.Deleting += NewHook_Deleting;
            newHook.PositionChanged += NewHook_PositionChanged;

            newHook.Top = e.Y - newHook.Height / 2;
            newHook.Left = e.X - newHook.Width / 2;
            newHook.Pair = this._lastHook;
            newHook.RefreshCoordinates();

            if (this._lastHook != null)
            {
                this._lastHook.Pair = newHook;
                this._lastHook = null;
            }

            this._lastHook = newHook;
            this.imgDraft.Controls.Add(newHook);
            if (newHook.Pair != null)
            {
                newHook.CreateLines();
                this._hookSet.Add(newHook.Pair);
                this._lastHook = null;

                Piece newPiece =
                    new Piece(
                        new Picture(this.imgDraft.ImageLocation),
                        newHook.GetOlderHuckFromPair().Point,
                        newHook.GetNewerHuckFromPair().Point);

                newPiece.Name = "Sprite2DObject " + (this._pieceSet.Count + 1).ToString();

                this._pieceSet.Add(
                    newHook.GetOlderHuckFromPair(),
                    newPiece);

                this.OnPieceSetChanged(newPiece, ActionEnum.CREATED);
            }
        }

        /// <summary>
        /// Occurs when hook has moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewHook_PositionChanged(object sender, HookState.HookEventArgs e)
        {
            var piece = this._pieceSet[e.MainHook];

            piece.Fix();

            this.OnPieceSetChanged(
                piece,
                ActionEnum.UPDATED);
        }

        /// <summary>
        /// Occurs when hook as been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewHook_BeenSelected(object sender, HookState.HookEventArgs e)
        {
            this.OnPieceSetChanged(
                this._pieceSet[e.MainHook],
                ActionEnum.SELECTED);
        }

        private void NewHook_Deleting(object sender, HookState.HookEventArgs e)
        {
            Piece piece = this._pieceSet[e.MainHook];
            e.MainHook.DestroyYourSelf();
            this._pieceSet.Remove(e.MainHook);

            this.OnPieceSetChanged(piece, ActionEnum.DELETED);
        }

        private void imgDraft_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursorStyle();
        }

        #endregion

        /// <summary>
        /// Selects a piece
        /// </summary>
        /// <param name="piece"></param>
        internal void SelectPiece(Piece piece)
        {
            HookControl mainHook = this.GetHookControl(piece);

            foreach (var item in this._pieceSet)
            {
                if (item.Key == mainHook)
                {
                    item.Key.Mark(true);
                }
                else
                {
                    item.Key.Mark(false);
                }
            }
        }

        /// <summary>
        /// Gets a hook control from a piece
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private HookControl GetHookControl(Piece piece)
        {
            foreach (var item in this._pieceSet)
            {
                if (item.Value == piece)
                {
                    return item.Key;
                }
            }

            return null;
        }
    }
}
