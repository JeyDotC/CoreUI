using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreUI.Dom.Styles;

namespace CoreUI.Dom.Rendering
{
    public class DrawBoxCalculator
    {

        private DrawableNode PrecalculateMeasures(CoreUIDomNode node, DrawableNode closestBlockParent)
        {
            var drawable = new DrawableNode {
                OriginalNode = node,
                ClosestBlockParent = closestBlockParent,
            };

            var nodeAsElement = node as CoreUIDomElement;
            var childrensClosestBlockParent = closestBlockParent;

            if (nodeAsElement != null)
            {
                drawable.Style = new Style(nodeAsElement.Style);
                if (nodeAsElement.Style.Display == DisplayStyle.Block)
                {
                    childrensClosestBlockParent = drawable;
                }
            }

            // Do the pre-calculation here.

            var children = node.Children.Where(child =>
            {
                var element = child as CoreUIDomElement;

                return element == null || element.Style.Display != DisplayStyle.None;
            }).Select(child => PrecalculateMeasures(child, childrensClosestBlockParent));

            foreach (var child in children)
            {
                drawable.Add(child);
            }

            return drawable;
        }

        private DrawableNode CompleteMeasures(DrawableNode drawableTree)
        {
            throw new NotImplementedException();
        }

        private DrawableNode CalculatePositions(DrawableNode drawableTree)
        {
            throw new NotImplementedException();
        }

        private DrawableNode Reflow(DrawableNode drawableTree)
        {
            throw new NotImplementedException();
        }

        internal DrawableNode CalculateDrawableDom(CoreUIDomDocument document)
        {
            var documentNode = new DrawableNode
            {
                PartialDrawBox = new PartialDrawBox(document.DrawBox),
                OriginalNode = document,
            };
            var drawableNode = PrecalculateMeasures(document.Body, documentNode);
            drawableNode = CompleteMeasures(drawableNode);
            drawableNode = CalculatePositions(drawableNode);
            drawableNode = Reflow(drawableNode);

            return drawableNode;
        }

        public void CalculateDrawBoxesForTree(CoreUIDomElement element)
        {
            var box = CalculateWidth(element);
            box = CalculateHeight(element, box);
            box = CalculatePositions(element, box);

            element.DrawBox = box;
        }

        // Postition
        private DrawBox CalculatePositions(CoreUIDomElement element, DrawBox box)
        {
            var style = element.Style;
            var marginBox = box.MarginBox;
            var borderBox = box.BorderBox;
            var paddingBox = box.PaddingBox;
            var contentBox = box.ContentBox;
            var parentLocation = element.Parent.DrawBox.ContentBox.Location;
            var location = parentLocation;
            var previousSibling = element.Previous();
            
            if (previousSibling != null)
            {
                if (element.Style.Display == DisplayStyle.Block)
                {
                    location = location + new Size(0, previousSibling.DrawBox.BorderBox.Location.Y);
                }

                var previousElement = previousSibling as CoreUIDomElement;

                if (previousElement != null)
                {
                    
                }
            }


            marginBox.Location = location;
            borderBox.Location = style.Margin.GetDrawPosition(marginBox);
            paddingBox.Location = style.Border.Box.GetDrawPosition(borderBox);
            contentBox.Location = style.Padding.GetDrawPosition(paddingBox);

            return new DrawBox
            {
                BorderBox = borderBox,
                ContentBox = contentBox,
                MarginBox = marginBox,
                PaddingBox = paddingBox
            };
        }

        // Height

        private DrawBox CalculateHeight(CoreUIDomElement element, DrawBox currentBox)
            => element.Style.Display switch
            {
                DisplayStyle.Block => CalculateHeightOnBlockElement(element, currentBox),
                DisplayStyle.Inline => CalculateHeightOnInlineElement(element, currentBox),
                _ => new DrawBox(),
            };

        private DrawBox CalculateHeightOnBlockElement(CoreUIDomElement element, DrawBox currentBox)
        {
            var height = element.Style.Height.ValueKind switch
            {
                ValueKind.Default => element.DefaultStyle.Height,
                ValueKind.Inherit => element.Parent is CoreUIDomElement ? (element.Parent as CoreUIDomElement).Style.Height : new LengthHint(element.Parent.DrawBox.ContentBox.Height),
                ValueKind.Value => element.Style.Height,
                _ => element.Style.Height
            };

            if (height.ValueKind == ValueKind.Auto)
            {
                return CalculateHeightOnBlockElementWithAutoValue(element, currentBox);
            }

            return CalculateHeightOnBlockElementWithProvidedValue(element, currentBox);
        }

