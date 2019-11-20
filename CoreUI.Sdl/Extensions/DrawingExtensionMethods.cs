using System;
using CoreUI.Sdl.SDL2;

namespace System.Drawing
{
    internal static class DrawingExtensionMethods
    {
        public static SDL.SDL_Rect ToSDLRect(this Rectangle rectangle) => new SDL.SDL_Rect
        {
            x = rectangle.X,
            y = rectangle.Y,
            w = rectangle.Width,
            h = rectangle.Height
        };

        public static SDL.SDL_Color ToSDLColor(this Color color) => new SDL.SDL_Color
        {
            r = color.R,
            g = color.G,
            b = color.B,
            a = color.A,
        };
    }
}
