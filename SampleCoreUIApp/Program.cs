using System;
using System.Drawing;
using CoreUI.Glfw;

namespace SampleCoreUIApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var app = new GlfwCoreUIApp())
            {
                var window = app.CreateWindow("Hello World").Renderer(context => {
                    context.Clear();
                    context.LineWidth = 5;
                    context.BeginPath()
                        .MoveTo(new Point(0, 0))
                        .LineTo(new Point(300, 250))
                        .LineTo(new Point(300, 400))
                        .ClosePath()
                        .Stroke();

                    context.FillStyle = Color.Blue;
                    context.BeginPath()
                        .MoveTo(new Point(360, 0))
                        .LineTo(new Point(600, 0))
                        .LineTo(new Point(600, 400))
                        .LineTo(new Point(360, 400))
                        .ClosePath()
                        .Fill();

                    var text = "Hello World!";
                    var measure = context.MeasureText(text);
                    context.FillText(text, new Point(2, 460));
                    context.LineWidth = 2;
                    context.BeginPath()
                        .MoveTo(new Point(0, 460 - 2))
                        .LineTo(new Point(measure.Width + 2, 460 - 2))
                        .LineTo(new Point(measure.Width + 2, 460 + measure.Height))
                        .LineTo(new Point(0, 460 + measure.Height))
                        .ClosePath()
                        .Stroke();
                });
                
                app.WaitForExit();
            }
        }
    }
}
