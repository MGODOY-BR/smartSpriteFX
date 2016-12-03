using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.Utilities;
using smartSuite.smartSpriteFX.Forms.Controls.HookState;
using System.Threading;

namespace smartSuite.smartSpriteFX.Forms.Controls
{
    public partial class HookControl : UserControl, IRemarkable, IDestroyable
    {
        /// <summary>
        /// It´s a respective point in the model
        /// </summary>
        private smartSuite.smartSpriteFX.Pictures.Point _point;

        /// <summary>
        /// It´s a respective point in the model
        /// </summary>
        public smartSuite.smartSpriteFX.Pictures.Point Point
        {
            get
            {
                return this._point;
            }
            set
            {
                this._point = value;

                #region Entries validation

                if (value == null)
                {
                    return;
                }

                #endregion

                this.Left = (int)this._point.X - (this.Width / 2);
                this.Top = (int)this._point.Y - (this.Height / 2);
            }
        }

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
            this.MouseUp += HookControl_MouseUp;
            this.GotFocus += HuckControl_GotFocus;
            this.LostFocus += HuckControl_LostFocus;
            this.KeyDown += HookControl_KeyDown;

            lock(this.GetType())
            {
                this._createdWhen = DateTime.Now;
                Thread.Sleep(1);
            }

            this.CreatePoint();
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

        /// <summary>
        /// Occurs when the hook are selected
        /// </summary>
        public event EventHandler<HookEventArgs> BeenSelected;

        /// <summary>
        /// Occurs when the hook has changed
        /// </summary>
        public event EventHandler<HookEventArgs> PositionChanged;

        /// <summary>
        /// Throws the event
        /// </summary>
        private void OnBeenSelected()
        {
            #region Entries validation

            if (this.BeenSelected == null)
            {
                return;
            }

            #endregion

            this.BeenSelected(this, new HookEventArgs
            {
                MainHook = this.GetOlderHuckFromPair()
            });
        }

        /// <summary>
        /// Throws the event associated
        /// </summary>
        private void OnPositionChanged()
        {
            #region Entries validation

            if (this.PositionChanged == null)
            {
                throw new NotImplementedException();
            }
            this.PositionChanged(this, new HookEventArgs
            {
                MainHook = this.GetOlderHuckFromPair()
            });

            #endregion
        }

        #endregion

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

        /// <summary>
        /// Creates the point
        /// </summary>
        public void CreatePoint()
        {
            this._point = 
                new smartSuite.smartSpriteFX.Pictures.Point(
                    this.Left + (this.Width / 2), 
                    this.Top + (this.Height / 2));
        }

        #region Events
        
        private void HookControl_MouseUp(object sender, MouseEventArgs e)
        {
            #region Entries validation

            if (sender != this)
            {
                return;
            }

            #endregion

            this._point.X = this.Left + (this.Width / 2);
            this._point.Y = this.Top + (this.Height / 2);

            this.OnPositionChanged();
        }

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

        private void HuckControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top += e.Y - this.Height / 2;
                this.Left += e.X - this.Width / 2;

                this._point.X = this.Left;
                this._point.Y = this.Top;

                if (this.Pair != null)
                {
                    RefreshLines();
                }
            }
        }

        /// <summary>
        /// Refreshes lines
        /// </summary>
        public void RefreshLines()
        {
            HookControl older = this.GetOlderHuckFromPair();
            HookControl newer = this.GetNewerHuckFromPair();

            this.ResizeLines(
                older,
                newer);
        }

        private void HuckControl_GotFocus(object sender, EventArgs e)
        {
            this.Mark(true);

            if (this.Selected)
            {
                this.OnBeenSelected();
            }
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

            if (this.LineHorizontal != null)
            {
                this.LineHorizontal.Mark(bold);
            }
            if (this.LineVertical != null)
            {
                this.LineVertical.Mark(bold);
            }

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
