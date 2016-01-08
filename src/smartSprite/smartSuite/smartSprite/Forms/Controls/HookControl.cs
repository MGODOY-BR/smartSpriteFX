using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSprite.Utilities;
using smartSprite.Forms.Controls.HookState;

namespace smartSprite.Forms.Controls
{
    public partial class HookControl : UserControl, IRemarkable, IDestroyable
    {
        /// <summary>
        /// It´s a respective point in the model
        /// </summary>
        public smartSuite.smartSprite.Pictures.Point Point { get; set; }

        /// <summary>
        /// Gets or sets the current item as selected
        /// </summary>
        public bool Selected { get; private set; }

        /// <summary>
        /// Sets or gets the pair of huck
        /// </summary>
        public HookControl Pair
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the date when huck was created
        /// </summary>
        private DateTime _createdWhen;

        /// <summary>
        /// It´s the tracked horizontal line by the huck
        /// </summary>
        public LineControl LineHorizontal { get; set; }

        /// <summary>
        /// It´s the tracked vertical line by the huck
        /// </summary>
        public LineControl LineVertical { get; set; }

        public HookControl()
        {
            InitializeComponent();

            this.MouseMove += HuckControl_MouseMove;
            this.GotFocus += HuckControl_GotFocus;
            this.LostFocus += HuckControl_LostFocus;
            this.KeyDown += HookControl_KeyDown;

            this._createdWhen = DateTime.Now;
            this.Point = new smartSuite.smartSprite.Pictures.Point(this.Left, this.Top);
        }

        #region Delegates

        /// <summary>
        /// Occurs before deleting
        /// </summary>
        public event EventHandler<HookEventArgs> Deleting;

        /// <summary>
        /// Throws the respective event
        /// </summary>
        private void OnDeleting()
        {
            #region Entries validation

            if (this.Deleting == null)
            {
                throw new NotImplementedException("this.Deleting");
            }

            #endregion

            this.Deleting(
                this,
                new HookEventArgs
                {
                    MainHook = this.GetOlderHuckFromPair()
                });
        }

        #endregion

        private void HookControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:          // ESC

                    this.Mark(false);
                    break;

                case Keys.Delete:
                    this.OnDeleting();
                    break;
            }
        }

        /// <summary>
        /// Creates the lines between the pairs
        /// </summary>
        public void CreateLines()
        {
            #region Entries validation

            if (this.Pair == null)
            {
                return;
            }

            #endregion

            /*
            Shape:

            A-------C
            |       |
            D-------B
            
            */
            HookControl older = this.GetOlderHuckFromPair();
            HookControl newer = this.GetNewerHuckFromPair();

            older.LineHorizontal = new LineControl(older);
            older.LineVertical = new LineControl(older);
            newer.LineHorizontal = new LineControl(newer);
            newer.LineVertical = new LineControl(newer);

            var AC = older.LineHorizontal;
            var AD = older.LineVertical;
            var CB = newer.LineVertical;
            var DB = newer.LineHorizontal;

            this.Pair.LineHorizontal = AC;
            this.Pair.LineVertical = AD;
            this.LineHorizontal = DB;
            this.LineVertical = CB;

            ResizeLines(older, newer);

            this.Parent.Controls.Add(AC);
            this.Parent.Controls.Add(DB);
            this.Parent.Controls.Add(AD);
            this.Parent.Controls.Add(CB);
        }

        /// <summary>
        /// Resizes the lines
        /// </summary>
        private void ResizeLines(HookControl older, HookControl newer)
        {
            Point currentPoint = new Point(newer.Left + (newer.Width / 2), newer.Top + (newer.Height / 2));
            Point pairPoint = new Point(older.Left + (older.Width / 2), older.Top + (older.Height / 2));

            LineControl AC = older.LineHorizontal;
            LineControl AD = older.LineVertical;
            LineControl DB = newer.LineHorizontal;
            LineControl CB = newer.LineVertical;

            AC.Resize(LineControlState.LineControlStyle.Horizontal, MathUtil.SubtractAbsolute(currentPoint.X, pairPoint.X));
            DB.Resize(AC.Style, AC.Width);

            AD.Resize(LineControlState.LineControlStyle.Vertical, MathUtil.SubtractAbsolute(currentPoint.Y, pairPoint.Y));
            CB.Resize(AD.Style, AD.Height);

            AC.Top = MathUtil.GetLower<int>(pairPoint.Y, currentPoint.Y);
            AC.Left = MathUtil.GetLower<int>(pairPoint.X, currentPoint.X);

            DB.Top = AC.Top + AD.Height;
            DB.Left = MathUtil.GetLower<int>(pairPoint.X, currentPoint.X);

            AD.Top = MathUtil.GetLower<int>(pairPoint.Y, currentPoint.Y);
            AD.Left = AC.Left;

            CB.Top = MathUtil.GetLower<int>(pairPoint.Y, currentPoint.Y);
            CB.Left = AC.Left + AC.Width;
        }

        /// <summary>
        /// Gets the older huck from pair
        /// </summary>
        public HookControl GetOlderHuckFromPair()
        {
            #region Entries validation

            if (this.Pair == null)
            {
                return this;
            }

            #endregion

            if (this._createdWhen.CompareTo(this.Pair._createdWhen) == -1)
            {
                return this;
            }
            else
            {
                return this.Pair;
            }
        }

        /// <summary>
        /// Gets the newer huck from pair
        /// </summary>
        public HookControl GetNewerHuckFromPair()
        {
            #region Entries validation

            if (this.Pair == null)
            {
                return this;
            }

            #endregion

            if (this._createdWhen.CompareTo(this.Pair._createdWhen) == 1)
            {
                return this;
            }
            else
            {
                return this.Pair;
            }
        }

        #region Events

        private void HuckControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top += e.Y - this.Height / 2;
                this.Left += e.X - this.Width / 2;

                this.Point.X = this.Left;
                this.Point.Y = this.Top;

                if (this.Pair != null)
                {
                    HookControl older = this.GetOlderHuckFromPair();
                    HookControl newer = this.GetNewerHuckFromPair();

                    this.ResizeLines(
                        older,
                        newer);
                }
            }
        }

        private void HuckControl_GotFocus(object sender, EventArgs e)
        {
            this.Mark(true);
        }

        private void HuckControl_LostFocus(object sender, EventArgs e)
        {
            this.Mark(false);
        }

        #endregion

        #region IRemarkable operations

        public void Mark(bool bold)
        {
            #region Entries validation

            if (bold == this.Selected)
            {
                return;
            }

            #endregion

            this.Selected = bold;

            if (bold)
            {
                this.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                this.BorderStyle = BorderStyle.None;
            }

            this.LineHorizontal.Mark(bold);
            this.LineVertical.Mark(bold);

            if (this.Pair != null)
            {
                this.Pair.Mark(bold);
            }
        }

        #endregion

        #region IDestroyable operations

        public void DestroyYourSelf()
        {
            this.Parent.Controls.Remove(this.LineHorizontal);
            this.Parent.Controls.Remove(this.LineVertical);
            this.Parent.Controls.Remove(this);

            if (this.GetOlderHuckFromPair() == this)
            {
                if (this.Pair != null)
                {
                    this.Pair.DestroyYourSelf();
                }
            }
        }

        #endregion
    }
}
