namespace ProcSpector.API
{
    public interface IFile
    {
        string FullName { get; }
        long Length { get; }
        byte[] Bytes { get; }
    }
}