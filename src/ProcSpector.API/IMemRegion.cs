using ProcSpector.API;

namespace ProcSpector.API
{
    public interface IMemRegion
    {
        long BaseAddress { get; }
        long Size { get; }
        string Protection { get; }
        string State { get; }
        string Type { get; }
    }
}