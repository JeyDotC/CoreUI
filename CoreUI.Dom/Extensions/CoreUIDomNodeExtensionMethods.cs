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
    }
}
