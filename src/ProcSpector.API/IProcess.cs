namespace ProcSpector.API
{
    public interface IProcess
    {
        int? Id { get; }
        string? Name { get; }
        string? Path { get; }
    }
}