using smartSuite.smartSpriteFX.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Forms
{
    public partial class WatchAnimation : Form
    {
        private string _uri;
        PlayModeEnum _playModeEnum = PlayModeEnum.STOPPED;
        static PlayModeEnum _command = PlayModeEnum.STOPPED;
        static float _timePerFrame;
        static FramePointer _currentPointer;

        public WatchAnimation()
        {
            InitializeComponent();
        }
        public WatchAnimation(string uri)
        {
            InitializeComponent();
            _uri = uri;

            _command = PlayModeEnum.STOPPED;
            _currentPointer = null;

            this.txtFramesPerSec.Value = (decimal)Settings.Default.lastFramePerSec;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.linkLabel1.Text = _uri;
            this.linkLabel1.Links.Add(0, _uri.Length, _uri);

            this.btnPlay.Text = ">";
            this.pnlEdit.Visible = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.pnlEdit.Visible = false;

            if(this.btnPlay.Text == ">")
            {
                _command = PlayModeEnum.RUNNING;
            }
            else
            {
                _command = PlayModeEnum.PAUSED;
            }

            switch (_command)
            {
                case PlayModeEnum.RUNNING:
                    // Play
                    this._playModeEnum = PlayModeEnum.RUNNING;
                    if (!this.backgroundWorker1.IsBusy)
                    {
                        _timePerFrame = 1 / (float)this.txtFramesPerSec.Value;

                        this.backgroundWorker1.RunWorkerAsync(
                            new State
                            {
                                URI = _uri,
                            });
                    }
                    break;
                case PlayModeEnum.STOPPED:
                case PlayModeEnum.PAUSED:
                    // Play
                    this._playModeEnum = PlayModeEnum.PAUSED;
                    break;
                default:
                    throw new NotSupportedException(_command.ToString());
            }

            switch (this._playModeEnum)
            {
                case PlayModeEnum.RUNNING:
                    this.btnPlay.Text = "||";
                    this.pnlEdit.Visible = false;
                    break;
                case PlayModeEnum.STOPPED:
                case PlayModeEnum.PAUSED:
                    this.btnPlay.Text = ">";
                    this.pnlEdit.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            State state = (State)e.Argument;
            BackgroundWorker worker = (BackgroundWorker)sender;
            List<string> files = GetFrames(state.URI);

            int bookmark = 0;

            while (_command == PlayModeEnum.RUNNING || _command == PlayModeEnum.PAUSED)
            {
                for (int i = bookmark; i < files.Count; i++)
                {
                    if (_command != PlayModeEnum.RUNNING) break;
                    bookmark = i;

                    var file = files[i];

                    _currentPointer =
                        new FramePointer
                        {
                            FileName = file,
                            Frame = i + 1,
                        };

                    worker.ReportProgress(
                        i / files.Count * 100, _currentPointer);

                    Thread.Sleep((int)(1000 * _timePerFrame));
                }
                if (_command == PlayModeEnum.RUNNING) bookmark = 0;
            }
        }

        private List<string> GetFrames(string uri)
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(uri, "*.png"));
            files.AddRange(Directory.GetFiles(uri, "*.jpg"));
            files.AddRange(Directory.GetFiles(uri, "*.jpeg"));
            files.AddRange(Directory.GetFiles(uri, "*.bmp"));
            files.AddRange(Directory.GetFiles(uri, "*.filtered.png"));
            return files;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.pictureBox1.Image != null) this.pictureBox1.Image.Dispose();

            FramePointer state = (FramePointer)e.UserState;
            this.pictureBox1.Image = LoadImage(state.FileName);
            this.txtFrame.Value = state.Frame;
            this.lblFile.Text = Path.GetFileName(state.FileName);
        }

        private Image LoadImage(string uri)
        {
            var file = Image.FromFile(uri);
            var returnValue = (Image)file.Clone();
            file.Dispose();
            return returnValue;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        enum PlayModeEnum
        {
            STOPPED,
            RUNNING,
            PAUSED
        }

        class State
        {
            public string URI { get; set; }
        }

        class FramePointer
        {
            public string FileName { get; set; }
            public int Frame { get; set; }
        }

        private void txtFramesPerSec_ValueChanged(object sender, EventArgs e)
        {
            _timePerFrame = 1 / (float)this.txtFramesPerSec.Value;
            Settings.Default.lastFramePerSec = (float)this.txtFramesPerSec.Value;
            Settings.Default.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image != null) this.pictureBox1.Image.Dispose();

            ProcessStartInfo startInfo = new ProcessStartInfo(_currentPointer.FileName);
            Process.Start(startInfo);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = this.LoadImage(_currentPointer.FileName);
            this.pictureBox1.Refresh();
        }

        private void txtFrame_ValueChanged(object sender, EventArgs e)
        {
            if(this._playModeEnum == PlayModeEnum.PAUSED)
            {
                try
                {
                    var files = this.GetFrames(this._uri);
                    var file = files[(int)this.txtFrame.Value - 1];

                    _currentPointer =
                        new FramePointer
                        {
                            FileName = file,
                            Frame = (int)this.txtFrame.Value - 1,
                        };

                    this.lblFile.Text = Path.GetFileName(file);

                    if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                    pictureBox1.Image = LoadImage(file);
                    pictureBox1.Refresh();
                }
                catch
                {
                    this.txtFrame.Value = 1;
                }
            }
        }
    }
}
