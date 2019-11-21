using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Dom.Styles
{
    public struct FontStyles
    {
        public static FontStyles Default { get; } = new FontStyles {
            FontSize = 12,
            FontColor = Color.Black
        };

        public string FontFamily { get; set; }

        public int FontSize { get; set; }

        public Color FontColor { get; set; }
    }
}
