using System;
using System.Drawing;

namespace CoreUI.Styles
{
    public class PaintStyle
    {
        public PaintStyleType Type { get; set; }

        public Color Color { get; set; }

        public GradientSpec Gradient { get; set; } = new GradientSpec();
        
        public static implicit operator PaintStyle(Color color) => new PaintStyle
        {
            Type = PaintStyleType.Solid,
            Color = color,
        };

        public static implicit operator PaintStyle(GradientSpec gradient) => new PaintStyle
        {
            Type = PaintStyleType.Gradient,
            Gradient = gradient,
        };

        public PaintStyle() { }

        public PaintStyle(PaintStyle paint)
        {
            Type = paint.Type;
            Color = paint.Color;
            Gradient = new GradientSpec(paint.Gradient);
        }
    }
}
