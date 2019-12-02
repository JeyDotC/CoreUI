using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Styles
{
    public struct FontStyles
    {
        public static FontStyles Default { get; } = new FontStyles {
            FontSize = 12,
            FontColor = Color.Black,
            FontFamily = "Arial",
        };

        public string FontFamily { get; set; }

        public int FontSize { get; set; }

        public Color FontColor { get; set; }

        public FontStyles(FontStyles font): this()
        {
            FontFamily = font.FontFamily;
            FontColor = font.FontColor;
            FontSize = font.FontSize;
        }
    }
}
