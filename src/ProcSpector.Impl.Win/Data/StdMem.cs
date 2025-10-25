using ProcSpector.API;
using ProcSpector.Impl.Win.Memory;

namespace ProcSpector.Impl.Win.Data
{
    public sealed class StdMem : IMemRegion
    {
        private readonly MemoryRegion _item;

        public StdMem(MemoryRegion item) => _item = item;

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