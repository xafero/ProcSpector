using System;
using ByteSizeLib;
using ProcSpector.Lib.Memory;

namespace ProcSpector.Lib
{
    public sealed class StdMem : IMemRegion
    {
        private readonly MemoryRegion _mem;

        public StdMem(MemoryRegion mem)
            => _mem = mem;

        public IntPtr BaseAddress => _mem.BaseAddress;
        public ByteSize Size => MiscExt.AsBytes(_mem.Size);
        public uint Protection => _mem.Protection;
        public uint State => _mem.State;
        public uint Type => _mem.Type;

        public override string ToString()
            => $"({BaseAddress})";
    }
}