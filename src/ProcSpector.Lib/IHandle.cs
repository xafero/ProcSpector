using System;
using ByteSizeLib;

namespace ProcSpector.Lib
{
    public interface IHandle
    {
        IntPtr Handle { get; }

        string? Title { get; }
    }
}