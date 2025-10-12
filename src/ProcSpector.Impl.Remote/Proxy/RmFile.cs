using ProcSpector.API;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmFile : IFile
    {
        public string? FullName { get; set; }
        public long Length { get; set; }
        public byte[]? Bytes { get; set; }
    }
}