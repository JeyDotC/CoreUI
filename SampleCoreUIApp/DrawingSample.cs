using CoreUI;
using CoreUI.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SampleCoreUIApp
{
    static class DrawingSample
    {
        public static ICoreUIWindow DrawingExample(this ICoreUIWindow window)
            => window.Renderer(context => {

                context.Clear();

                context.FillStyle = Color.DarkBlue;
                context.LineWidth = 5;
                context.Font.FontSize = 25;

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

                context.StrokeStyle = GradientSpec.Linear(new Point(0, 460 - 2), new Point(measure.Width, 460 - 2))
                                                  .AddColorStop(0, Color.Black)
                                                  .AddColorStop(0.5f, Color.Red);

                context.FillStyle = GradientSpec.Linear(new Point(0, 460 - 2), new Point(0, 460 + measure.Height))
                                                  .AddColorStop(0, Color.AliceBlue)
                                                  .AddColorStop(0.9f, Color.Green);
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
    }
}
