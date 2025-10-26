namespace ProcSpector.API
{
    public interface IMemRegionEx : IMemRegion
    {
        int ProcessId { get; }
    }
}