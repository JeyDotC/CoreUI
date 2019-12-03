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
        private Size _currentSize = new Size();

        public string Text { get; set; }

        public FontStyles FontStyles { get; set; } = FontStyles.Default;

        public override Size Size { 
            get => _currentSize; 
            set => _currentSize = value; 
        }

        protected override void Render(ICoreUIDrawContext drawContext)
        {
            drawContext.Save();

            var position = this.GetGlobalPosition();
            _currentSize = drawContext.MeasureText(Text);

            drawContext.Font = FontStyles;

            drawContext.FillText(Text, position);

            drawContext.Restore();
        }
    }
}
