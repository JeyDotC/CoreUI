using GLFW;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GL = GLFW.Glfw;

namespace CoreUI.Glfw
{
    internal class GlfwCoreUIWindow : ICoreUIWindow
    {
        private readonly NativeWindow _window;
        private readonly GlfwCoreUIDrawContext _context;

        private Action<ICoreUIDrawContext> _render;

        internal bool ShouldClose => _window.IsClosing;

        public Size Size
        {
            get =>_window.Size;
            set => _window.Size = value;
        }

        internal GlfwCoreUIWindow(string title, int width, int height)
        {
            _window =  new NativeWindow(width, height, title);
            _context = new GlfwCoreUIDrawContext(_window);
            _window.Refreshed += _window_Refreshed;
        }

        private void _window_Refreshed(object sender, EventArgs e) => DoRender();

        public ICoreUIWindow Renderer(Action<ICoreUIDrawContext> render)
        {
            _render += render;
            return this;
        }

        internal void DoRender()
        {
            _render?.Invoke(_context);

            _context.Flush();
            _window.SwapBuffers();
        }

        public void Dispose()
        {
            //_context.Dispose();
            _window.Dispose();
        }
    }
}
