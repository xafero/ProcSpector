using System;

namespace ProcSpector.Lib.Memory
{
    public interface IMemRegion
    {
        IntPtr BaseAddress { get; }
        long Size { get; }
        uint Protection { get; }
        uint State { get; }
        uint Type { get; }
        byte[]? Data { get; }
    }
}