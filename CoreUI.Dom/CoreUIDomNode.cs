using System;
using System.Collections.Generic;
using System.Drawing;

namespace CoreUI.Dom
{
    public abstract class CoreUIDomNode
    {
        public CoreUIDomNode Parent { get; set; }

        /// <summary>
        /// Position relative to parent
        /// </summary>
        public Point Position { get; set; } = new Point();

        public abstract Size Size { get; set; }

        IList<CoreUIDomNode> Children { get; } = new List<CoreUIDomNode>();
    }
}
