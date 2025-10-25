using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcSpector.API;
using ProcSpector.Impl.Net;

namespace ProcSpector.Impl.Win
{
    public sealed class WinPlatform : NetPlatform, ISystem2
    {
        public override ISystem2 System2 => this;

        public IAsyncEnumerable<IHandle> GetHandles(IProcess arg)
            => GetHandlesSync(arg).ToAsyncEnumerable();

        private IEnumerable<IHandle> GetHandlesSync(IProcess arg)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<IMemRegion> GetRegions(IProcess arg)
            => GetRegionsSync(arg).ToAsyncEnumerable();

        private IEnumerable<IMemRegion> GetRegionsSync(IProcess arg)
        {
            throw new System.NotImplementedException();
        }

        public Task<IFile?> CreateScreenShot(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public Task<IFile?> CreateScreenShot(IHandle handle)
        {
            throw new System.NotImplementedException();
        }

        public Task<IFile?> CreateMemSave(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public Task<IFile?> CreateMemSave(IMemRegion mem)
        {
            throw new System.NotImplementedException();
        }

        public Task<IFile?> CreateMiniDump(IProcess proc)
        {
            throw new System.NotImplementedException();
        }
    }
}