﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace CoreUI.Dom
{
    public abstract class CoreUIDomNode
    {
        private readonly IList<CoreUIDomNode> _children = new List<CoreUIDomNode>();

        public CoreUIDomNode Parent { get; protected set; }

        IEnumerable<CoreUIDomNode> Children => _children;

        public DrawBox DrawBox { get; set; }

        public CoreUIDomNode Add(CoreUIDomNode node)
        {
            if(node == this)
            {
                throw new InvalidOperationException("Adding a node to itself can cause infinite recursion!");
            }
            
            node.Parent = this;

            _children.Add(node);

            return this;
        }

        public CoreUIDomNode Remove(CoreUIDomNode node)
        {
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
