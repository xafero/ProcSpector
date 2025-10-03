using System.Diagnostics;
using System.IO;

namespace ProcSpector.Lib
{
    public static class ProcExt
    {
        public static void OpenFolder(IProcess proc)
        {
            var file = proc.FileName;
            var dir = Path.GetDirectoryName(file);
            OpenInShell(dir);
        }

        private static void OpenInShell(string? path)
        {
            var info = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(info);
        }
    }
}