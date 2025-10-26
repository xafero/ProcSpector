using System;
using System.Runtime.InteropServices;

#pragma warning disable CA1416

namespace ProcSpector.Impl.Win.Internal
{
    public static class Win32Dsk
    {
        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32")]
        private static extern bool IsIconic(IntPtr hWnd);

        private const int SwRestore = 9;

        public static bool ActivateWindowById(IntPtr hWnd, bool recursive)
        {
            if (recursive && Win32.GetMyParent(hWnd) is { } parent)
            {
                ActivateWindowById(parent, recursive);
            }
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SwRestore);
            }
            return SetForegroundWindow(hWnd);
        }

        [DllImport("user32")]
        private static extern bool SetCursorPos(int x, int y);

        public static bool SetMouseToOffset(IntPtr hWnd, (int X, int Y) offset)
        {
            if (hWnd == IntPtr.Zero)
                return false;
            if (Win32.GetWindowRect(hWnd, out var rect))
                return SetCursorPos(rect.Left + offset.X, rect.Top + offset.Y);
            return false;
        }
    }
}