using System;

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
        }

        public uint ProcessId { get; }
        public uint ThreadId { get; }
        public IntPtr Handle { get; }
        public string? Title { get; }

        public override string ToString()
            => $"({Handle})";
    }
}