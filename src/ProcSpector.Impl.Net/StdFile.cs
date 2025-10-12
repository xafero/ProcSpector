using System;
using System.IO;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public sealed class StdFile : IFile
    {
        public string? FullName { get; set; }
        public long Length { get; set; }
        public byte[]? Bytes { get; set; }

        public static StdFile Create(string filePath, byte[]? bytes, Action<Stream>? action)
        {
            byte[] array;
            if (bytes != null)
                array = bytes;
            else
            {
                using var mem = new MemoryStream();
                action?.Invoke(mem);
                mem.Flush();
                array = mem.ToArray();
            }
            var fileName = Path.GetFileName(filePath);
            var res = new StdFile { FullName = fileName, Length = array.Length, Bytes = array };
            return res;
        }
    }
}