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
        public DateTime StartTime => MiscExt.TryGet(() => _process.StartTime);
        public int Threads => _process.Threads.Count;
        public int Handles => _process.HandleCount;
        public TimeSpan CpuTime => MiscExt.TryGet(() => _process.TotalProcessorTime);
        public ByteSize WorkingSet => ByteSize.FromBytes(_process.WorkingSet64);
        public ByteSize VirtualMem => ByteSize.FromBytes(_process.VirtualMemorySize64);

        public override string ToString()
            => $"({Name})";
    }
}