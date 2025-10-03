using System;
using System.Collections.Generic;
using System.Diagnostics;
using ByteSizeLib;
using static ProcSpector.Lib.MiscExt;
using D = ProcSpector.Lib.Defaults;

namespace ProcSpector.Lib
{
    public sealed class StdProc : IProcessEx
    {
        internal readonly Process _process;

        public StdProc(Process process)
            => _process = process;

        public int Id => _process.Id;
        public string Name => _process.ProcessName;
        public DateTime? StartTime => _process.StartTime;
        public DateTime? ExitTime => _process.ExitTime;
        public int Threads => _process.Threads.Count;
        public int Handles => _process.HandleCount;
        public TimeSpan? CpuTime => _process.TotalProcessorTime;
        public ByteSize WorkingSet => AsBytes(_process.WorkingSet64);
        public ByteSize PagedMem => AsBytes(_process.PagedMemorySize64);
        public ByteSize VirtualMem => AsBytes(_process.VirtualMemorySize64);
        public bool Responding => _process.Responding;
        public ProcessModule? Main => _process.HasExited ? null : _process.MainModule;
        public string? FileName => Main?.FileName;
        public IEnumerable<IModule> Modules => D.System.GetModules(this);
        public IEnumerable<IHandle> Windows => D.System.GetHandles(this);

        public override string ToString()
            => $"({Name})";
    }
}