using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcSpector.Lib.Memory;

namespace ProcSpector.Lib
{
    public sealed class StdSystem : ISystem
    {
        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            var raw = (StdProc)proc;
            var real = raw._process;
            var regions = MemoryReader.ReadAllMemoryRegions(real);
            foreach (var item in regions)
                if (WrapR(item) is { } wrap)
                    yield return wrap;
        }

        private static IMemRegion WrapR(MemoryRegion region)
        {
            var wrap = new StdMem(region);
            return wrap;
        }

        private static IEnumerable<WinStruct> GetAllHandles(IProcess proc)
        {
            foreach (var top in Win32.GetWindows().Where(w => w.ProcessId == proc.Id))
            {
                yield return top;

                foreach (var sub in Win32.GetWindows(top.WindowHandle))
                    yield return sub;
            }
        }

        public IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            var res = GetAllHandles(proc).Select(WrapH)
                .Where(x =>
                {
                    var p = (StdWnd)x;
                    return p.ProcessId == proc.Id && p.Title != null;
                });
            return res;
        }

        private static IHandle WrapH(WinStruct obj)
        {
            var wrap = new StdWnd(obj.WindowHandle, obj.ProcessId, obj.ThreadId, obj.ParentHandle);
            return wrap;
        }
    }
}