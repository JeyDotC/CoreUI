using GL = GLFW.Glfw;

namespace System
{
    public static class SystemExtensionMethods
    {
        public static int Check(this int valueToCheck, string actionAttempted)
        {
            if (valueToCheck < 0)
            {
                GL.GetError(out var errorMessage);
                throw new InvalidOperationException($"Could not {actionAttempted}. SDL. Error: {errorMessage}");
            }

            return valueToCheck;
        }

        public static IntPtr Check(this IntPtr valueToCheck, string actionAttempted)
        {
            if (valueToCheck == IntPtr.Zero)
            {
                GL.GetError(out var errorMessage);
                throw new InvalidOperationException($"Could not {actionAttempted}. SDL. Error: {errorMessage}");
            }

            return valueToCheck;
        }
    }
}
