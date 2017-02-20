using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.Effects.Facade;
using smartSuite.smartSpriteFX.Pictures.PixelPatterns;
using smartSuite.smartSpriteFX.Effects.Core;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// It´s a filter that draws a shadow uppon the image
    /// </summary>
    public class ShadowFilter : SmartSpriteOriginalFilterBase
    {
        /// <summary>
        /// It´s the strenght of sun
        /// </summary>
        private int _sunStrenght;

        /// <summary>
        /// Sets or gets the sun strenght
        /// </summary>
        public int SunStrenght
        {
            get
            {
                return _sunStrenght;
            }
            set
            {
                _sunStrenght = value;
            }
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            #region Entries validation

            if (frame == null)
            {
                throw new ArgumentNullException("frame");
            }
            var transparentBackground = EffectFacade.GetTransparentBackgroundFilter();
            if (transparentBackground == null)
            {
                throw new ApplicationException(
                    this.GetType().Name + " requires a " + new TransparentBackgroundFilter().GetIdentification().GetName() + " placed before it to run");
            }
            if (EffectFacade.FindFilter<CropFilter>() == null && EffectFacade.FindFilter<CutFilter>() == null)
            {
                throw new ApplicationException(
                    this.GetType().Name + " requires " + new CropFilter().GetIdentification().GetName() + 
                    " or " + new CutFilter().GetIdentification().GetName()  + " placed before it to run");
            }

            #endregion

            List<PixelInfo> shadowPixelList = new List<PixelInfo>();

            int percentageHeight = 
                frame.Height / _sunStrenght;

            #region Scan

            for (int y = 0; y < frame.Height; y+= percentageHeight)
            {
                for (int x = 0; x < frame.Width; x++)
                {
                    var pixel = frame.GetPixel(x, y);

                    #region Entries validation

                    if (pixel == null)
                    {
                        continue;
                    }
                    if (pixel.Value.ToArgb().Equals(transparentBackground.TransparentColor.ToArgb()))
                    {
                        continue;
                    }

                    #endregion

                    shadowPixelList.Add(new PixelInfo { X = x, Y = y, Color = pixel.Value });
                }
            }

            #endregion

            #region Preparation

            var shadowPixelGroup = from pixel in shadowPixelList
                                   group pixel by pixel.Y into g
                                   select g.Key;

            var groupList = shadowPixelGroup.ToList();
            groupList.Sort(delegate (int comparing, int compareTo)
            {
                return compareTo.CompareTo(comparing); // <-- Descending order
            });

            var middleShadow = groupList.Count / 2;

            int oldHeight = frame.Height;
            frame.Height += percentageHeight + middleShadow;

            #endregion

            #region Printing

            var groupEnumerator = groupList.GetEnumerator();
            for (int y = oldHeight - middleShadow; y < frame.Height; y++)
            {
                if (!groupEnumerator.MoveNext())
                {
                    break;
                }

                var realY = groupEnumerator.Current;
                var xList = from item in shadowPixelList
                            where item.Y == realY
                            select item;

                foreach (var item in xList)
                {
                    #region Entries validation

                    var existentPixel = frame.GetPixel(item.X, y);
                    if (existentPixel != null && existentPixel != transparentBackground.TransparentColor)
                    {
                        continue;
                    }
                    if (existentPixel == transparentBackground.TransparentColor)
                    {
                        frame.RemovePixel(item.X, y);
                    }

                    #endregion

                    frame.SetPixel(item.X, y, System.Drawing.Color.Black);
                }
            }

            #endregion

            return true;
        }

        public override void Reset()
        {
            _sunStrenght = 20;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ShadowConfigurationPanelControl();
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Shadow");
            identification.SetDescription("A filter which draws a shadow uppon the picture (requires a Transparent Background filter before)");
            identification.SetGroup("Environment");

            return identification;
        }
    }
}
