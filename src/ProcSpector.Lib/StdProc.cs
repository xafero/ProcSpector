using System;
using System.Diagnostics;
using ByteSizeLib;

namespace ProcSpector.Lib
{
    public sealed class StdProc : IProcess
    {
        private readonly Process _process;

        public StdProc(Process process)
            => _process = process;

        public int Id => _process.Id;
        public string Name => _process.ProcessName;
        public DateTime? StartTime => MiscExt.TryGet<DateTime?>(() => _process.StartTime);
        public DateTime? ExitTime => MiscExt.TryGet<DateTime?>(() => _process.ExitTime);
        public int Threads => _process.Threads.Count;
        public int Handles => _process.HandleCount;
        public TimeSpan? CpuTime => MiscExt.TryGet<TimeSpan?>(() => _process.TotalProcessorTime);
        public ByteSize WorkingSet => ByteSize.FromBytes(_process.WorkingSet64);
        public ByteSize PagedMem => ByteSize.FromBytes(_process.PagedMemorySize64);
        public ByteSize VirtualMem => ByteSize.FromBytes(_process.VirtualMemorySize64);
        public bool Responding => _process.Responding;
        public ProcessModule? Main => MiscExt.TryGet(() => _process.MainModule);
        public string? FileName => Main?.FileName;

        public override string ToString()
            => $"({Name})";
    }
}