using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GL = GLFW.Glfw;

namespace CoreUI.Glfw
{
    public class GlfwCoreUIApp : ICoreUIApp
    {
        private GlfwCoreUIWindow _window;
        public GlfwCoreUIApp()
        {
            if (!GL.Init())
            {
                var errorCode = GL.GetError(out var errorMessage);
                throw new InvalidOperationException($"{errorCode}: {errorMessage}");
            }
        }

        public ICoreUIWindow CreateWindow(string title, int width = 800, int heitght = 600)
        {
            if (_window != null)
            {
                _window.Dispose();
            }

            _window = new GlfwCoreUIWindow(title, width, heitght);
            
            return _window;
        }

        

        public void WaitForExit()
        {
            while(!_window.ShouldClose)
            {
                GL.PollEvents();
            }
        }

        public ICoreUIApp WithFont(FileInfo fontFile, string fontName)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _window?.Dispose();
                }

                GL.Terminate();

                disposedValue = true;
            }
        }
        
        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
