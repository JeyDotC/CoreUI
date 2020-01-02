using System;
using System.Drawing;
using CoreUI.Glfw;
using CoreUI;
using CoreUI.Styles;

namespace SampleCoreUIApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var app = new GlfwCoreUIApp())
            {
                // var window = app.CreateWindow("Hello World").DrawingExample();
                // var window = app.CreateWindow("Hello World").DomExample();
                var window = app.CreateWindow("Hello World").PrimitivesExample();
                
                app.WaitForExit();
            }
        }
    }
}
