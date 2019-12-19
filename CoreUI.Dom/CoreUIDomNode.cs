using System;
using System.Collections.Generic;
using System.Drawing;

namespace CoreUI.Dom
{
    public abstract class CoreUIDomNode
    {
        private readonly IList<CoreUIDomNode> _children = new List<CoreUIDomNode>();

        private CoreUIDomNode _parent;

        public CoreUIDomNode Parent
        {
            get => _parent;
            protected set
            {
                if (value == this)
                {
                    throw new InvalidOperationException("A node cannot be parent of itself");
                }
                _parent = value;
            }
        }

        public IEnumerable<CoreUIDomNode> Children => _children;

        public virtual DrawBox DrawBox { get; set; } = new DrawBox();

        public CoreUIDomNode Add(CoreUIDomNode node)
        {
            if (node == this)
            {
                throw new InvalidOperationException("Adding a node to itself can cause infinite recursion!");
            }

            node.Parent = this;

            _children.Add(node);

            return this;
        }

        public CoreUIDomNode Remove(CoreUIDomNode node)
        {
            node.Parent = null;
            _children.Remove(node);

            return this;
        }

        public CoreUIDomNode Clear()
        {
            foreach (var child in _children)
            {
                child.Parent = null;
            }

            _children.Clear();

            return this;
        }
    }
}
