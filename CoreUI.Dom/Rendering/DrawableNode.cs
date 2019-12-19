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

        private readonly IList<DrawableNode> _children = new List<DrawableNode>();

        public IEnumerable<DrawableNode> Children => _children;

        public DrawableNode Add(DrawableNode node)
        {
            if (node == this)
            {
                throw new InvalidOperationException("Adding a node to itself can cause infinite recursion!");
            }

            node.Parent = this;

            _children.Add(node);

            return this;
        }
    }
}
