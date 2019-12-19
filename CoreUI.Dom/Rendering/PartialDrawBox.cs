using System;
using System.Drawing;

namespace CoreUI.Dom.Rendering
{
    public class PartialDrawBox
    {
        public Rectangle? MarginBox { get; set; }

        public Rectangle? BorderBox { get; set; }

        public Rectangle? PaddingBox { get; set; }

        public Rectangle? ContentBox { get; set; }

        public PartialDrawBox() { }

        public PartialDrawBox(DrawBox drawBox)
        {
            MarginBox = drawBox.MarginBox;
            BorderBox = drawBox.BorderBox;
            PaddingBox = drawBox.PaddingBox;
            ContentBox = drawBox.ContentBox;
        }

        public DrawBox ToDrawBox() => new DrawBox
        {
            MarginBox = MarginBox.GetValueOrDefault(),
            BorderBox = BorderBox.GetValueOrDefault(),
            PaddingBox = PaddingBox.GetValueOrDefault(),
            ContentBox = ContentBox.GetValueOrDefault(),
        };
    }
}
