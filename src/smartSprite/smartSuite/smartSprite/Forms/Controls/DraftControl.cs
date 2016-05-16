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
using smartSprite.Properties;

namespace smartSprite.Forms.Controls
{
    /// <summary>
    /// Represents a board to work with draft to define pieces
    /// </summary>
    public partial class DraftControl : UserControl
    {
        #region Attributes

        /// <summary>
        /// It´s the main collection of pieces
        /// </summary>
        private PieceCollection _pieces;

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

        /// <summary>
        /// Returns or sets the full path of the file used to save all the work
        /// </summary>
        public String ProjectFullPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the collecion of pieces
        /// </summary>
        public PieceCollection Pieces
        {
            get
            {
                return _pieces;
            }
        }

        #endregion

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

            // Updating main piece list
            if (this._pieces != null)
            {
                switch (action)
                {
                    case ActionEnum.DELETED:
                        this._pieces.PieceList.Remove(piece);
                        break;
                    case ActionEnum.CREATED:
                        this._pieces.PieceList.Add(piece);
                        break;
                }
            }
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
        /// Binds all the events of a hook
        /// </summary>
        /// <param name="newHook"></param>
        private void BindEvents(HookControl newHook)
        {
            #region Entries validation

            if (newHook == null)
            {
                throw new ArgumentNullException("newHook");
            }

            #endregion

            newHook.BeenSelected += NewHook_BeenSelected;
            newHook.Deleting += NewHook_Deleting;
            newHook.PositionChanged += NewHook_PositionChanged;
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
        /// Adds a new hook
        /// </summary>
        /// <param name="e"></param>
        private void AddNewHook(MouseEventArgs e)
        {
            HookControl newHook = new HookControl();
            this.BindEvents(newHook);

            newHook.Top = e.Y - newHook.Height / 2;
            newHook.Left = e.X - newHook.Width / 2;
            newHook.Pair = this._lastHook;
            newHook.CreatePoint();

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
                        Picture.GetInstance(this.imgDraft.ImageLocation),
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
        /// Loads the project file
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadProject(string fileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            #endregion

            try
            {
                this.ParentForm.Cursor = Cursors.WaitCursor;

                var pieces = PieceCollection.Load(fileName);
                this.LoadPieces(pieces);

                Settings.Default.lastProjectFolder = fileName;
                Settings.Default.Save();
            }
            finally
            {
                this.ParentForm.Cursor = Cursors.Default;
                Picture.ClearCache();
            }
        }

        /// <summary>
        /// Selects a piece
        /// </summary>
        /// <param name="piece"></param>
        public void SelectPiece(Piece piece)
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
        /// Loads the draft picture
        /// </summary>
        /// <param name="draftPicture"></param>
        public void LoadDraftPicture(string draftPicture)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(draftPicture))
            {
                throw new ArgumentNullException("draftPicture");
            }

            #endregion

            this.imgDraft.Load(draftPicture);
            this._pieces = new PieceCollection(Picture.GetInstance(draftPicture));
            this._hookSet.Clear();
            this._lastHook = null;
            this._lastSettings = null;
            this._pieceSet.Clear();
            this.Visible = true;

            // Saving settings
            Settings.Default.lastDraftFolder = draftPicture;
            Settings.Default.Save();
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

        /// <summary>
        /// Send pieces to Unity
        /// </summary>
        /// <param name="folderDestination"></param>
        public void SendToUnity(string folderDestination)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(folderDestination))
            {
                throw new ArgumentNullException("folderDestination");
            }

            #endregion

            this._pieces.Generate(folderDestination);

            MessageBox.Show("Pieces has been sent with success!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Loads the piece collection
        /// </summary>
        private void LoadPieces(PieceCollection pieces)
        {
            #region Entries validation

            if (pieces == null)
            {
                throw new ArgumentNullException("pieces");
            }

            #endregion

            try
            {
                this.LoadDraftPicture(
                    pieces.ReferencePicture.FullPath);

                this._pieces = pieces;
                this.RebuildHookSet(this._pieces);
            }
            finally
            {
                Picture.ClearCache();
            }
        }

        /// <summary>
        /// Rebuild the hookset, based on the pieces
        /// </summary>
        /// <param name="pieces"></param>
        private void RebuildHookSet(PieceCollection pieces)
        {
            #region Entries validation

            if (pieces == null)
            {
                throw new ArgumentNullException("pieces");
            }

            #endregion

            this.imgDraft.Controls.Clear();

            foreach (Piece piece in pieces.PieceList)
            {
                HookControl mainHook = new HookControl();
                mainHook.Point = piece.PointA;

                HookControl otherHook = new HookControl();
                otherHook.Point = piece.PointB;

                mainHook.Pair = otherHook;
                otherHook.Pair = mainHook;

                this.imgDraft.Controls.AddRange(new Control[2] { mainHook, otherHook });
                mainHook.CreateLines();
                //otherHook.CreateLines();      <--- This can't bee made because mainHook already do that (we would have repeated lines)

                this._hookSet.Add(mainHook);
                this._pieceSet.Add(mainHook, piece);

                this.BindEvents(mainHook);
                this.BindEvents(otherHook);
            }
        }
    }
}
