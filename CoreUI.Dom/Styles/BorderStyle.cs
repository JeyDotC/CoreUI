﻿using CoreUI.Styles;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUI.Dom.Styles
{
    public struct BorderStyle
    {
        public static BorderStyle None => new BorderStyle
        {
            Paint = PaintStyle.None,
            Width = 0
        };

        public PaintStyle Paint { get; set; }

        public LengthHint Width { get; set; }
    }
}
