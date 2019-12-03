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
            get => base.Position + new Size((int)Style.Padding.Left.Value, (int)Style.Padding.Top.Value);
            set => base.Position = value;
        }
    }
}
