using System;
using System.Diagnostics;
using ByteSizeLib;
using ProcSpector.API;
using static ProcSpector.Core.MiscExt;

namespace ProcSpector.Impl.Net
{
    public sealed class StdProc : IProcess
    {
        public Process Proc { get; }
        private ISystem Sys { get; }

        public StdProc(Process process, ISystem sys)
        {
            Proc = process;
            Sys = sys;
        }

        public int Id => Proc.Id;
        public string Name => Proc.ProcessName;
        public DateTime? StartTime => Proc.StartTime;
        // public DateTime? ExitTime => Proc.ExitTime;
        public int Threads => Proc.Threads.Count;
        public int Handles => Proc.HandleCount;
        public TimeSpan? CpuTime => Proc.TotalProcessorTime;
        public ByteSize WorkingSet => AsBytes(Proc.WorkingSet64);
        public ByteSize PagedMem => AsBytes(Proc.PagedMemorySize64);
        public ByteSize VirtualMem => AsBytes(Proc.VirtualMemorySize64);
        public bool Responding => Proc.Responding;
        public ProcessModule? Main => Proc.HasExited ? null : Proc.MainModule;
        public string? FileName => Main?.FileName;

        public override string ToString()
            => $"({Name})";

        // public void Kill() => Sys.Kill(this);
        // public void CreateMemSave() => Sys.CreateMemSave(this);
        // public void CreateScreenShot() => Sys.CreateScreenShot(this);
        // public void CreateMiniDump() => Sys.CreateMiniDump(this);
        // public void OpenFolder() => Sys.OpenFolder(this);
        long IProcess.WorkingSet => Proc.WorkingSet64;
        long IProcess.PagedMem => Proc.PagedMemorySize64;
    }
}