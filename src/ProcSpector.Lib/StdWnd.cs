using System;
using System.Drawing;

namespace ProcSpector.Lib
{
    public sealed class StdWnd : IHandle
    {
        public StdWnd(IntPtr hWnd, uint procId, uint thrId)
        {
            Handle = hWnd;
            ProcessId = procId;
            ThreadId = thrId;

            Title = Win32.GetWindowText(hWnd);
            var ws = Win32.GetWindowSize(hWnd);
            Point = ws?.Location;
            Size = ws?.Size;
        }

        public uint ProcessId { get; }
        public uint? ThreadId { get; }
        public IntPtr? Handle { get; }
        public string? Title { get; }
        public Point? Point { get; }
        public Size? Size { get; }

        public override string ToString()
            => $"({Handle})";
    }
}