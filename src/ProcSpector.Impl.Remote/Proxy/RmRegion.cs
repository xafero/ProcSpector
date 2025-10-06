using ProcSpector.API;
using ProcSpector.API.Memory;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmRegion : IMemRegion
    {
        public long BaseAddress { get; set; }
        public long Size { get; set; }
        public MemoryProtect Protection { get; set; }
        public MemoryState State { get; set; }
        public MemoryType Type { get; set; }
    }
}