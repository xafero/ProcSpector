using System;
using ByteSizeLib;

namespace ProcSpector.Lib
{
    public interface IProcess
    {
        int Id { get; }
        string Name { get; }
        DateTime StartTime { get; }
        int Threads { get; }
        int Handles { get; }
        TimeSpan CpuTime { get; }
        ByteSize WorkingSet { get; }
        ByteSize VirtualMem { get; }
    }
}