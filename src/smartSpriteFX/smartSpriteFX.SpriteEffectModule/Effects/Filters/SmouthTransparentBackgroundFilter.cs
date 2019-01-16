using smartSuite.smartSpriteFX.Effects.Facade;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace smartSuite.smartSpriteFX.Effects.Filters
{
    public class SmouthTransparentBackgroundFilter : TransparentBackgroundFilter, IScaleOrientedObject
    {
        private TransparentBackgroundFilter _transparentBackgroundFilter;

        public float Scale
        {
            get;
            set;
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            this._transparentBackgroundFilter =
                EffectFacade.GetTransparentBackgroundFilter();

            if(this._transparentBackgroundFilter != null)
            {
                this._transparentBackgroundFilter = new TransparentBackgroundFilter();
            }

            // Making transparent background
            var result = this._transparentBackgroundFilter.ApplyFilter(frame, index);
            if (!result) return false;

            this.TransparentColor = this._transparentBackgroundFilter.TransparentColor;
            var borderList = frame.ListBorder();

            var alphaChangingDelegate = new Func<int, int, int, bool>(delegate (int x, int y, int newAlpha)
            {
                var pixel = frame.GetPixel(x, y);
                if (pixel == null || pixel == Color.Transparent) return false;

                Color newPixel =
                    Color.FromArgb(
                        newAlpha,
                        pixel.Value);

                frame.ReplacePixel(x, y, newPixel);

                return true;
            });

            var pointScanDelegate = new Action<PointInfo>(delegate (PointInfo borderItem)
            {
                int newAlpha = 0;
                float scale = this.Scale;
                int step = 255 / (int)scale;

                for (int x = (int)borderItem.X; x < borderItem.X + scale; x++)
                {
                    for (int y = (int)borderItem.Y; y < borderItem.Y + scale; y++)
                    {
                        newAlpha += step;

                        if (newAlpha > 255) break;
                        alphaChangingDelegate(x, y, newAlpha);
                    }

                    #region Upside-down extra-scan (disabled for awhile)

                    //newAlpha = 255;
                    //for (int y = (int)borderItem.Y + (int)scale; y > borderItem.Y; y--)
                    //{
                    //    newAlpha -= step;

                    //    if (newAlpha < 0) break;
                    //    alphaChangingDelegate(x, y, newAlpha);
                    //}

                    #endregion
                }
            });

            foreach (var borderItem in borderList)
            {
                pointScanDelegate(borderItem);
            }

            return true;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ScaleConfigurationPanelControl(1, 40);
        }

        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Smouth Transparent Background");
            identification.SetDescription("A filter which detects the background and turn it to transparent, allowing hide the borders");
            identification.SetGroup("Background");

            return identification;
        }

        public override void Reset()
        {
            this.Scale = 10;
        }
    }
}