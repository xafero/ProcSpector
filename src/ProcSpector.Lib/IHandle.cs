using System;

namespace ProcSpector.Lib
{
    public interface IHandle
    {
        uint ProcessId { get; }
        uint ThreadId { get; }
        IntPtr Handle { get; }
        string? Title { get; }
    }
}