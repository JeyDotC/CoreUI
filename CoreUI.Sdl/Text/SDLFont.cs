using System;
using System.Collections.Generic;
using CoreUI.Sdl.SDL2;

namespace CoreUI.Sdl.Text
{
    internal class SDLFont : IDisposable
    {
        private string _fontSource;
        private IDictionary<int, IntPtr> _fontSizes = new Dictionary<int, IntPtr>();

        public SDLFont(string source)
        {
            _fontSource = source;
        }

        internal IntPtr GetForSize(int size)
        {
            if (!_fontSizes.ContainsKey(size))
            {
                var font = SDL_ttf.TTF_OpenFont(_fontSource, size).Check($"Open font {_fontSource} with size {size}");
                _fontSizes[size] = font;
            }

            return _fontSizes[size];
        }

        public void Dispose()
        {
            foreach (var font in _fontSizes.Values)
            {
                SDL_ttf.TTF_CloseFont(font);
            }
        }
    }
}
