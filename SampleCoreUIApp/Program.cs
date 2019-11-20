using System;
using System.Drawing;
using CoreUI.Sdl;

namespace SampleCoreUIApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var app = new SdlCoreUIApp())
            {
                var window = app.CreateWindow("Hello World");
                window.Renderer(ctx =>
                {
                    ctx.DrawColor(Color.Black)
                       .DrawText("Hello World", 45, new Point(80, 60));
                });

                app.WaitForExit();
            }
        }
    }
}
