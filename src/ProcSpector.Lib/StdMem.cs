using System;
using ByteSizeLib;
using ProcSpector.Lib.Memory;

namespace ProcSpector.Lib
{
    public sealed class StdMem : IMemRegion
    {
        internal readonly MemoryRegion _mem;

        public StdMem(MemoryRegion mem)
            => _mem = mem;

        public IntPtr BaseAddress => _mem.BaseAddress;
        public ByteSize Size => MiscExt.AsBytes(_mem.Size);
        public MemoryProtection Protection => (MemoryProtection)_mem.Protection;
        public MemoryState State => (MemoryState)_mem.State;
        public MemoryType Type => (MemoryType)_mem.Type;

        public override string ToString()
            => $"({BaseAddress})";
    }
}