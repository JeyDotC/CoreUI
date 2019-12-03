using System;
using System.Drawing;
using System.Linq;
using CoreUI.Styles;
using SkiaSharp;

namespace CoreUI
{
    internal static class CoreUIExtensionMethods
    {
        public static SKPaint ToSKPaint(this PaintStyle paintStyle)
        {
            var paint = new SKPaint();

            if (paintStyle.Type == PaintStyleType.Solid)
            {
                paint.Color = paintStyle.Color.ToSKColor();
            }
            else if (paintStyle.Type == PaintStyleType.Gradient)
            {
                var gradient = paintStyle.Gradient;

                paint.Shader = gradient.Type switch
                {
                    GradientType.Linear => SKShader.CreateLinearGradient(
                        gradient.Start.ToSKPoint(),
                        gradient.End.ToSKPoint(),
                        gradient.ColorStops.Select(s => s.Color.ToSKColor()).ToArray(),
                        gradient.ColorStops.Select(s => s.Percent).ToArray(),
                        SKShaderTileMode.Repeat
                    ),
                    _ => SKShader.CreateRadialGradient(
                            gradient.Start.ToSKPoint(),
                            gradient.InnerRadius,
                            gradient.ColorStops.Select(s => s.Color.ToSKColor()).ToArray(),
                            gradient.ColorStops.Select(s => s.Percent).ToArray(),
                            SKShaderTileMode.Repeat
                        )
                };

            }

            return paint;
        }
    }
}
