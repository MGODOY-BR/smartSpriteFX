// Decompiled with JetBrains decompiler
// Type: smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.BrightFilter
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
  public class BrightFilter : SmartSpriteOriginalFilterBase, IScaleOrientedObject
  {
    public float Scale { get; set; }

    public override bool ApplyFilter(Picture frame, int index)
    {
      TransparentBackgroundFilter backgroundFilter = EffectEngine.GetTransparentBackgroundFilter();
      CropFilter filter = EffectEngine.FindFilter<CropFilter>();
      List<PointInfo> allPixels = frame.GetAllPixels();
      for (int index1 = 0; index1 < allPixels.Count; ++index1)
      {
        PointInfo pointInfo = allPixels[index1];
        Color color1;
        if (backgroundFilter != null)
        {
          color1 = backgroundFilter.TransparentColor;
          int argb1 = color1.ToArgb();
          color1 = pointInfo.Color;
          int argb2 = color1.ToArgb();
          if (argb1 == argb2)
            continue;
        }
        if (filter != null)
        {
          color1 = filter.GetTransparentColor();
          int argb1 = color1.ToArgb();
          color1 = pointInfo.Color;
          int argb2 = color1.ToArgb();
          if (argb1 == argb2)
            continue;
        }
        int scale = (int) this.Scale;
        color1 = pointInfo.Color;
        int a = (int) color1.A;
        color1 = pointInfo.Color;
        int red = this.ApplyBright((int) color1.R, scale);
        color1 = pointInfo.Color;
        int green = this.ApplyBright((int) color1.G, scale);
        color1 = pointInfo.Color;
        int blue = this.ApplyBright((int) color1.B, scale);
        Color color2 = Color.FromArgb(a, red, green, blue);
        frame.ReplacePixel((int) ((Pictures.Point) pointInfo).X, (int) ((Pictures.Point) pointInfo).Y, color2);
      }
      return true;
    }

    private int ApplyBright(int color, int bright)
    {
      int num = color + bright;
      if (num < 0)
        num = 0;
      if (num > 254)
        num = 254;
      return num;
    }

    public override void Reset()
    {
      this.Scale = 0.0f;
    }

    public override IConfigurationPanel ShowConfigurationPanel()
    {
      return (IConfigurationPanel) new ScaleConfigurationPanelControl(-255f, (float) byte.MaxValue);
    }

    public override Identification GetIdentification()
    {
      Identification identification = base.GetIdentification();
      identification.SetName("Bright");
      identification.SetDescription("A filter used to enfisize the bright");
      identification.SetGroup("Color");
      return identification;
    }
  }
}
