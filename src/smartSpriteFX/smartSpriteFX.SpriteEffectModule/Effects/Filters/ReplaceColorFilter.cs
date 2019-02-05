// Decompiled with JetBrains decompiler
// Type: smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.BlueFilter
// Assembly: smartSpriteFX.SpriteEffectModule, Version=1.0.0.1, Culture=neutral, PublicKeyToken=ce706f4351ff1c90
// MVID: E6BC09AE-E985-4C4F-953A-4846430587BE
// Assembly location: C:\Users\mgodo\Documents\Repositorio GIT\smartSpriteFX\src\smartSpriteFX\smartSpriteFX.SpriteEffectModule\obj\Release\smartSpriteFX.SpriteEffectModule.dll

using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    public class ReplaceColorFilter : SmartSpriteOriginalFilterBase
    {
        /// <summary>
        /// It's a comparer used to compare colors
        /// </summary>
        private ColorEqualityComparer _colorEqualityComparer = new ColorEqualityComparer();

        /// <summary>
        /// It's the new color
        /// </summary>
        public Color NewColor { get; set; } = Color.Transparent;

        /// <summary>
        /// It's a list of color to be replaced
        /// </summary>
        public List<Color> FromColorList { get; set; } = new List<Color>();

        public override bool ApplyFilter(Picture frame, int index)
        {
            if (this.NewColor == Color.Transparent) return false;
            foreach (var colorItem in frame.GetAllColors())
            {
                if (!FromColorList.Exists(c => this._colorEqualityComparer.LooksLikeBySensibility3(c, colorItem))) continue;

                frame.ReplaceColor(colorItem, this.NewColor);
            }
            return true;
        }

        public override void Reset()
        {
            this.FromColorList.Clear();
            this.NewColor = Color.Transparent;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ReplaceColorsPanelControl();
        }

        public override Identification GetIdentification()
        {
            Identification identification = base.GetIdentification();
            identification.SetName("Replace color");
            identification.SetDescription("A filter used to replace colors for another one");
            identification.SetGroup("Color");
            return identification;
        }
    }
}
