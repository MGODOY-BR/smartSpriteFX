// Decompiled with JetBrains decompiler
// Type: smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.CropFilter
// Assembly: smartSpriteFX.SpriteEffectModule, Version=1.0.0.1, Culture=neutral, PublicKeyToken=ce706f4351ff1c90
// MVID: E6BC09AE-E985-4C4F-953A-4846430587BE
// Assembly location: C:\Users\mgodo\Documents\Repositorio GIT\smartSpriteFX\src\smartSpriteFX\smartSpriteFX.SpriteEffectModule\obj\Release\smartSpriteFX.SpriteEffectModule.dll

using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Point = smartSuite.smartSpriteFX.Pictures.Point;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    public class CropFilter : SmartSpriteOriginalFilterBase, ICutFilter
    {
        private TransparentBackgroundFilter _transparentBackgroundFilter = new TransparentBackgroundFilter();
        private CutFilter _cutFilter = new CutFilter();

        /// <summary>
        /// Gets the first point in the original frame used to start the crop point
        /// </summary>
        public Point Reference { get; private set; }

        public Color GetTransparentColor()
        {
            if (this._transparentBackgroundFilter == null)
                return Color.Transparent;
            return this._transparentBackgroundFilter.TransparentColor;
        }

        private static TransparentBackgroundFilter CreateTransparentBackgroundFilter()
        {
            return EffectEngine.GetTransparentBackgroundFilter() ?? new TransparentBackgroundFilter();
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            var copyFrame = frame.Clone();

            this._transparentBackgroundFilter.ApplyFilter(copyFrame, index);
            List<PointInfo> pointInfoList = copyFrame.ListBorder();
            if (pointInfoList.Count == 0) return false;

            #region New calculation

            float minY = pointInfoList.Where(p => this._transparentBackgroundFilter.TransparentColor != p.Color).Min<PointInfo>(p => p.Y);
            float minX = pointInfoList.Where(p => this._transparentBackgroundFilter.TransparentColor != p.Color).Min<PointInfo>(p => p.X);

            float maxY = pointInfoList.Where(p => this._transparentBackgroundFilter.TransparentColor != p.Color).Max<PointInfo>(p => p.Y);
            float maxX = pointInfoList.Where(p => this._transparentBackgroundFilter.TransparentColor != p.Color).Max<PointInfo>(p => p.X);

            Point minPoint = new Point(minX, minY);
            Point maxPoint = new Point(maxX, maxY);

            #endregion

            // this.Reference = new Point(num1, num3);
            this.Reference = minPoint;

            this._cutFilter.SetPoint(minPoint, maxPoint);
            this._cutFilter.ApplyFilter(copyFrame, index);

            frame.Buffer.CLEAR();
            frame.SetPixel(copyFrame.GetAllPixels());
            frame.Width = copyFrame.Width;
            frame.Height = copyFrame.Height;
            frame.TransparentColor = copyFrame.TransparentColor;
            return true;
        }

        public override void Reset()
        {
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return (IConfigurationPanel)new NoneConfigurationPanelControl();
        }

        public override Identification GetIdentification()
        {
            Identification identification = base.GetIdentification();
            identification.SetName("Crop");
            identification.SetDescription("A filter used to automatically maintain just the image inside of frame, removing the empty spaces in frame");
            identification.SetGroup("Picture");
            return identification;
        }
    }
}
