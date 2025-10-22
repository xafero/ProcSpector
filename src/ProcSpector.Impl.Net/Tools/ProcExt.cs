using System;
using System.Diagnostics;

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
    }
}