using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUI.Dom
{
    /// <summary>
    /// Serves as root for CoreUI DOM
    /// </summary>
    public class CoreUIDomDocument : CoreUIDomNode
    {
        private readonly ICoreUIDrawContext _context;

        public CoreUIDomDocument(ICoreUIDrawContext context, CoreUIDomElement body)
        {
            _context = context;
            Body = body;
            Add(body);
        }

        public override DrawBox DrawBox
        {
            get => new DrawBox
            {
                ContentBox = new System.Drawing.Rectangle
                {
                    Size = _context.ViewPort,
                }
            };
            set => throw new InvalidOperationException("Document's draw box is read-only.");
        }

        public CoreUIDomElement Body { get; }
    }
}
