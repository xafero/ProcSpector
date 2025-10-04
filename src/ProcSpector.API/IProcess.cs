using System;
using ByteSizeLib;

namespace ProcSpector.API
{
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

        void Kill();
        void CreateMemSave();
        void CreateScreenShot();
        void CreateMiniDump();
        void OpenFolder();
    }
}