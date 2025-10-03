using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ProcSpector.Lib.Memory
{
    public static class MemoryReader
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct MemoryBasicInformation
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [DllImport("kernel32")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32")]
        private static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress,
            out MemoryBasicInformation lpBuffer, uint dwLength);

        [DllImport("kernel32")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        private const uint PROCESS_QUERY_INFORMATION = 0x0400;
        private const uint PROCESS_VM_READ = 0x0010;

        private const uint MEM_COMMIT = 0x1000;
        private const uint MEM_FREE = 0x10000;
        private const uint MEM_RESERVE = 0x2000;

        private const uint PAGE_NOACCESS = 0x01;
        private const uint PAGE_GUARD = 0x100;

        public static IEnumerable<IMemRegion> ReadAllMemoryRegions(Process process)
        {
            const uint access = PROCESS_QUERY_INFORMATION | PROCESS_VM_READ;
            var processHandle = OpenProcess(access, false, process.Id);
            if (processHandle == IntPtr.Zero)
                yield break;
            foreach (var region in ReadAllMemoryRegions(processHandle))
                yield return region;
            CloseHandle(processHandle);
        }

        private static IEnumerable<MemoryRegion> ReadAllMemoryRegions(IntPtr processHandle)
        {
            var address = IntPtr.Zero;
            var maxAddress = IntPtr.Size == 4 ? new IntPtr(0x7fffffff) : new IntPtr(0x7fffffffffff);

            while (address.ToInt64() < maxAddress.ToInt64())
            {
                var result = VirtualQueryEx(processHandle, address, out var mbi,
                    (uint)Marshal.SizeOf(typeof(MemoryBasicInformation)));

                if (result == 0)
                    break;

                var isUsable = mbi.State == MEM_COMMIT &&
                               mbi.Protect != PAGE_NOACCESS &&
                               (mbi.Protect & PAGE_GUARD) == 0;
                if (isUsable)
                {
                    var region = new MemoryRegion
                    {
                        BaseAddress = mbi.BaseAddress,
                        Size = mbi.RegionSize.ToInt64(),
                        Protection = mbi.Protect,
                        State = mbi.State,
                        Type = mbi.Type
                    };

                    var buffer = new byte[mbi.RegionSize.ToInt64()];

                    if (ReadProcessMemory(processHandle, mbi.BaseAddress, buffer, buffer.Length, out var bytesRead))
                    {
                        if (bytesRead < buffer.Length)
                        {
                            Array.Resize(ref buffer, bytesRead);
                        }
                        region.Data = buffer;
                        yield return region;
                    }
                }

                address = new IntPtr(mbi.BaseAddress.ToInt64() + mbi.RegionSize.ToInt64());
            }
        }
    }
}