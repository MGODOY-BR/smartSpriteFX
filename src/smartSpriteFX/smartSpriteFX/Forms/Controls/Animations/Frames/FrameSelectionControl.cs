using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using smartSuite.smartSpriteFX.SpriteEffectModule.Animations;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Animations;

namespace smartSuite.smartSpriteFX.Forms.Controls.Animations.Frames
{
    /// <summary>
    /// It´s a control to selecion of frame
    /// </summary>
    public partial class FrameSelectionControl : UserControl
    {
        /// <summary>
        /// It´s a path of animation folder
        /// </summary>
        private string _path;

        /// <summary>
        /// It´s a list of sorted loaded file
        /// </summary>
        private List<string> _fileList = new List<string>();

        /// <summary>
        /// It happens when a frame is selected
        /// </summary>
        public event EventHandler<FrameSelectionEventArgs> SelectingFrame;

        /// <summary>
        /// It happens when an frame has issues to me loaded
        /// </summary>
        public event EventHandler<FrameSelectionErrorEventArgs> LoadingFrameError;

        /// <summary>
        /// It's the last selected frame
        /// </summary>
        private PictureBox _lastSelectedFrame;

        public FrameSelectionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the path
        /// </summary>
        /// <param name="path"></param>
        public void SetPath(string path)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            #endregion

            this._path = path;
        }

        /// <summary>
        /// Loads the control content with thumbnails
        /// </summary>
        public void LoadThumbNails()
        {
            flowLayoutPanel1.Controls.Clear();
            this._fileList.Clear();
            this._fileList.AddRange(
                FrameIterator.GetFileList(this._path));

            this.numCurrentFrame.Minimum = 1;
            this.numCurrentFrame.Maximum = this._fileList.Count;
            this.lblTotal.Text = String.Format("(Total: {0})", this._fileList.Count);

            for (int i = 0; i < this._fileList.Count; i++)
            {
                #region Entries validation

                if (i > 200)
                {
                    break;
                }

                #endregion

                var fileItem = this._fileList[i];

                Stream imageStreamSource = new FileStream(fileItem, FileMode.Open, FileAccess.Read, FileShare.Read);
                try
                {
                    using (var image = Image.FromStream(imageStreamSource, true))
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Height = 50;
                        pictureBox.Width = 50;
                        pictureBox.Image =
                            image.GetThumbnailImage(
                                pictureBox.Width,
                                pictureBox.Height,
                                new Image.GetThumbnailImageAbort(delegate ()
                                {
                                    return false;
                                }),
                                IntPtr.Zero);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.BorderStyle = BorderStyle.FixedSingle;
                        pictureBox.Tag = new FrameSelectionEventArgs
                        {
                            FilePath = fileItem,
                            FrameIndex = i
                        };
                        pictureBox.Click += PictureBox_Click;

                        Label title = new Label();
                        title.Text = Path.GetFileName(fileItem);
                        title.ForeColor = Color.White;
                        title.BorderStyle = BorderStyle.FixedSingle;
                        title.Click += delegate (Object sender, EventArgs e)
                        {
                            this.OnSelectingFrame(pictureBox);
                        };

                        Panel panel = new Panel();
                        panel.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(title);
                        title.Dock = DockStyle.Right;
                        panel.Controls.Add(pictureBox);

                        panel.Width = pictureBox.Width + title.Width; 
                        flowLayoutPanel1.Controls.Add(panel);
                    }
                }
                catch(Exception ex)
                {
                    if (LoadingFrameError != null)
                    {
                        this.LoadingFrameError(
                            this,
                            new FrameSelectionErrorEventArgs
                            {
                                Exception = ex,
                                FilePath = fileItem,
                                FrameIndex = i,
                                Message = String.Format("Skip frame {0} - It wasn't possible to load {1}", i + 1, fileItem)
                            });
                    }
                }
            }
        }

        /// <summary>
        /// It ocurrs when user click in picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Click(object sender, EventArgs e)
        {
            #region Entries validation

            if (this.SelectingFrame == null)
            {
                return;
            }

            #endregion

            OnSelectingFrame((PictureBox)sender);
        }

        /// <summary>
        /// Occurs when a frame was selecting.
        /// </summary>
        /// <param name="sender"></param>
        private void OnSelectingFrame(PictureBox pictureBox)
        {
            try
            {
                pictureBox.FindForm().Cursor = Cursors.WaitCursor;

                FrameSelectionEventArgs eventArgs = (FrameSelectionEventArgs)pictureBox.Tag;
                EffectEngine.GetIterator().MoveTo(eventArgs.FrameIndex);
                EffectEngine.SetSourcePreviewImage(
                    EffectEngine.GetIterator().GetCurrent());

                this.SelectingFrame(pictureBox, eventArgs);
                this.numCurrentFrame.Value = eventArgs.FrameIndex + 1;

                if (this._lastSelectedFrame != null)
                {
                    this._lastSelectedFrame.BorderStyle = BorderStyle.FixedSingle;
                }

                this._lastSelectedFrame = pictureBox;
                pictureBox.BorderStyle = BorderStyle.Fixed3D;
            }
            finally
            {
                pictureBox.FindForm().Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Clears the frames
        /// </summary>
        public void ClearFrames()
        {
            this._fileList.Clear();
            flowLayoutPanel1.Controls.Clear();
        }

        private void numCurrentFrame_Validated(object sender, EventArgs e)
        {
            var frameIndex = (int)numCurrentFrame.Value - 1;
            var frame = flowLayoutPanel1.Controls[frameIndex] as PictureBox;

            this.OnSelectingFrame(frame);
        }

        private void numCurrentFrame_Click(object sender, EventArgs e)
        {
            var frameIndex = (int)numCurrentFrame.Value - 1;
            var frame = flowLayoutPanel1.Controls[frameIndex] as PictureBox;

            this.OnSelectingFrame(frame);
        }

        private void numCurrentFrame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var frameIndex = (int)numCurrentFrame.Value - 1;
                var frame = flowLayoutPanel1.Controls[frameIndex] as PictureBox;

                this.OnSelectingFrame(frame);
            }
        }
    }
}
