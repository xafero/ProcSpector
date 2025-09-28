using System;

namespace ProcSpector.Lib
{
    public interface IProcess
    {
        string ProcessName { get; }

        IntPtr Hwnd { get; }
    }
}