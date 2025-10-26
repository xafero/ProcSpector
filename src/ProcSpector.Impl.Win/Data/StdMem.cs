using ProcSpector.API;
using ProcSpector.Impl.Win.Memory;

namespace ProcSpector.Impl.Win.Data
{
    public sealed class StdMem : IMemRegionEx
    {
        private readonly MemoryRegion _item;

        public StdMem(MemoryRegion item, int procId)
        {
            _item = item;
            ProcessId = procId;
        }

        public int ProcessId { get; }

        public long BaseAddress => _item.BaseAddress;
        public long Size => _item.Size;
        public string Protection => $"{(MemoryProtect)_item.Protection}";
        public string State => $"{(MemoryState)_item.State}";
        public string Type => $"{(MemoryType)_item.Type}";

        public override string ToString()
            => $"({BaseAddress})";

        internal MemoryRegion GetReal() => _item;
    }
}