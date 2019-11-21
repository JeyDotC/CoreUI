using System;
using System.Drawing;

namespace CoreUI
{
    public interface ICoreUIDrawContext : IDisposable
    {
        public ICoreUIDrawContext FontFamily(string fontFamily);

        public ICoreUIDrawContext DrawColor(Color color);

        public ICoreUIDrawContext Clear();

        public ICoreUIDrawContext DrawRectangle(Rectangle rectangle);

        public Size CalculateTextSize(string text, int fontSize);

        public Color RenderColor { get; }

        public ICoreUIDrawContext DrawText(string text, int fontSize, Point position);
    }
}
