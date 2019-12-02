﻿using System;
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
                var window = app.CreateWindow("Hello World").Renderer(context => {

                    context.Clear();

                    context.FillStyle = Color.DarkBlue;
                    context.LineWidth = 5;
                    context.Font = new FontStyles(context.Font)
                    {
                        FontSize = 25
                    };

                    context.BeginPath()
                        .MoveTo(new Point(0, 0))
                        .LineTo(new Point(300, 250))
                        .LineTo(new Point(300, 400))
                        .ClosePath()
                        .Fill()
                        .Stroke();
                    
                    context.FillRect(new Rectangle(360, 0, 240, 400));

                    context.ClearRect(new Rectangle(380, 40, 60, 60));

                    var text = "Hello World!";
                    var measure = context.MeasureText(text);

                    context.Save();
                    context.LineWidth = 2;
                    context.StrokeStyle = Color.Red;
                    context.FillStyle = Color.AliceBlue;
                    context.Rect(new Rectangle(
                            new Point(0, 460 - 2), 
                            measure + new Size(2, 2)
                        )
                    ).Fill().Stroke();

                    context.Restore();

                    context.FillText(text, new Point(2, 460));
                    context.Rect(new Rectangle(380, 460, 200, 100))
                        .Fill()
                        .Stroke();
                });
                
                app.WaitForExit();
            }
        }
    }
}
