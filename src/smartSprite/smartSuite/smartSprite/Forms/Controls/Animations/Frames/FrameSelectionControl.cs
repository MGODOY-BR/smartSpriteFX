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
using smartSprite.SpriteEffectModule.Animations;

namespace smartSprite.Forms.Controls.Animations.Frames
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
            this._fileList.Clear();
            this._fileList.AddRange(
                Directory.GetFiles(this._path));

            this._fileList.Sort(_animationComparer);

            throw new NotImplementedException();
        }
    }
}
