using CoreUI.Dom.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Dom
{
    public abstract class CoreUIDomElement : CoreUIDomNode
    {
        public Style Style { get; } = new Style();

        public override Point Position
        {
            get => base.Position + new Size(Style.Padding.Left, Style.Padding.Top);
            set => base.Position = value;
        }
    }
}
