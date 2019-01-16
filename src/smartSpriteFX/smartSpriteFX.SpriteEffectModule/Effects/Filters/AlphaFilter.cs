// Decompiled with JetBrains decompiler
// Type: smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.AlphaFilter
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
using System.Collections.Generic;
using System.Drawing;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
  public class AlphaFilter : SmartSpriteOriginalFilterBase, IScaleOrientedObject
  {
    public float Scale { get; set; }

    public override bool ApplyFilter(Picture frame, int index)
    {
      TransparentBackgroundFilter backgroundFilter = EffectEngine.GetTransparentBackgroundFilter();
      CropFilter filter = EffectEngine.FindFilter<CropFilter>();
      using (List<PointInfo>.Enumerator enumerator = frame.GetAllPixels().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PointInfo current = enumerator.Current;
          Color color1;
          if (backgroundFilter != null)
          {
            color1 = backgroundFilter.TransparentColor;
            int argb1 = color1.ToArgb();
            color1 = current.Color;
            int argb2 = color1.ToArgb();
            if (argb1 == argb2)
              continue;
          }
          if (filter != null)
          {
            color1 = filter.GetTransparentColor();
            int argb1 = color1.ToArgb();
            color1 = current.Color;
            int argb2 = color1.ToArgb();
            if (argb1 == argb2)
              continue;
          }
          int scale = (int) this.Scale;
          color1 = current.Color;
          int r = (int) color1.R;
          color1 = current.Color;
          int g = (int) color1.G;
          color1 = current.Color;
          int a = (int) color1.A;
          Color color2 = Color.FromArgb(scale, r, g, a);
          frame.ReplacePixel((int) ((Pictures.Point) current).X, (int) ((Pictures.Point) current).Y, color2);
        }
      }
      return true;
    }

    public override void Reset()
    {
      this.Scale = (float) byte.MaxValue;
    }

    public override IConfigurationPanel ShowConfigurationPanel()
    {
      return (IConfigurationPanel) new ScaleConfigurationPanelControl(0.0f, (float) byte.MaxValue);
    }

    public override Identification GetIdentification()
    {
      Identification identification = base.GetIdentification();
      identification.SetName("Alpha");
      identification.SetDescription("A filter used to enfisize the transparent color");
      identification.SetGroup("Color");
      return identification;
    }
  }
}
