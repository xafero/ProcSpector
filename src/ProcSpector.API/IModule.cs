namespace ProcSpector.API
{
    public interface IModule
    {
        long BaseAddress { get; }
        string FileName { get; }
        long Size { get; }
    }
}