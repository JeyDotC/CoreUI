using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUI.Styles
{
    static class StylesExtensionMethods
    {
        public static SKPaint ToSkPaint(this FontStyles fontStyles)
        {
            return new SKPaint
            {
                TextSize = fontStyles.FontSize,
                IsStroke = false,
                Typeface = SKTypeface.FromFamilyName(fontStyles.FontFamily),
            };
        }
    }
}
