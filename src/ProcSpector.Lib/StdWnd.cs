using System;
using System.Diagnostics;
using ByteSizeLib;
using static ProcSpector.Lib.MiscExt;

namespace ProcSpector.Lib
{
    public sealed class StdWnd : IHandle
    {
        private readonly IntPtr _hWnd;

        public StdWnd(IntPtr hWnd)
        {
            _hWnd = hWnd;
            Title = Win32.GetWindowText(_hWnd);
        }

        public IntPtr Handle => _hWnd;
        public string? Title { get; }

        public override string ToString()
            => $"({_hWnd})";
    }
}