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

        private IEnumerable<IHandle> GetHandlesSync(IProcess arg)
        {
            throw new System.NotImplementedException();
        }

        public Task<IFile?> CreateScreenShot(IHandle handle)
        {
            throw new System.NotImplementedException();
        }
    }
}