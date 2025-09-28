using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcSpector.Lib
{
    public record WinStruct(
        int MainWindowHandle,
        uint ProcessId,
        uint ThreadId
    );

    internal static class Win32
    {
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private delegate bool CallBackPtr(int hWnd, int lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(CallBackPtr lpEnumFunc, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static List<WinStruct> GetWindows()
        {
            var list = new List<WinStruct>();
            EnumWindows(Callback, IntPtr.Zero);
            return list;

            bool Callback(int hWnd, int lparam)
            {
                var tId = GetWindowThreadProcessId(hWnd, out var pId);
                var item = new WinStruct(hWnd, pId, tId);
                list.Add(item);
                return true;
            }
        }

        public static string? GetWindowText(IntPtr hWnd)
        {
            const int max = 256;
            var sb = new StringBuilder(max);
            var size = GetWindowText(hWnd, sb, max);
            var title = sb.ToString()[..size];
            return title.TrimOrNull();
        }

        public static Rectangle? GetWindowSize(IntPtr hWnd)
        {
            if (GetWindowRect(hWnd, out var r))
            {
                var x = r.Left;
                var y = r.Top;
                var width = r.Right - r.Left;
                var height = r.Bottom - r.Top;
                var item = new Rectangle(x, y, width, height);
                return item;
            }
            return null;
        }
    }
}