using SkiaSharp;
using System;

namespace System.Drawing
{
    internal static class DrawingExtensionMethods
    {
        public static SKColor ToSKColor(this Color color) => new SKColor(color.R, color.G, color.B, color.A);

        public static SKPoint ToSKPoint(this Point point) => new SKPoint(point.X, point.Y);

        public static SKSize ToSKSize(this Size size) => new SKSize(size.Width, size.Height);

        public static SKRect ToSKRect(this Rectangle rectangle) => new SKRect(
            rectangle.X,
            rectangle.Y,
            rectangle.X + rectangle.Width,
            rectangle.Y + rectangle.Height
        );
    }
}
