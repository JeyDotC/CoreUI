using System;
using System.Collections.Generic;
using System.Linq;
using CoreUI.Dom.Styles;

namespace CoreUI.Dom.Rendering
{
    internal class DrawableNode
    {
        public DrawableNode ClosestBlockParent { get; set; }

        public PartialDrawBox PartialDrawBox { get; set; }

        public Style Style { get; set; }

        public CoreUIDomNode OriginalNode { get; set; }

        public DrawableNode Parent { get; set; }

        public List<DrawableNode> Children { get; set; } = new List<DrawableNode>();

        public static DrawableNode FromCoreUINode(CoreUIDomNode node)
        {
            var drawable = new DrawableNode
            {
                OriginalNode = node,
            };

            var nodeAsElement = node as CoreUIDomElement;

            if (nodeAsElement != null)
            {
                drawable.Style = new Style(nodeAsElement.Style);
            }

            var selectableChildren = node.Children.Where(child =>
            {
                var element = child as CoreUIDomElement;

                return element == null || element.Style.Display != DisplayStyle.None;
            });

            foreach (var child in selectableChildren)
            {
                var element = child as CoreUIDomElement;

                var drawableChild = FromCoreUINode(child);
                drawableChild.Parent = drawable;

                if (element != null)
                {
                    drawableChild.Style = new Style(element.Style);
                }

                drawable.Children.Add(drawableChild);
            }

            return drawable;
        }
    }
}
