using System;

namespace ProcSpector.Lib.Memory
{
    public sealed class MemoryRegion : IMemRegion
    {
        public IntPtr BaseAddress { get; set; }
        public long Size { get; set; }
        public uint Protection { get; set; }
        public uint State { get; set; }
        public uint Type { get; set; }
        public byte[]? Data { get; set; }
    }
}