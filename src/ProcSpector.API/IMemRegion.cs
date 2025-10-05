using System;
using ByteSizeLib;
using ProcSpector.API.Memory;

namespace ProcSpector.API
{
    public interface IMemRegion
    {
        IntPtr BaseAddress { get; }
        ByteSize Size { get; }
        MemoryProtect Protection { get; }
        MemoryState State { get; }
        MemoryType Type { get; }
        
        void CreateMemSave();
    }
}