        private DrawBox CalculateHeightOnBlockElementWithAutoValue(CoreUIDomElement element, DrawBox currentBox)
        {
            throw new NotImplementedException();
        }

        private DrawBox CalculateHeightOnBlockElementWithProvidedValue(CoreUIDomElement element, DrawBox currentBox)
        {
            var style = element.Style;
            var parentHeight = element.Parent.DrawBox.ContentBox.Height;

            var contentBox = new Rectangle
            {
                Size = new Size
                {
                    Width = currentBox.ContentBox.Width,
                    Height = style.Height.Value.GetDrawValue(parentHeight),
                }
            };
            var paddingBox = new Rectangle
            {
                Size = style.Padding.GetDrawSize(contentBox.Size),
            };
            var borderBox = new Rectangle
            {
                Size = style.Border.Box.GetDrawSize(paddingBox.Size),
            };
            var marginBox = new Rectangle
            {
                Size = style.Margin.GetDrawSize(borderBox.Size),
            };

            return new DrawBox
            {
                MarginBox = marginBox,
                BorderBox = borderBox,
                PaddingBox = paddingBox,
                ContentBox = contentBox,
            };
        }

        private DrawBox CalculateHeightOnInlineElement(CoreUIDomElement element, DrawBox currentBox)
        {
            throw new NotImplementedException();
        }

        // Width

        private DrawBox CalculateWidth(CoreUIDomElement element)
            => element.Style.Display switch
            {
                DisplayStyle.Block => CalculateWidthOnBlockElement(element),
                DisplayStyle.Inline => CalculateWidthOnInlineElement(element),
                _ => new DrawBox(),
            };

        private DrawBox CalculateWidthOnBlockElement(CoreUIDomElement element)
        {
            var width = element.Style.Width.ValueKind switch
            {
                ValueKind.Default => element.DefaultStyle.Width,
                ValueKind.Inherit => element.Parent is CoreUIDomElement ? (element.Parent as CoreUIDomElement).Style.Width : new LengthHint(element.Parent.DrawBox.ContentBox.Width),
                ValueKind.Value => element.Style.Width,
                _ => element.Style.Width
            };

            if (width.ValueKind == ValueKind.Auto)
            {
                return CalculateWidthOnBlockElementWithAutoValue(element);
            }

            return CalculateWidthOnBlockElementWithProvidedValue(element);
        }

        private DrawBox CalculateWidthOnBlockElementWithAutoValue(CoreUIDomElement element)
        {
            var style = element.Style;

            var marginBox = new Rectangle
            {
                Size = new Size
                {
                    Width = element.Parent.DrawBox.ContentBox.Width,
                },
            };

            var borderBox = new Rectangle
            {
                Size = new Size
                {
                    Width = marginBox.Width - style.Margin.Left.Value.GetDrawValue(marginBox.Width) - style.Margin.Right.Value.GetDrawValue(marginBox.Width),
                },
            };

            var paddingBox = new Rectangle
            {
                Size = new Size
                {
                    Width = borderBox.Width - style.Border.Box.Left.Value.GetDrawValue(marginBox.Width) - style.Border.Box.Right.Value.GetDrawValue(marginBox.Width),
                },
            };

            var contentBox = new Rectangle
            {
                Size = new Size
                {
                    Width = paddingBox.Width - style.Padding.Left.Value.GetDrawValue(marginBox.Width) - style.Padding.Right.Value.GetDrawValue(marginBox.Width),
                },
            };

            return new DrawBox
            {
                MarginBox = marginBox,
                BorderBox = borderBox,
                PaddingBox = paddingBox,
                ContentBox = contentBox,
            };
        }

        private DrawBox CalculateWidthOnBlockElementWithProvidedValue(CoreUIDomElement element)
        {
            var style = element.Style;

            var contentBox = new Rectangle
            {
                Size = new Size
                {
                    Width = style.Width.Value.GetDrawValue(element.Parent.DrawBox.ContentBox.Width),
                }
            };

            return new DrawBox
            {
                ContentBox = contentBox,
            };
        }

        private DrawBox CalculateWidthOnInlineElement(CoreUIDomElement element)
        {
            throw new NotImplementedException();
        }
    }
}
