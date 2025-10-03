using System;
using ByteSizeLib;

namespace ProcSpector.Lib.Memory
{
    public interface IMemRegion
    {
        IntPtr BaseAddress { get; }
        ByteSize Size { get; }
        uint Protection { get; }
        uint State { get; }
        uint Type { get; }
    }
}