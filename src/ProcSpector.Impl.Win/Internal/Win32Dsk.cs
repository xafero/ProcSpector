using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

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

        public static bool ActivateProcessById(int procId)
        {
            var process = Process.GetProcessById(procId);
            if (process.MainWindowHandle == IntPtr.Zero)
                return false;
            var handle = process.MainWindowHandle;
            return ActivateWindowById(handle);
        }

        public static bool ActivateWindowById(IntPtr hWnd)
        {
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SwRestore);
            }
            return SetForegroundWindow(hWnd);
        }
    }
}