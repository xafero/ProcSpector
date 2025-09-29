using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcSpector.Lib
{
    public record WinStruct(
        IntPtr WindowHandle,
        uint ProcessId,
        uint ThreadId,
        IntPtr? ParentHandle
    );

    internal static class Win32
    {
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private delegate bool EnumCallBack(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumCallBack lpEnumFunc, IntPtr lParam);

        [DllImport("user32", SetLastError = true)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumCallBack lpEnumFunc, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32", SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32", SetLastError = true)]
        private static extern IntPtr GetParent(IntPtr hWnd);

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

        public static List<WinStruct> GetWindows(IntPtr? parent = null)
        {
            var list = new List<WinStruct>();
            if (parent is { } parentPtr)
                EnumChildWindows(parentPtr, Callback, IntPtr.Zero);
            else
                EnumWindows(Callback, IntPtr.Zero);
            return list;

            bool Callback(IntPtr hWnd, IntPtr _)
            {
                var tId = GetWindowThreadProcessId(hWnd, out var pId);
                var oId = GetMyParent(hWnd);
                var item = new WinStruct(hWnd, pId, tId, oId);
                list.Add(item);
                return true;
            }
        }

        private static IntPtr? GetMyParent(IntPtr hWnd)
        {
            var res = GetParent(hWnd);
            return res == 0 ? null : res;
        }

        private static string? GetTextFromBld(IntPtr hWnd, Func<IntPtr, StringBuilder, int, int> func)
        {
            const int max = 256;
            var sb = new StringBuilder(max);
            var size = func(hWnd, sb, max);
            var title = sb.ToString()[..size];
            return title.TrimOrNull();
        }

        public static string? GetWindowText(IntPtr hWnd)
        {
            return GetTextFromBld(hWnd, GetWindowText);
        }

        public static string? GetWindowClass(IntPtr hWnd)
        {
            return GetTextFromBld(hWnd, GetClassName);
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