using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProcSpector.API;
using ProcSpector.Impl.Net.Data;
using FF = ProcSpector.API.FeatureFlags;

namespace ProcSpector.Impl.Net
{
    public class NetPlatform : IPlatform, ISystem
    {
        public ISystem System => this;

        public FF Flags => FF.GetUserInfo | FF.GetProcesses;

        public Task<IUserInfo?> GetUserInfo()
            => Task.FromResult<IUserInfo?>(GetUserInfoSync());

        private IUserInfo GetUserInfoSync()
            => new NetUser();

        public IAsyncEnumerable<IProcess> GetProcesses()
            => GetProcessesSync().ToAsyncEnumerable();

        private IEnumerable<IProcess> GetProcessesSync()
        {
            foreach (var item in Process.GetProcesses())
                yield return new NetProc(item);
        }

        public IAsyncEnumerable<IHandle> GetHandles(IProcess arg)
            => GetHandlesSync(arg).ToAsyncEnumerable();

        public Task<IFile?> CreateScreenShot(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

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

        public IAsyncEnumerable<IModule> GetModules(IProcess arg)
            => GetModulesSync(arg).ToAsyncEnumerable();

        private IEnumerable<IModule> GetModulesSync(IProcess arg)
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

        public Task<bool> Kill(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> OpenFolder(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> ISystem.OpenFolder(IModule mod)
        {
            throw new System.NotImplementedException();
        }

        public Task OpenFolder(IModule mod)
        {
            throw new System.NotImplementedException();
        }
    }
}