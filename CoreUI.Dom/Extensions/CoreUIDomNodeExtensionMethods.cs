using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CoreUI.Dom
{
    public static class CoreUIDomNodeExtensionMethods
    {
        public static Point GetGlobalPosition(this CoreUIDomNode node)
            => node.GetAllParents().Aggregate(node.Position, (accumulate, item) => accumulate + (Size)item.Position);

        public static IEnumerable<CoreUIDomNode> GetAllParents(this CoreUIDomNode node)
        {
            while (node.Parent != null)
            {
                yield return node.Parent;
            }
        }
    }
}
