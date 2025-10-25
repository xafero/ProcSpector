using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProcSpector.API;
using ProcSpector.Impl.Net.Data;
using FF = ProcSpector.API.FeatureFlags;

namespace ProcSpector.Impl.Net
{
    public class NetPlatform : IPlatform, ISystem1, ISystem3
    {
        public ISystem1 System1 => this;
        public ISystem2? System2 => null;
        public ISystem3 System3 => this;

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

        public IAsyncEnumerable<IModule> GetModules(IProcess proc)
            => GetModulesSync(proc).ToAsyncEnumerable();

        private IEnumerable<IModule> GetModulesSync(IProcess proc)
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

        public Task<bool> OpenFolder(IModule mod)
        {
            throw new System.NotImplementedException();
        }
    }
}