using CoreUI;
using CoreUI.Dom;
using CoreUI.Dom.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SampleCoreUIApp
{
    class Div : CoreUIDomElement
    {
        public Div(Action<Div> setup, params Div[] children)
        {
            Style.Display = DisplayStyle.Block;
            Style.Width = new LengthHint(100, MeasureUnit.Percent);
            
            setup(this);
            foreach (var child in children)
            {
                Add(child);
            }
        }
    }

    static class DomSample
    {
        public static ICoreUIWindow DomExample(this ICoreUIWindow window) => window.Renderer(context =>
        {
            var root = new Div(d => {
                var solidColoredBorder = new BorderStyle
                {
                    Paint = Color.Black,
                    Width = 1,
                };
                d.Style.Width = 400;
                d.Style.Height = 400;
                d.Style.Border = new BorderBox(solidColoredBorder);
                d.Style.Margin = new Box(20);
                d.Style.Padding = new Box(6);
                d.Style.Background = Color.AliceBlue;
            });

            var contentBox = new Rectangle
            {
                Size = new Size((int)root.Style.Width.Value, (int)root.Style.Height.Value)
            };
            var paddingBox = new Rectangle
            {
                Size = contentBox.Size + new Size(
                    (int)(root.Style.Padding.Right.Value + root.Style.Padding.Left.Value),
                    (int)(root.Style.Padding.Top.Value + root.Style.Padding.Bottom.Value)
                )
            };
            var borderBox = new Rectangle {
                Size = paddingBox.Size + new Size(
                    (int)(root.Style.Border.Box.Right.Value + root.Style.Border.Box.Left.Value),
                    (int)(root.Style.Border.Box.Top.Value + root.Style.Border.Box.Bottom.Value)
                )
            };
            var marginBox = new Rectangle
            {
                Size = borderBox.Size + new Size(
                    (int)(root.Style.Margin.Right.Value + root.Style.Margin.Left.Value),
                    (int)(root.Style.Margin.Top.Value + root.Style.Margin.Bottom.Value)
                )
            };

            marginBox.Location = new Point();
            borderBox.Location = marginBox.Location + new Size((int)root.Style.Margin.Right.Value, (int)root.Style.Margin.Top.Value);
            paddingBox.Location = borderBox.Location + new Size((int)root.Style.Border.Box.Right.Value, (int)root.Style.Border.Box.Top.Value);
            contentBox.Location = paddingBox.Location + new Size((int)root.Style.Padding.Right.Value, (int)root.Style.Padding.Top.Value);

            var drawBox = new DrawBox
            {
                MarginBox = marginBox,
                BorderBox = borderBox,
                PaddingBox = paddingBox,
                ContentBox = contentBox,
            };

            context.Clear();

            context.FillStyle = root.Style.Background;
            context.BeginPath();

            context.StrokeStyle = root.Style.Border.Top.Paint;
            context.LineWidth = (int)root.Style.Border.Top.Width.Value;
            context.MoveTo(borderBox.Location).LineTo(new Point(borderBox.Right, borderBox.Location.Y));

            context.StrokeStyle = root.Style.Border.Right.Paint;
            context.LineWidth = (int)root.Style.Border.Right.Width.Value;
            context.LineTo(new Point(borderBox.Right, borderBox.Bottom));

            context.StrokeStyle = root.Style.Border.Bottom.Paint;
            context.LineWidth = (int)root.Style.Border.Bottom.Width.Value;
            context.LineTo(new Point(borderBox.Location.X, borderBox.Bottom));

            context.StrokeStyle = root.Style.Border.Left.Paint;
            context.LineWidth = (int)root.Style.Border.Left.Width.Value;
            context.ClosePath();

            context.Fill().Stroke();

        });
    }
}
