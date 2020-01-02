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

        public bool HasFocus { get; set; }

        public int CaretLocation { get; set; } = 0;
        
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

            if (HasFocus)
            {
                var caretDistanceToLeftSide = drawContext.MeasureText(Value.Substring(0, CaretLocation)).Width;
                var caretDisplacement = new Size(caretDistanceToLeftSide, 0);

                drawContext.FillStyle = Color.Black;
                drawContext.Rect(new Rectangle
                {
                    Location = ContentArea.Location + caretDisplacement,
                    Size = new Size(1, ContentArea.Height)
                }).Fill();
            }

            drawContext.Restore();
        }
    }
}
