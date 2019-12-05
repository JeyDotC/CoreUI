using System;
using System.Collections.Generic;
using System.Text;
using CoreUI.Styles;

namespace CoreUI.Dom.Styles
{
    public class Style
    {
        public FontStyles FontStyles { get; set; } = FontStyles.Default;

        public Box Padding { get; set; } = Box.None;

        public Box Margin { get; set; } = Box.None;

        public BorderBox Border { get; set; } = BorderBox.None;

        public PaintStyle Background { get; set; } = PaintStyle.None;

        public DisplayStyle Display { get; set; }

        public LengthHint Width { get; set; }

        public LengthHint Height { get; set; }
    }
}
