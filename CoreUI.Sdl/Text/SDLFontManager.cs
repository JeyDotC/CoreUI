using System;
using System.Collections.Generic;
using System.IO;

namespace CoreUI.Sdl.Text
{
    internal class SDLFontManager : IDisposable
    {
        private IDictionary<string, SDLFont> _fonts = new Dictionary<string, SDLFont>();

        public void LoadFont(FileInfo fontNameFile, string fontName)
        {
            _fonts[fontName] = new SDLFont(fontNameFile.FullName);
        }

        public void Dispose()
        {
            foreach (var font in _fonts.Values)
            {
                font.Dispose();
            }
        }

        public SDLFont this[string fontName] => _fonts[fontName];
    }
}
