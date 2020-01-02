using CoreUI.Styles;
using System;
using System.Drawing;

namespace CoreUI.Primitives
{
    public class TextBox
    {
        public Rectangle ContentArea { get; set; }

        public PaintStyle Background { get; set; } = Color.White;

        public FontStyles Font { get; set; } = FontStyles.Default;

        public string Value { get; set; } = string.Empty;

        public void Draw(ICoreUIDrawContext drawContext)
        {
            drawContext.Save();
            drawContext.FillStyle = Background;
            drawContext.Font = Font;

            drawContext
                .Rect(ContentArea)
                .Fill()
                .Clip()
                .FillText(Value, ContentArea.Location);

            drawContext.Restore();
        }
    }
}
