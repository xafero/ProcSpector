using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcSpector.API;
using ProcSpector.Impl.Net;
using ProcSpector.Impl.Win.Internal;
using ProcSpector.Impl.Win.Memory;

namespace ProcSpector.Impl.Win
{
    public sealed class WinPlatform : NetPlatform
    {
        public override Task<bool> CreateMemSave(IProcess proc)
        {
            var res = Win32Ext.CreateMemSave(proc, this);
            return Task.FromResult(res);
        }

        public override Task<bool> CreateScreenShot(IProcess proc)
        {
            var res = Win32Ext.CreateScreenShot(proc);
            return Task.FromResult(res);
        }

        public override Task<bool> CreateMiniDump(IProcess proc)
        {
            var res = Win32Ext.CreateMiniDump(proc);
            return Task.FromResult(res);
        }

        private static IMemRegion WrapR(MemoryRegion region)
        {
            var wrap = new StdMem(region);
            return wrap;
        }

        private IEnumerable<IMemRegion> GetRegionsInt(IProcess proc)
        {
            var raw = ProcExt.GetStdProc(proc, this);
            var real = raw.Proc;
            var regions = MemoryReader.ReadAllMemoryRegions(real);
            foreach (var item in regions)
                if (WrapR(item) is { } wrap)
                    yield return wrap;
        }

        public override Task<IEnumerable<IMemRegion>> GetRegions(IProcess proc)
        {
            var res = GetRegionsInt(proc);
            return Task.FromResult(res);
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

        private static IHandle WrapH(WinStruct obj)
        {
            var wrap = new StdWnd(obj.WindowHandle, obj.ProcessId, obj.ThreadId, obj.ParentHandle);
            return wrap;
        }

        private IEnumerable<IHandle> GetHandlesInt(IProcess proc)
        {
            var res = GetAllHandles(proc).Select(WrapH)
                .Where(x =>
                {
                    var p = (StdWnd)x;
                    return p.ProcessId == proc.Id && p.Title != null;
                });
            return res;
        }

        public override Task<IEnumerable<IHandle>> GetHandles(IProcess proc)
        {
            var res = GetHandlesInt(proc);
            return Task.FromResult(res);
        }
    }
}