using CoreUI.Sdl.SDL2;

namespace System
{
    public static class SystemExtensionMethods
    {
        public static int Check(this int valueToCheck, string actionAttempted)
        {
            if (valueToCheck < 0)
            {
                throw new InvalidOperationException($"Could not {actionAttempted}. SDL. Error: {SDL.SDL_GetError()}");
            }

            return valueToCheck;
        }

        public static IntPtr Check(this IntPtr valueToCheck, string actionAttempted)
        {
            if (valueToCheck == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Could not {actionAttempted}. SDL. Error: {SDL.SDL_GetError()}");
            }

            return valueToCheck;
        }
    }
}
