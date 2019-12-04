using CoreUI.Dom.Styles;
using CoreUI.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Dom
{
    public sealed class CoreUIDomTextNode : CoreUIDomNode
    {
        public string Text { get; set; }

        public FontStyles FontStyles { get; set; } = FontStyles.Default;

        // Break

        // Join
    }
}
