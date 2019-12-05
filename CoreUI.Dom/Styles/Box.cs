using System;
using System.Collections.Generic;
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
    }
}
