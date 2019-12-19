using System;
using CoreUI.Dom.Styles;

namespace CoreUI.Dom
{
    internal static class CoreUIDomElementExtensionMethods
    {
        public static StyleValue<LengthHint> GetWidthHint(this CoreUIDomElement element) => element.Style.Width.ValueKind switch
        {
            ValueKind.Default => element.DefaultStyle.Width,
            ValueKind.Inherit => element.Parent is CoreUIDomElement ? (element.Parent as CoreUIDomElement).Style.Width : new LengthHint(element.Parent.DrawBox.ContentBox.Width),
            ValueKind.Value => element.Style.Width,
            _ => element.Style.Width
        };

        public static StyleValue<LengthHint> GetHeightHint(this CoreUIDomElement element) => element.Style.Height.ValueKind switch
        {
            ValueKind.Default => element.DefaultStyle.Height,
            ValueKind.Inherit => element.Parent is CoreUIDomElement ? (element.Parent as CoreUIDomElement).Style.Height : new LengthHint(element.Parent.DrawBox.ContentBox.Height),
            ValueKind.Value => element.Style.Height,
            _ => element.Style.Height
        };
    }
}
