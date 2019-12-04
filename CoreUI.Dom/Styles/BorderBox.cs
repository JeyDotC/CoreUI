using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUI.Dom.Styles
{
    public struct BorderBox
    {
        public static BorderBox None => new BorderBox
        {
            Top = BorderStyle.None,
            Right = BorderStyle.None,
            Bottom = BorderStyle.None,
            Left = BorderStyle.None,
        };

        public BorderStyle Top { get; set; }

        public BorderStyle Right { get; set; }

        public BorderStyle Bottom { get; set; }

        public BorderStyle Left { get; set; }

        public Box Box => new Box
        {
            Top = Top.Width,
            Right = Right.Width,
            Bottom = Bottom.Width,
            Left = Left.Width,
        };
    }
}
