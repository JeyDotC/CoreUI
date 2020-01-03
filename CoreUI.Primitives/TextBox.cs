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

        private bool _caretClocationMustBeCalculated = false;
        private Point _pointToCalculateCaret;

        public void LocateCaretAt(Point point)
        {
            _caretClocationMustBeCalculated = ContentArea.Contains(point);
            _pointToCalculateCaret = point;
        }
        
        public void Draw(ICoreUIDrawContext drawContext)
        {
            var value = Value ?? string.Empty;

            drawContext.Save();

            drawContext.FillStyle = Background;
            drawContext.Font = Font;
            drawContext
                .Rect(ContentArea)
                .Fill()
                .FillText(value, ContentArea.Location);

            if (HasFocus)
            {
                var fullTextWidth = drawContext.MeasureText(value).Width;

                if (_caretClocationMustBeCalculated)
                {

                }

                var caretDistanceToLeftSide = drawContext.MeasureText(value.Substring(0, CaretLocation)).Width;
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
