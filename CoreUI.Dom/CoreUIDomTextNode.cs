using CoreUI.Dom.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreUI.Dom
{
    public abstract class CoreUIDomTextNode : CoreUIDomNode
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
            var position = this.GetGlobalPosition();
            _currentSize = drawContext.CalculateTextSize(Text, FontStyles.FontSize);

            if (!string.IsNullOrWhiteSpace(FontStyles.FontFamily))
            {
                drawContext.FontFamily(FontStyles.FontFamily);
            }

            drawContext.DrawColor(FontStyles.FontColor)
                .DrawText(Text, FontStyles.FontSize, position);
        }
    }
}
