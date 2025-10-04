using System;
using ProcSpector.API;

namespace ProcSpector.Lib
{
    public sealed class StdWnd : IHandle
    {
        public StdWnd(IntPtr hWnd, uint procId, uint thrId, IntPtr? parent)
        {
            Handle = hWnd;
            ProcessId = procId;
            ThreadId = thrId;
            Parent = parent;

            Title = Win32.GetWindowText(hWnd);
            Class = Win32.GetWindowClass(hWnd);
            var ws = Win32.GetWindowSize(hWnd);
            X = ws?.Location.X;
            Y = ws?.Location.Y;
            W = ws?.Size.Width;
            H = ws?.Size.Height;
        }

        public uint ProcessId { get; }
        public uint? ThreadId { get; }
        public IntPtr? Parent { get; }
        public IntPtr? Handle { get; }
        public string? Class { get; }
        public string? Title { get; }
        public int? X { get; }
        public int? Y { get; }
        public int? W { get; }
        public int? H { get; }

        public override string ToString()
            => $"({Handle})";

        public void CreateScreenShot()
        {
            throw new NotImplementedException();
        }
    }
}