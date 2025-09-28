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
            => _hWnd = hWnd;

        public override string ToString()
            => $"({_hWnd})";
    }
}