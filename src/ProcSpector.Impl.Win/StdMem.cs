using System;
using ByteSizeLib;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Lib.Memory;

namespace ProcSpector.Lib
{
    public sealed class StdMem : IMemRegion
    {
        public MemoryRegion Mem { get; }

        public StdMem(MemoryRegion mem)
            => Mem = mem;

        public IntPtr BaseAddress => Mem.BaseAddress;
        public ByteSize Size => MiscExt.AsBytes(Mem.Size);
        public MemoryProtect Protection => (MemoryProtect)Mem.Protection;
        public MemoryState State => (MemoryState)Mem.State;
        public MemoryType Type => (MemoryType)Mem.Type;

        public override string ToString()
            => $"({BaseAddress})";

        public void CreateMemSave() => Win32Ext.CreateMemSave(this);
    }
}