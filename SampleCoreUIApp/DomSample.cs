﻿using CoreUI;
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
            Style.Width = MeasureUnit.Auto;
            
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

            var drawBox = CalculateProvidedWidth(root, context);
            var style = root.Style;

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

        });

        private static DrawBox CalculateProvidedWidth(CoreUIDomElement root, ICoreUIDrawContext context)
        {
            var style = root.Style;

            var contentBox = new Rectangle
            {
                Size = new Size
                {
                    Width = style.Width.GetDrawValue(context.ViewPort.Width),
                    Height = style.Height.GetDrawValue(context.ViewPort.Height),
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
            return new DrawBox
            {
            };
        }
    }
}
