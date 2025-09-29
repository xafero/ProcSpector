using System;

namespace ProcSpector.Lib
{
    public interface IHandle
    {
        uint? ThreadId { get; }
        IntPtr? Handle { get; }
        string? Title { get; }
        int? X { get; }
        int? Y { get; }
        int? W { get; }
        int? H { get; }
        string? Class { get; }
    }
}