﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Dom
{
    public class DrawBox
    {
        public Rectangle MarginBox { get; set; }

        public Rectangle BorderBox { get; set; }

        public Rectangle PaddingBox { get; set; }

        public Rectangle ContentBox { get; set; }

        public DrawBox() {}

        public DrawBox(DrawBox drawBox)
        {
            MarginBox = drawBox.MarginBox;
            BorderBox = drawBox.BorderBox;
            PaddingBox = drawBox.PaddingBox;
            ContentBox = drawBox.ContentBox;
        }
    }
}
