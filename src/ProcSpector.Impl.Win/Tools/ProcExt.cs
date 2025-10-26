using System.Diagnostics;
using ProcSpector.API;
using System.Linq;
using ProcSpector.Impl.Win.Data;
using ProcSpector.Impl.Win.Memory;

namespace ProcSpector.Impl.Win.Tools
{
    public static class WProcExt
    {
        public static StdMem GetStdMem(IMemRegion region)
        {
            StdMem raw;
            if (region is StdMem sdp)
                raw = sdp;
            else
                raw = FindMemory((IMemRegionEx)region)!;
            return raw;
        }

        private static StdMem? FindMemory(IMemRegionEx region)
        {
            var proc = Process.GetProcessById(region.ProcessId);
            var mr = MemoryReader.ReadAllMemoryRegions(proc)
                .FirstOrDefault(m =>
                    m.BaseAddress == region.BaseAddress &&
                    m.Size == region.Size
                );
            if (mr == null)
                return null;
            return new StdMem(mr);
        }
    }
}