using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ProcSpector.Lib.Memory
{
    internal static class MiniDumper
    {
        [DllImport("dbghelp", SetLastError = true)]
        private static extern bool MiniDumpWriteDump(
            IntPtr hProcess,
            uint processId,
            IntPtr hFile,
            MiniDumpType dumpType,
            IntPtr exceptionParam,
            IntPtr userStreamParam,
            IntPtr callbackParam
        );

        public static void CreateDump(Process process, string filePath,
            MiniDumpType dumpType = MiniDumpType.MiniDumpWithFullMemory)
        {
            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            var success = MiniDumpWriteDump(
                process.Handle,
                (uint)process.Id,
                fs.SafeFileHandle.DangerousGetHandle(),
                dumpType,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);

            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}