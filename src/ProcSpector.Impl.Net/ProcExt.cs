using System.IO;
using ProcSpector.API;
using System.Diagnostics;

#pragma warning disable CA1416

namespace ProcSpector.Impl.Net
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

        public static void OpenInShell(string? path)
        {
            var info = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(info);
        }

        public static bool Kill(IProcess proc)
        {
            var real = ((StdProc)proc).Proc;
            real.Kill(entireProcessTree: true);
            return real.HasExited;
        }

        public static StdProc GetStdProc(IProcess proc, ISystem sys)
        {
            StdProc raw;
            if (proc is StdProc sdp)
                raw = sdp;
            else
                raw = new StdProc(Process.GetProcessById(proc.Id), sys);
            return raw;
        }
    }
}