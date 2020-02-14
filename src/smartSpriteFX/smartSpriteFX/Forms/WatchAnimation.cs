using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        static float FramesPerSec { get; set; }

        public WatchAnimation()
        {
            InitializeComponent();
        }
        public WatchAnimation(string uri)
        {
            InitializeComponent();
            _uri = uri;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.linkLabel1.Text = _uri;
            this.linkLabel1.Links.Add(0, _uri.Length, _uri);

            this.btnPlay.Text = ">";

            _command = PlayModeEnum.STOPPED;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            switch (_command)
            {
                case PlayModeEnum.RUNNING:
                    // Pause
                    this.btnPlay.Text = "||";
                    _command = PlayModeEnum.PAUSED;
                    break;
                case PlayModeEnum.STOPPED:
                    // Play
                    this.btnPlay.Text = ">";
                    if (!this.backgroundWorker1.IsBusy)
                    {
                        _command = PlayModeEnum.RUNNING;
                        FramesPerSec = 1 / (float)this.txtFramesPerSec.Value;

                        this.backgroundWorker1.RunWorkerAsync(
                            new State
                            {
                                URI = _uri,
                            });
                    }
                    this._playModeEnum = PlayModeEnum.RUNNING;
                    break;
                case PlayModeEnum.PAUSED:
                    // Play
                    this.btnPlay.Text = ">";
                    this._playModeEnum = PlayModeEnum.RUNNING;
                    _command = PlayModeEnum.RUNNING;
                    break;
                default:
                    throw new NotSupportedException(this._playModeEnum.ToString());
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            State state = (State)e.Argument;
            BackgroundWorker worker = (BackgroundWorker)sender;
            var files = Directory.GetFiles(state.URI);

            int bookmark = 0;

            while (_command == PlayModeEnum.RUNNING || _command == PlayModeEnum.PAUSED)
            {
                for (int i = bookmark; i < files.Length; i++)
                {
                    if (_command != PlayModeEnum.RUNNING) break;
                    bookmark = i;

                    var file = files[i];
                    worker.ReportProgress(
                        i / files.Length * 100, 
                        new FramePointer
                        {
                            FileName = file,
                            Frame = i + 1,
                        });

                    Thread.Sleep((int)(1000 * FramesPerSec));
                }
                if (_command == PlayModeEnum.RUNNING) bookmark = 0;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FramePointer state = (FramePointer)e.UserState;
            this.pictureBox1.Image = Image.FromFile(state.FileName);
            this.lblFrame.Text = state.Frame.ToString();
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
            FramesPerSec = 1 / (float)this.txtFramesPerSec.Value;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}
