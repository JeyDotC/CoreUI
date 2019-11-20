using System;
using System.Drawing;

namespace CoreUI
{
    public interface ICoreUIWindow : IDisposable
    {
        public ICoreUIWindow Renderer(Action<ICoreUIDrawContext> render);

        public Size Size { get; set; }
    }
}
