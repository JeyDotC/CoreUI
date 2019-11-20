
using System;
using System.Collections.Generic;
using System.IO;
using CoreUI.Sdl.SDL2;
using CoreUI.Sdl.Text;

namespace CoreUI.Sdl
{
    public class SdlCoreUIApp : ICoreUIApp
    {
        public const string DefaultFont = "RobotoMono";
        private readonly List<SdlCoreUIWindow> _windows = new List<SdlCoreUIWindow>();
        private readonly SDLFontManager _fontManager = new SDLFontManager();

        public SdlCoreUIApp()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0 || SDL_ttf.TTF_Init() < 0)
            {
                throw new InvalidOperationException($"Unable to initialize SDL. Error: {SDL.SDL_GetError()}");
            }

            SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "1");

            WithFont(new FileInfo(Path.Combine(Environment.CurrentDirectory, "Fonts", $"{DefaultFont}-Regular.ttf")), DefaultFont);
        }

        public ICoreUIApp WithFont(FileInfo fontFile, string fontName)
        {
            _fontManager.LoadFont(fontFile, fontName);
            return this;
        }

        public ICoreUIWindow CreateWindow(string title, int width = 800, int heitght = 600) { 

            var window = new SdlCoreUIWindow(title, width, heitght, _fontManager);

            _windows.Add(window);

            return window;
        }

        public void WaitForExit()
        {
            bool quit = false;
            
            while (!quit)
            {
                _windows.ForEach(w => w.DoRender());

                while (SDL.SDL_PollEvent(out var e) != 0)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            quit = true;
                            break;
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var window in _windows)
            {
                window.Dispose();
            }

            _fontManager.Dispose();

            SDL.SDL_Quit();
        }
    }
}
