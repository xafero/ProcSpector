using ProcSpector.API.Memory;

namespace ProcSpector.API
{
    public interface IMemRegion
    {
        long BaseAddress { get; }
        long Size { get; }
        MemoryProtect Protection { get; }
        MemoryState State { get; }
        MemoryType Type { get; }
    }
}