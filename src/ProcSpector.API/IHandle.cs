namespace ProcSpector.API
{
    public interface IHandle
    {
        long? Parent { get; }
        long? Handle { get; }
        string? Title { get; }
        int? X { get; }
        int? Y { get; }
        int? W { get; }
        int? H { get; }
        string? Class { get; }
    }
}