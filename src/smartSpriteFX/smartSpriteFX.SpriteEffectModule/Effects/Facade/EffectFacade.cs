// Decompiled with JetBrains decompiler
// Type: smartSuite.smartSpriteFX.Effects.Facade.EffectFacade
// Assembly: smartSpriteFX.SpriteEffectModule, Version=1.0.0.1, Culture=neutral, PublicKeyToken=ce706f4351ff1c90
// MVID: E6BC09AE-E985-4C4F-953A-4846430587BE
// Assembly location: C:\Users\mgodo\Documents\Repositorio GIT\smartSpriteFX\src\smartSpriteFX\smartSpriteFX.SpriteEffectModule\obj\Release\smartSpriteFX.SpriteEffectModule.dll

using smartSuite.smartSpriteFX.Animations;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Effects.FilterEngine;
using smartSuite.smartSpriteFX.Effects.Filters;
using System.Drawing;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Effects.Facade
{
  public static class EffectFacade
  {
    public static bool DoNotPutFrameIndex
    {
      get
      {
        return FilterCollection.DoNotPutFrameIndex;
      }
      set
      {
        FilterCollection.DoNotPutFrameIndex = value;
      }
    }

    public static FrameIterator GetIterator()
    {
      return EffectEngine.GetIterator();
    }

    public static Image UpdatePreviewBoard()
    {
      return EffectEngine.UpdatePreviewBoard();
    }

    public static T FindFilter<T>() where T : IEffectFilter
    {
      return EffectEngine.FindFilter<T>();
    }

    public static TransparentBackgroundFilter GetTransparentBackgroundFilter()
    {
      return EffectEngine.GetTransparentBackgroundFilter();
    }

    public static Control.ControlCollection GetControlCollectionFromPreviewBoard()
    {
      return EffectEngine.GetPreviewBoard().Controls;
    }

    public static void RegisterFilter(IEffectFilter effectFilter, int frameIndex)
    {
      EffectEngine.RegisterFilter(effectFilter, frameIndex);
    }

    public static void UnRegisterFilter(IEffectFilter effectFilter)
    {
      EffectEngine.UnRegisterFilter(effectFilter);
    }

    public static void UpFilterOrder(IEffectFilter filter)
    {
      EffectEngine.UpFilterOrder(filter);
    }

    public static void DownFilterOrder(IEffectFilter filter)
    {
      EffectEngine.DownFilterOrder(filter);
    }
  }
}
