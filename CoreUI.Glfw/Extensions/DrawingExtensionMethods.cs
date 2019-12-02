using SkiaSharp;
using System;

namespace System.Drawing
{
    internal static class DrawingExtensionMethods
    {
        public static SKColor ToSKColor(this Color color) => new SKColor(color.R, color.G, color.B, color.A);
    }
}
