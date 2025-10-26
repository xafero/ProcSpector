using System;
using System.IO;

namespace ProcSpector.Core
{
    public sealed class FileTmp : IDisposable
    {
        private string _file;

        public FileTmp(string? file = null)
        {
            _file = file ?? Path.GetTempFileName();
        }

        public string FullPath => _file;

        public void Dispose()
        {
            if (File.Exists(_file))
                File.Delete(_file);
        }

        public void SetBytes(byte[]? bytes)
        {
            File.WriteAllBytes(_file, bytes ?? []);
        }
    }
}