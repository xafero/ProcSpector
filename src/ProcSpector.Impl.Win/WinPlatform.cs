using System.Collections.Generic;
using ProcSpector.API;
using ProcSpector.Impl.Net;
using System.Linq;
using ProcSpector.Impl.Win.Internal;
using ProcSpector.Impl.Win.Memory;

namespace ProcSpector.Impl.Win
{
    public sealed class WinPlatform : NetPlatform
    {
        public override IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            var raw = (StdProc)proc;
            var real = raw.Proc;
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

        public override IEnumerable<IHandle> GetHandles(IProcess proc)
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

        private static IEnumerable<WinStruct> GetAllHandles(IProcess proc)
        {
            foreach (var top in Win32.GetWindows().Where(w => w.ProcessId == proc.Id))
            {
                yield return top;

                foreach (var sub in Win32.GetWindows(top.WindowHandle))
                    yield return sub;
            }
        }

        public override void CreateMemSave(IProcess proc)
            => Win32Ext.CreateMemSave(proc);

        public override void CreateScreenShot(IProcess proc)
            => Win32Ext.CreateScreenShot(proc);

        public override void CreateMiniDump(IProcess proc)
            => Win32Ext.CreateMiniDump(proc);
    }
}