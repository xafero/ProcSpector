using System;

namespace ProcSpector.Lib
{
    public interface IHandle
    {
        IntPtr? Parent { get; }
        IntPtr? Handle { get; }
        string? Title { get; }
        int? X { get; }
        int? Y { get; }
        int? W { get; }
        int? H { get; }
        string? Class { get; }
    }
}