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
    }
}
