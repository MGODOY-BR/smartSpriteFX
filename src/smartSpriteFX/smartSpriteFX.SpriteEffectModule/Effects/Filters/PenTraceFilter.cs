using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter which keep just pen traces
    /// </summary>
    public class PenTraceFilter : SmartSpriteOriginalFilterBase
    {
        /// <summary>
        /// It's the new trace color
        /// </summary>
        private Color? _newTraceColor;

        /// <summary>
        /// It's a factor that enfasizes the darkness of pen
        /// </summary>
        private int _extraDarknessFactor;

        /// <summary>
        /// It's a comparer of color
        /// </summary>
        private PenColorComparer _penColorComparer = new PenColorComparer();

        /// <summary>
        /// It's the new trace color
        /// </summary>
        public Color? NewTraceColor { get => _newTraceColor; set => _newTraceColor = value; }

        /// <summary>
        /// It's a factor that enfasizes the darkness of pen
        /// </summary>
        public int ExtraDarknessFactor { get => _extraDarknessFactor; set => _extraDarknessFactor = value; }

        public override bool ApplyFilter(Picture frame, int index)
        {
            this._penColorComparer.ExtraDarkness = _extraDarknessFactor;

            Picture newFrame = new Picture();

            foreach (var pointItem in frame.GetAllPixels())
            {
                if(this._penColorComparer.IsPencilColor(pointItem.Color))
                {
                    Color newColor = pointItem.Color;
                    if(_newTraceColor != null)
                    {
                        newColor = _newTraceColor.Value;
                    }

                    newFrame.SetPixel((int)pointItem.X, (int)pointItem.Y, newColor);
                }
            }

            frame.ReleaseBuffer();
            frame.ShareDataWithMe(newFrame);

            return true;
        }

        public override void Reset()
        {
            this._extraDarknessFactor = 0;
            this._newTraceColor = null;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new PenTraceConfigurationPanelControl();
        }

        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Pen Trace Filter");
            identification.SetDescription("Represents a filter used to keep just the trace of pen");
            identification.SetGroup("Art");

            return identification;
        }
    }
}
