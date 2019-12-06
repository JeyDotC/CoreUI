using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUI.Dom.Rendering
{
    public class DrawBoxCalculator
    {
        public void CalculateDrawBoxesForTree(CoreUIDomElement element)
        {
            var parentBox = element.Parent.DrawBox?.ContentBox;


        }
    }
}
