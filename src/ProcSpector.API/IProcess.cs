using System;

namespace ProcSpector.API
{
    public interface IProcess
    {
        int Id { get; }
        string? Name { get; }
        DateTime? StartTime { get; }
        int Threads { get; }
        int Handles { get; }
        long WorkingSet { get; }
        long PagedMem { get; }
        string? FileName { get; }
    }
}