using System;

namespace CoreUI
{
    public class MouseButtonEventArgs : EventArgs
    {
        //
        // Summary:
        //     Initializes a new instance of the MouseButtonEventArgs class.
        //
        // Parameters:
        //   button:
        //     The mouse button.
        //
        //   state:
        //     The state of the button.
        //
        //   modifiers:
        //     The modifier keys.
        public MouseButtonEventArgs(MouseButton button, InputState state, ModifierKeys modifiers)
        {
            Action = state;
            Button = button;
            Modifiers = modifiers;
        }

        //
        // Summary:
        //     Gets the state of the mouse button when the event was raised.
        public InputState Action { get; }
        //
        // Summary:
        //     Gets the mouse button that raised the event.
        public MouseButton Button { get; }
        //
        // Summary:
        //     Gets the key modifiers present when mouse button was pressed.
        public ModifierKeys Modifiers { get; }
    }
}