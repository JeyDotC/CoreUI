using System;

namespace CoreUI
{
    //
    // Summary:
    //     Describes bitwise combination of modifier keys.
    [Flags]
    public enum ModifierKeys
    {
        //
        // Summary:
        //     Either of the Shift keys.
        Shift = 1,
        //
        // Summary:
        //     Either of the Ctrl keys.
        Control = 2,
        //
        // Summary:
        //     Either of the Alt keys
        Alt = 4,
        //
        // Summary:
        //     The super key ("Windows" key on Windows)
        Super = 8,
        //
        // Summary:
        //     The caps-lock is enabled.
        CapsLock = 16,
        //
        // Summary:
        //     The num-lock is enabled.
        NumLock = 32
    }
}