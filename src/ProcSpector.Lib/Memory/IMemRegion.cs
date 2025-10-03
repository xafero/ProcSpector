using System;
using ByteSizeLib;

namespace ProcSpector.Lib.Memory
{
    public interface IMemRegion
    {
        IntPtr BaseAddress { get; }
        ByteSize Size { get; }
        MemoryProtection Protection { get; }
        MemoryState State { get; }
        MemoryType Type { get; }
    }
}