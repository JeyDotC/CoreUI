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
        public static ICoreUIWindow PrimitivesExample(this ICoreUIWindow window) => window.Renderer(context =>
        {
            context.ClearStyle = Color.White;

            context.Clear();

            var textBox = new TextBox {
                Background = Color.AliceBlue,
                ContentArea = new Rectangle(50, 50, 200, 60),
                Value = "El Burrito Pepe, Muy Cargado va, Trota que te Trota, Trota que te Tra.",
            };

            textBox.Draw(context);
        });
    }
}
