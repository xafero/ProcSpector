using System.Diagnostics;
using System.IO;

namespace ProcSpector.Lib
{
    public static class ProcExt
    {
        public static void OpenFolder(IProcess proc) => OpenFileFolder(proc.FileName);
        public static void OpenFolder(IModule mod) => OpenFileFolder(mod.FileName);

        public static void OpenFileFolder(string? file)
        {
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

        public static void Kill(IProcess proc)
        {
            var real = ((StdProc)proc)._process;
            real.Kill(entireProcessTree: true);
        }
    }
}