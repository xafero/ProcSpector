using System;
using System.Diagnostics;
using ProcSpector.API;
using System.IO;
using ProcSpector.Impl.Net.Data;

namespace ProcSpector.Impl.Net.Tools
{
    public static class ProcExt
    {
        public static ProcessModule? TryMainModule(this Process item)
        {
            try
            {
                return item.MainModule;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? TryStartTime(this Process item)
        {
            try
            {
                return item.StartTime;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool OpenInShell(string? path)
        {
            var info = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(info);
            return true;
        }

        public static bool OpenFolder(IProcess proc) => OpenFileFolder(proc.Path);
        public static bool OpenFolder(IModule mod) => OpenFileFolder(mod.FileName);

        public static bool OpenFileFolder(string? file)
        {
            var dir = Path.GetDirectoryName(file);
            return ProcExt.OpenInShell(dir);
        }

        public static bool Kill(IProcess proc)
        {
            var real = GetStdProc(proc).GetReal();
            real.Kill(entireProcessTree: true);
            return real.HasExited;
        }

        public static StdProc GetStdProc(IProcess proc)
        {
            StdProc raw;
            if (proc is StdProc sdp)
                raw = sdp;
            else
                raw = new StdProc(Process.GetProcessById(proc.Id));
            return raw;
        }
    }
}