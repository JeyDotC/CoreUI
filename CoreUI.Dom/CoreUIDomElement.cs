using CoreUI.Dom.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Dom
{
    public abstract class CoreUIDomElement : CoreUIDomNode
    {
        public Style Style { get; protected set; } = new Style();

        public abstract Style DefaultStyle { get; }
    }
}
