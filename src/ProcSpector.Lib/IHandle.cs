using System;
using System.Drawing;

namespace ProcSpector.Lib
{
    public interface IHandle
    {
        uint? ThreadId { get; }
        IntPtr? Handle { get; }
        string? Title { get; }
        Point? Point { get; }
        Size? Size { get; }
    }
}