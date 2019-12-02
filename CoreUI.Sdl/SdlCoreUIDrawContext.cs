using System;
using System.Drawing;
using CoreUI.Sdl.SDL2;
using CoreUI.Sdl.Text;
using CoreUI.Styles;

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


        public ICoreUIDrawContext FillRectangle(Rectangle rectangle)
        {
            var sdlRect = rectangle.ToSDLRect();

            SDL.SDL_RenderFillRect(_renderer, ref sdlRect).Check("Draw Rectangle");
            return this;
        }

        public ICoreUIDrawContext DrawRectangle(Rectangle rectangle)
        {
            var sdlRect = rectangle.ToSDLRect();

            SDL.SDL_RenderDrawRect(_renderer, ref sdlRect).Check("Draw Rectangle");
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

        public int LineWidth { get; set; }
        public Color FillStyle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color StrokeStyle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FontStyles Font { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public ICoreUIDrawContext FontFamily(string fontFamily)
        {
            _currentFont = _fontManager[fontFamily];
            return this;
        }

        public ICoreUIDrawContext ClearRect(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext Fill()
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext Stroke()
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext BeginPath()
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext MoveTo(Point point)
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext ClosePath()
        {
            throw new NotImplementedException();
        }

        public Size MeasureText(string text)
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext FillText(string text, Point position)
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext Save()
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext Restore()
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext LineTo(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
