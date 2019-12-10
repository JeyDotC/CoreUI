﻿using CoreUI;
using CoreUI.Dom;
using CoreUI.Dom.Rendering;
using CoreUI.Dom.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SampleCoreUIApp
{
    class Div : CoreUIDomElement
    {
        public override Style DefaultStyle => new Style
        {
            Display = DisplayStyle.Block,
            Width = ValueKind.Auto
        };

        public Div(Action<Div> setup, params Div[] children)
        {
            Style = new Style(DefaultStyle);

            setup(this);
            foreach (var child in children)
            {
                Add(child);
            }
        }
    }

    static class DomSample
    {
        public static ICoreUIWindow DomExample(this ICoreUIWindow window)
        {
            var root = new Div(d =>
            {
                var solidColoredBorder = new BorderStyle
                {
                    Paint = Color.Black,
                    Width = 1,
                };
                d.Style.Width = 50f.Percent();
                d.Style.Height = 400f.Px();
                d.Style.Border = new BorderBox(solidColoredBorder);
                d.Style.Margin = new Box(20, 40);
                d.Style.Padding = new Box(6);
                d.Style.Background = Color.AliceBlue;
                d.Style.FontStyles.FontSize = 18;
            });

            return window.Renderer(context =>
            {
                var document = new CoreUIDomDocument(context, root);

                var drawboxCalculator = new DrawBoxCalculator();

                drawboxCalculator.CalculateDrawBoxesForTree(document.Body);


                var drawBox = document.Body.DrawBox;
                var style = document.Body.Style;
                
                context.Clear();

                context.FillStyle = style.Background;
                context.BeginPath();

                context.StrokeStyle = style.Border.Top.Paint;
                context.LineWidth = (int)style.Border.Top.Width.Value;
                context.MoveTo(drawBox.BorderBox.Location).LineTo(new Point(drawBox.BorderBox.Right, drawBox.BorderBox.Location.Y));

                context.StrokeStyle = style.Border.Right.Paint;
                context.LineWidth = (int)style.Border.Right.Width.Value;
                context.LineTo(new Point(drawBox.BorderBox.Right, drawBox.BorderBox.Bottom));

                context.StrokeStyle = style.Border.Bottom.Paint;
                context.LineWidth = (int)style.Border.Bottom.Width.Value;
                context.LineTo(new Point(drawBox.BorderBox.Location.X, drawBox.BorderBox.Bottom));

                context.StrokeStyle = style.Border.Left.Paint;
                context.LineWidth = (int)style.Border.Left.Width.Value;
                context.ClosePath();

                context.Fill().Stroke();

                DrawText(context, "This is text contained into a div, it is a veeeery long one, so, it should be split into several lines depending on the container's size.", root);

            });
        }

        struct Line
        {
            public string Text { get; set; }

            public Size Size { get; set; }
        }

        private static void DrawText(ICoreUIDrawContext context, string text, CoreUIDomElement container)
        {
            context.Font = container.Style.FontStyles;

            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var lines = new List<string>();

            var currentLine = string.Empty;

            foreach (var word in words)
            {
                var candidateLine = $"{currentLine} {word}";
                if (context.MeasureText(candidateLine).Width <= container.DrawBox.ContentBox.Width)
                {
                    currentLine = candidateLine;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
            }

            lines.Add(currentLine);

            var drawPoint = container.DrawBox.ContentBox.Location;

            lines.ForEach(l =>
            {
                var measure = context.MeasureText(l);
                context.FillText(l, drawPoint);
                drawPoint = new Point(drawPoint.X, drawPoint.Y + measure.Height);
            });
        }

        private static DrawBox CalculateProvidedWidth(CoreUIDomElement root, ICoreUIDrawContext context)
        {
            var style = root.Style;

            var contentBox = new Rectangle
            {
                Size = new Size
                {
                    Width = style.Width.Value.GetDrawValue(context.ViewPort.Width),
                    Height = style.Height.Value.GetDrawValue(context.ViewPort.Height),
                }
            };
            var paddingBox = new Rectangle
            {
                Size = style.Padding.GetDrawSize(contentBox.Size),
            };
            var borderBox = new Rectangle
            {
                Size = style.Border.Box.GetDrawSize(paddingBox.Size),
            };
            var marginBox = new Rectangle
            {
                Size = style.Margin.GetDrawSize(borderBox.Size),
            };

            marginBox.Location = new Point();
            borderBox.Location = style.Margin.GetDrawPosition(marginBox);
            paddingBox.Location = style.Border.Box.GetDrawPosition(borderBox);
            contentBox.Location = style.Padding.GetDrawPosition(paddingBox);

            return new DrawBox
            {
                MarginBox = marginBox,
                BorderBox = borderBox,
                PaddingBox = paddingBox,
                ContentBox = contentBox,
            };
        }

        private static DrawBox CalculateAuto(CoreUIDomElement root, ICoreUIDrawContext context)
        {
            var style = root.Style;

            var marginBox = new Rectangle
            {
                Size = new Size
                {
                    Width = context.ViewPort.Width,
                    Height = style.Height.Value.GetDrawValue(context.ViewPort.Height), // Here goes children height calculation.
                },
            };

            var borderBox = new Rectangle
            {
                Size = new Size
                {
                    Width = marginBox.Width - style.Margin.Left.Value.GetDrawValue(marginBox.Width) - style.Margin.Right.Value.GetDrawValue(marginBox.Width),
                    Height = marginBox.Height - style.Margin.Top.Value.GetDrawValue(marginBox.Height) - style.Margin.Bottom.Value.GetDrawValue(marginBox.Height),
                },
            };

            var paddingBox = new Rectangle
            {
                Size = new Size
                {
                    Width = borderBox.Width - style.Border.Box.Left.Value.GetDrawValue(marginBox.Width) - style.Border.Box.Right.Value.GetDrawValue(marginBox.Width),
                    Height = borderBox.Height - style.Border.Box.Top.Value.GetDrawValue(marginBox.Height) - style.Border.Box.Bottom.Value.GetDrawValue(marginBox.Height),
                },
            };

            var contentBox = new Rectangle
            {
                Size = new Size
                {
                    Width = paddingBox.Width - style.Padding.Left.Value.GetDrawValue(marginBox.Width) - style.Padding.Right.Value.GetDrawValue(marginBox.Width),
                    Height = paddingBox.Height - style.Padding.Top.Value.GetDrawValue(marginBox.Height) - style.Padding.Bottom.Value.GetDrawValue(marginBox.Height),
                },
            };

            marginBox.Location = new Point();
            borderBox.Location = style.Margin.GetDrawPosition(marginBox);
            paddingBox.Location = style.Border.Box.GetDrawPosition(borderBox);
            contentBox.Location = style.Padding.GetDrawPosition(paddingBox);

            return new DrawBox
            {
                MarginBox = marginBox,
                BorderBox = borderBox,
                PaddingBox = paddingBox,
                ContentBox = contentBox,
            };
        }
    }
}
