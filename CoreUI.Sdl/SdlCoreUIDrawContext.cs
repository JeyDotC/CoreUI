using System;
using System.Drawing;
using CoreUI.Sdl.SDL2;
using CoreUI.Sdl.Text;

namespace CoreUI.Sdl
{
    internal class SdlCoreUIDrawContext : ICoreUIDrawContext
    {
        private readonly IntPtr _renderer;
        private readonly SDLFontManager _fontManager;

        private SDLFont _currentFont;

        internal SdlCoreUIDrawContext(IntPtr renderer, SDLFontManager fontManager)
        {
            _renderer = renderer;
            _fontManager = fontManager;
            _currentFont = _fontManager[SdlCoreUIApp.DefaultFont];
        }

        public ICoreUIDrawContext DrawColor(Color color) {
            SDL.SDL_SetRenderDrawColor(_renderer, color.R, color.G, color.B, color.A).Check("Set Render Draw Color");
            return this;
        }

        public ICoreUIDrawContext Clear()
        {
            SDL.SDL_RenderClear(_renderer).Check("Clear Screen");
            return this;
        }

        public ICoreUIDrawContext DrawRectangle(Rectangle rectangle)
        {
            var sdlRect = rectangle.ToSDLRect();

            SDL.SDL_RenderFillRect(_renderer, ref sdlRect).Check("Draw Rectangle");
            return this;
        }

        public Size CalculateTextSize(string text, int fontSize)
        {
            var font = _currentFont.GetForSize(fontSize);

            SDL_ttf.TTF_SizeText(font, text, out var w, out var h).Check("calculate text size");

            return new Size(w, h);
        }

        public Color RenderColor
        {
            get
            {
                SDL.SDL_GetRenderDrawColor(_renderer, out var r, out var g, out var b, out var a).Check("Get Render Draw Color");

                return Color.FromArgb(a, r, g, b);
            }
        }

        public ICoreUIDrawContext DrawText(string text, int fontSize, Point position)
        {
            var font = _currentFont.GetForSize(fontSize);

            var textColor = RenderColor.ToSDLColor();
            
            var surface = SDL_ttf.TTF_RenderText_Blended(font, text, textColor).Check("Create text surface");

            var texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface).Check("Create texture from text surface");

            var size = CalculateTextSize(text, fontSize);

            var destinationRect = new Rectangle(position, size).ToSDLRect();

            SDL.SDL_RenderCopy(_renderer, texture, IntPtr.Zero, ref destinationRect).Check("Render the Text");

            SDL.SDL_FreeSurface(surface);

            return this;
        }

        internal void Present()
        {
            SDL.SDL_RenderPresent(_renderer);
        }

        public void Dispose()
        {
            SDL.SDL_DestroyRenderer(_renderer);
        }
    }
}
