namespace ProcSpector.API
{
    public interface IFile
    {
        string? FullName { get; }

        byte[]? Bytes { get; }
    }
}