using System;
using System.Drawing;
using CoreUI.Sdl.SDL2;
using CoreUI.Sdl.Text;

namespace CoreUI.Sdl
{
    internal class SdlCoreUIWindow : ICoreUIWindow
    {
        private readonly IntPtr _window;
        private readonly SdlCoreUIDrawContext _context;

        private Action<SdlCoreUIDrawContext> _render;

        public Size Size {
            get {
                SDL.SDL_GetWindowSize(_window, out var w, out var h);
                return new Size(w, h);
            }
            set => SDL.SDL_SetWindowSize(_window, value.Width, value.Height);
        }

        internal SdlCoreUIWindow(string title, int width, int height, SDLFontManager fontManager)
        {
            _window = SDL.SDL_CreateWindow(title,
                    SDL.SDL_WINDOWPOS_CENTERED,
                    SDL.SDL_WINDOWPOS_CENTERED,
                    width,
                    height,
                    SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
                );

            if (_window == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Unable to create a window. SDL. Error: {SDL.SDL_GetError()}");
            }

            var renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if(renderer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Unable to create a renderer. SDL. Error: {SDL.SDL_GetError()}");
            }

            _context = new SdlCoreUIDrawContext(renderer, fontManager);
        }

        public ICoreUIWindow Renderer(Action<ICoreUIDrawContext> render)
        {
            _render += render;
            return this;
        }

        internal void DoRender()
        {
            _context.DrawColor(Color.White)
                    .Clear();

            _render?.Invoke(_context);

            _context.Present();
        }

        public void Dispose()
        {
            _context.Dispose();
            SDL.SDL_DestroyWindow(_window);
        }
    }
}
