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

        public event EventHandler<MouseButtonEventArgs> MouseButton;

        internal bool ShouldClose => _window.IsClosing;

        public Size Size
        {
            get => _window.Size;
            set => _window.Size = value;
        }

        public Point MousePosition
        {
            get {
                GL.GetCursorPosition(_window, out var x, out var y);
                return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
            }
            set => _window.MousePosition = value;
        }

        internal GlfwCoreUIWindow(string title, int width, int height)
        {
            _window = new NativeWindow(width, height, title);
            _context = new GlfwCoreUIDrawContext(_window);
            _window.Refreshed += Window_Refreshed;
            _window.MouseButton += Window_MouseButton;
        }

        private void Window_MouseButton(object sender, GLFW.MouseButtonEventArgs e) => MouseButton?.Invoke(this, new MouseButtonEventArgs(
                (MouseButton)e.Button,
                (InputState)e.Action,
                (ModifierKeys)e.Modifiers
            ));

        private void Window_Refreshed(object sender, EventArgs e) => DoRender();

        public ICoreUIWindow Renderer(Action<ICoreUIDrawContext> render)
        {
            _render += render;
            return this;
        }

        public void Refresh() => DoRender();

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
