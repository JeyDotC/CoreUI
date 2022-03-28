using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUI.Styles
{
    static class StylesExtensionMethods
    {
        private static readonly IDictionary<string, SKTypeface> _cachedTypeFaces = new Dictionary<string, SKTypeface>();

        public static SKPaint ToSkPaint(this FontStyles fontStyles)
        {
            if (!_cachedTypeFaces.ContainsKey(fontStyles.FontFamily))
            {
                _cachedTypeFaces[fontStyles.FontFamily] = SKTypeface.FromFamilyName(fontStyles.FontFamily);
            }

            return new SKPaint
            {
                TextSize = fontStyles.FontSize,
                IsStroke = false,
                Typeface = _cachedTypeFaces[fontStyles.FontFamily],
            };
        }
    }
}
