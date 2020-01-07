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
        private int _caretHorizontalDistance;

        public void LocateCaretAt(Point point)
        {
            var pointIsInContentArea = ContentArea.Contains(point);
            HasFocus = _caretClocationMustBeCalculated = pointIsInContentArea;
            _caretHorizontalDistance = point.X - ContentArea.Left;
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
                CalculateCaretLocationIfNeeded(drawContext);

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
        
        private void CalculateCaretLocationIfNeeded(ICoreUIDrawContext drawContext)
        {
            if (_caretClocationMustBeCalculated)
            {
                CaretLocation = LocateCaret(drawContext, _caretHorizontalDistance, Value);
                _caretClocationMustBeCalculated = false;
            }
        }

        private static int LocateCaret(ICoreUIDrawContext context, int distance, string text, int addedValue = 0)
        {
            if(distance <= 0 || string.IsNullOrEmpty(text))
            {
                return 0;
            }

            var foundCaret = text.Length / 2;
            var leftTex = text.Substring(0, foundCaret);
            var leftMeasure = context.MeasureText(leftTex).Width;

            var diff = leftMeasure - distance;
            
            if (diff > 3)
            {
                return LocateCaret(context, distance, leftTex, addedValue);
            }

            if (diff < -3)
            {
                return LocateCaret(context, distance - leftMeasure, text.Substring(foundCaret), addedValue + foundCaret);
            }

            return foundCaret + addedValue;
        }
    }
}
