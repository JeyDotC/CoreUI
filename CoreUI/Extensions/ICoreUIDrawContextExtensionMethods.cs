using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI
{
    public static class ICoreUIDrawContextExtensionMethods
    {
        public static ICoreUIDrawContext Rect(this ICoreUIDrawContext self, Rectangle rectangle) => self.BeginPath()
                .MoveTo(rectangle.Location)
                .LineTo(new Point(rectangle.Right, rectangle.Location.Y))
                .LineTo(new Point(rectangle.Right, rectangle.Bottom))
                .LineTo(new Point(rectangle.X, rectangle.Bottom))
                .ClosePath();

        public static ICoreUIDrawContext FillRect(this ICoreUIDrawContext self, Rectangle rectangle) => self.Rect(rectangle)
                .Fill();

        public static ICoreUIDrawContext StrokeRect(this ICoreUIDrawContext self, Rectangle rectangle) => self.Rect(rectangle)
                .Stroke();

    }
}
