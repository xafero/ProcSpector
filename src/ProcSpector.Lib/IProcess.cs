using System;
using ByteSizeLib;

namespace ProcSpector.Lib
{
    public interface IModule
    {
    }

    public interface IProcess
    {
        int Id { get; }
        string Name { get; }
        DateTime? StartTime { get; }
        int Threads { get; }
        int Handles { get; }
        ByteSize WorkingSet { get; }
        ByteSize PagedMem { get; }
        string? FileName { get; }
    }
}