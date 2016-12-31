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
        /// It´s a comparer used to sort files
        /// </summary>
        private AnimationComparer _animationComparer = new AnimationComparer();

        /// <summary>
        /// It happens when a frame is selected
        /// </summary>
        public event EventHandler<FrameSelectionEventArgs> SelectingFrame;

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
                Directory.GetFiles(this._path));

            this._fileList.Sort(_animationComparer);

            for(int i = 0; i < this._fileList.Count; i++)
            {
                var fileItem = this._fileList[i];
                var image = Image.FromFile(fileItem);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = image;
                pictureBox.Height = this.Height - 10;
                pictureBox.Width = this.Height;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.Tag = new FrameSelectionEventArgs
                {
                    FilePath = fileItem,
                    FrameIndex = i
                };
                pictureBox.Click += PictureBox_Click;

                flowLayoutPanel1.Controls.Add(pictureBox);
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

            PictureBox pictureBox = (PictureBox)sender;
            FrameSelectionEventArgs eventArgs = (FrameSelectionEventArgs)pictureBox.Tag;
            EffectEngine.GetIterator().MoveTo(eventArgs.FrameIndex);

            this.SelectingFrame(sender, eventArgs);

            if (this._lastSelectedFrame != null)
            {
                this._lastSelectedFrame.BorderStyle = BorderStyle.FixedSingle;
            }

            this._lastSelectedFrame = pictureBox;
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// Clears the frames
        /// </summary>
        public void ClearFrames()
        {
            flowLayoutPanel1.Controls.Clear();
        }
    }
}
