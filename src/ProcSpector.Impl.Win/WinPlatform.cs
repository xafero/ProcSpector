using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcSpector.API;
using ProcSpector.Impl.Net;
using ProcSpector.Impl.Net.Tools;
using ProcSpector.Impl.Win.Data;
using ProcSpector.Impl.Win.Internal;
using ProcSpector.Impl.Win.Memory;

namespace ProcSpector.Impl.Win
{
    public sealed class WinPlatform : NetPlatform, ISystem2
    {
        public override ISystem2 System2 => this;

        public IAsyncEnumerable<IHandle> GetHandles(IProcess arg)
            => GetHandlesSync(arg).ToAsyncEnumerable();

        private IEnumerable<IHandle> GetHandlesSync(IProcess proc)
        {
            var res = GetAllHandles(proc).Select(WrapH)
                .Where(x =>
                {
                    var p = (StdWnd)x;
                    return p.ProcessId == proc.Id && p.Title != null;
                });
            return res;
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

        public IAsyncEnumerable<IMemRegion> GetRegions(IProcess arg)
            => GetRegionsSync(arg).ToAsyncEnumerable();

        private IEnumerable<IMemRegion> GetRegionsSync(IProcess arg)
        {
            var real = ProcExt.GetStdProc(arg).GetReal();
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

        public Task<IFile?> CreateScreenShot(IProcess proc)
        {
            var res = Win32Ext.CreateScreenShot(proc);
            return Task.FromResult(res);
        }

        public Task<IFile?> CreateScreenShot(IHandle handle)
        {
            var res = Win32Ext.CreateScreenShot(handle);
            return Task.FromResult(res);
        }

        public Task<IFile?> CreateMemSave(IProcess proc)
        {
            var res = Win32Ext.CreateMemSave(proc);
            return Task.FromResult<IFile?>(res);
        }

        public Task<IFile?> CreateMemSave(IMemRegion mem)
        {
            var res = Win32Ext.CreateMemSave(mem);
            return Task.FromResult(res);
        }

        public Task<IFile?> CreateMiniDump(IProcess proc)
        {
            var res = Win32Ext.CreateMiniDump(proc);
            return Task.FromResult<IFile?>(res);
        }
    }
}