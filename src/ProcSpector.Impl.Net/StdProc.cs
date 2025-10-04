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

        public StdProc(Process process)
        {
            Proc = process;
        }

        public int Id => Proc.Id;
        public string Name => Proc.ProcessName;
        public DateTime? StartTime => Proc.StartTime;
        public DateTime? ExitTime => Proc.ExitTime;
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

        public void Kill()
        {
            throw new System.NotImplementedException();
        }

        public void CreateMemSave()
        {
            throw new System.NotImplementedException();
        }

        public void CreateScreenShot()
        {
            throw new System.NotImplementedException();
        }

        public void CreateMiniDump()
        {
            throw new System.NotImplementedException();
        }

        public void OpenFolder()
        {
            throw new System.NotImplementedException();
        }
    }
}