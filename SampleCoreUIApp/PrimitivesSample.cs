using CoreUI;
using CoreUI.Primitives;
using CoreUI.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SampleCoreUIApp
{
    static class PrimitivesSample
    {
        public static ICoreUIWindow PrimitivesExample(this ICoreUIWindow window)
        {
            var textBox = new TextBox
            {
                Background = Color.AliceBlue,
                ContentArea = new Rectangle(50, 50, 200, 13),
                Value = "El Burrito Pepe, Muy Cargado va, Trota que te Trota, Trota que te Tra.",
                HasFocus = true,
            };

            window.MouseButton += (o, args) =>
            {
                if (args.Action == InputState.Release && args.Button == MouseButton.Left)
                {
                    var mousePosition = window.MousePosition;
                    textBox.LocateCaretAt(mousePosition);
                }
            };

            return window.Renderer(context =>
            {
                context.ClearStyle = Color.White;

                context.Clear();

                textBox.Draw(context);
            });
        }
    }
}