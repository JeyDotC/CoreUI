using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Dom.Styles
{
    public struct Box
    {
        public static Box None => new Box
        {
            Top = 0,
            Right = 0,
            Bottom = 0,
            Left = 0,
        };

        public LengthHint Top { get; set; }

        public LengthHint Right { get; set; }

        public LengthHint Bottom { get; set; }

        public LengthHint Left { get; set; }

        public Box(LengthHint top) : this(top, top, top, top)
        { }

        public Box(LengthHint top, LengthHint right) : this(top, right, top, right)
        { }

        public Box(LengthHint top, LengthHint right, LengthHint bottom) : this(top, right, bottom, right)
        { }

        public Box(LengthHint top, LengthHint right, LengthHint bottom, LengthHint left) : this()
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public Point GetDrawPosition(Rectangle relativeTo)
            => new Point
            {
                X = Left.GetDrawValue(relativeTo.Width),
                Y = Top.GetDrawValue(relativeTo.Height)
            } + (Size)relativeTo.Location;

        public Size GetDrawSize(Size relativeTo = new Size())
            => new Size
            {
                Height = Top.GetDrawValue(relativeTo.Height) + Bottom.GetDrawValue(relativeTo.Height),
                Width = Left.GetDrawValue(relativeTo.Width) + Right.GetDrawValue(relativeTo.Width),
            } + relativeTo;
    }
}
