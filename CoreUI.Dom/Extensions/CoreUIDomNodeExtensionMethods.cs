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
            => node.GetAllParents().Aggregate(new Point(), (accumulate, item) => accumulate);

        public static IEnumerable<CoreUIDomNode> GetAllParents(this CoreUIDomNode node)
        {
            while (node.Parent != null)
            {
                yield return node.Parent;
            }
        }

        public static CoreUIDomNode Previous(this CoreUIDomNode node)
        {
            var siblings = node.Parent?.Children?.ToList() ?? new List<CoreUIDomNode>();
            var index = siblings.IndexOf(node);

            return index - 1 >= 0 ? siblings[index - 1] : null;
        }
    }
}
