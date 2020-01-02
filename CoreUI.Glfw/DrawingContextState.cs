using CoreUI.Styles;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Glfw
{
    internal class DrawingContextState
    {
        public SKPath Path { get; set; } = new SKPath();
        public PaintStyle ClearStyle { get; set; } = Color.White;
        public PaintStyle FillStyle { get; set; } = Color.White;
        public PaintStyle StrokeStyle { get; set; } = Color.Black;
        public int LineWidth { get; set; } = 1;
        public FontStyles Font { get; set; } = FontStyles.Default;

        public DrawingContextState() { }

        public DrawingContextState(DrawingContextState state)
        {
            Path = new SKPath(state.Path);
            ClearStyle = state.ClearStyle;
            FillStyle = state.FillStyle;
            StrokeStyle = state.StrokeStyle;
            LineWidth = state.LineWidth;
            Font = new FontStyles(state.Font);
        }
    }
}
